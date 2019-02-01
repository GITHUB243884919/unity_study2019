using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace UFrame.Network
{
    internal class LogicSocket : IDisposable
    {
        private const int HEAD_LEN = 2;
        public Void_object logCallback = null;
        #region Fields
        private string host;
        private int port;
        private EndPoint iep;
        private Socket socket;

        private Queue<byte[]> bufferToSend;
        private NetBuffer receiveBuffer;

        internal volatile object PacksLock;
        internal volatile Queue<byte[]> Packets;

        private bool needHash;

        private bool mDisposed = false;

        #endregion

        #region Constructors

        internal LogicSocket()
        {
            bufferToSend = new Queue<byte[]>();
            receiveBuffer = new NetBuffer();
            this.PacksLock = new object();
            this.Packets = new Queue<byte[]>();
        }

        #endregion

        #region Dispose

        ~LogicSocket()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!mDisposed)
            {
                mDisposed = true;
                if (disposing)
                {
                    socket.Close();
                    socket = null;
                }
            }
        }

        #endregion

        internal void Connect(string host, int port)
        {
            this.host = host;
            this.port = port;

            try
            {
                iep = new IPEndPoint(IPAddress.Parse(host), port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                Log("Connect Begin BeginReceiveFrom " + ((IPEndPoint)iep).Port + " hash " + ((IPEndPoint)iep).GetHashCode());
                socket.BeginReceiveFrom(receiveBuffer.Bytes, 0, NetBuffer.MTU, SocketFlags.None, ref iep, ReceiveCallback, null);
                Log("Connect End BeginReceiveFrom " + ((IPEndPoint)iep).Port + " hash " + ((IPEndPoint)iep).GetHashCode());
            }
            catch (Exception ex)
            {
                Log("LogicSocket Connect " + ex);
                return;
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            int receiveLen = 0;
            try
            {
                receiveLen = socket.EndReceiveFrom(result, ref iep);
                Log("EndReceiveFrom Port " + ((IPEndPoint)iep).Port + " hash " + ((IPEndPoint)iep).GetHashCode());
            }
            catch (Exception ex)
            {

                Log("LogicSocket ReceiveCallback EndReceiveFrom " + ex);
                return;
            }
            if (receiveLen == 0)
            {
                Log("LogicSocket ReceiveCallback receiveLen == 0 ");
                return;
            }

            if (receiveLen >= HEAD_LEN)
            {
                lock (PacksLock)
                {
                    byte[] buffer = new byte[receiveLen];
                    Buffer.BlockCopy(receiveBuffer.Bytes, 0, buffer, 0, receiveLen);

                    Packets.Enqueue(buffer);
                }
            }
            else
            {
                Log("LogicSocket Receive short data");
            }

            try
            {
                Log("ReceiveCallback Begin BeginReceiveFrom " + ((IPEndPoint)iep).Port);
                socket.BeginReceiveFrom(receiveBuffer.Bytes, 0, NetBuffer.MTU, SocketFlags.None, ref iep, ReceiveCallback, null);
                Log("ReceiveCallback End BeginReceiveFrom " + ((IPEndPoint)iep).Port);
            }
            catch (Exception ex)
            {
                Log("LogicSocket ReceiveCallback BeginReceiveFrom " + ex);
                return;
            }
            
        }

        internal void Send(byte[] msg)
        {
            if (msg == null)
            {
                return;
            }

            lock (bufferToSend)
            {
                bufferToSend.Enqueue(msg);
                if (bufferToSend.Count == 1)
                {
                    PostSend();
                }
            }
        }

        private void PostSend()
        {
            if (bufferToSend.Count != 0)
            {
                byte[] msg = bufferToSend.Peek();
                
                try
                {
                    Log("begin PostSend BeginSendTo");
                    socket.BeginSendTo(msg, 0, msg.Length, SocketFlags.None, iep, SendCallback, null);
                    Log("end PostSend BeginSendTo");
                }
                catch (Exception ex)
                {

                    Log("LogicSocket PostSend BeginSendTo " + ex);
                    return;
                }
            }
        }

        private void SendCallback(IAsyncResult async)
        {
            int sendLen = 0;
            try
            {
                Log("begin SendCallback EndSendTo");
                sendLen = socket.EndSendTo(async);
                Log("end SendCallback EndSendTo");
            }
            catch (Exception ex)
            {
                Log("LogicSocket EndSendTo " + ex);
                return;
            }
			
			if (sendLen == 0)
            {
                Log("LogicSocket EndSendTo sendLen == 0 ");
            }

            lock (bufferToSend)
            {
                if (bufferToSend.Count != 0)
                {
                    bufferToSend.Dequeue();
                    PostSend();
                }
            }
        }

        internal void Reconnect()
        {
            lock (bufferToSend)
            {
                bufferToSend.Clear();
            }
            
            Connect(host, port);
        }

        void Log(object msg)
        {
            if (logCallback != null)
            {
                logCallback(msg);
            }
        }
    }

}
