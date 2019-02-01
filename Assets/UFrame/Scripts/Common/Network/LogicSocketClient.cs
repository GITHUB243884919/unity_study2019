//using System;
//using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace UFrame.Network
{
    public enum MessageType : byte
    {
        Ping = 1,
        Login = 2,
        Leave = 4,
        Action = 5,
        Loading = 9,
        Ready = 10,
        ResendTurn = 11,
    }

    public class MessageHandler
    {
        public byte Method;
        public Void_Bytes Callbacks;
    }

    public class ReceiveMessage
    {
        public byte isHash;
        public byte method;
        public byte[] data;
    }

    public struct SocketParam
    {
        public string ip;
        public int port;
        public long battleId;
        public string token;
        public int selfIndex;
    }

    public class LogicSocketClient
    {
        public SocketParam initParam;
        public Void_object logCallback = null;

        private LogicSocket conn;
        private NetBuffer sendBufferTmp;

        private List<MessageHandler> messageHandlers;

        private List<byte[]> packs;

        public LogicSocketClient()
        {
            sendBufferTmp = new NetBuffer();
            conn = new LogicSocket();
            packs = new List<byte[]>();
        }

        public void Destroy()
        {
            if (packs != null)
            {
                packs.Clear();
                packs = null;
            }

            if (messageHandlers != null)
            {
                messageHandlers.Clear();
                messageHandlers = null;
            }

            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

        public void Init(SocketParam startData)
        {
            initParam.ip = startData.ip;
            initParam.port = startData.port;
            initParam.battleId = startData.battleId;
            initParam.token = startData.token;
            initParam.selfIndex = startData.selfIndex;
        }

        public void Connect()
        {
            conn.Connect(initParam.ip, initParam.port);
        }

        public void Reconnect()
        {
            conn.Reconnect();
        }

        public void Tick()
        {
            TickPackets();
        }

        public void Send(byte method, byte[] buffer = null, int len = 0)
        {
            sendBufferTmp.WriteByte(0);
            sendBufferTmp.WriteByte(method);
            sendBufferTmp.WriteInt64(initParam.battleId);
            sendBufferTmp.WriteByte((byte)initParam.selfIndex);

            if (buffer != null)
            {
                sendBufferTmp.WriteBytes(buffer, len);
            }

            byte[] tmp = new byte[sendBufferTmp.Size];
            Buffer.BlockCopy(sendBufferTmp.Bytes, 0, tmp, 0, sendBufferTmp.Size);

            conn.Send(tmp);
            sendBufferTmp.Size = 0;
        }

        private void TickPackets()
        {
            if (packs == null)
            {
                return;
            }

            packs.Clear();
            byte[] pack = null;

            
            lock (conn.PacksLock)
            {
                while(conn.Packets.Count > 0)
                {
                    pack = conn.Packets.Dequeue();
                    if (pack != null)
                    {
                        packs.Add(pack);
                    }
                }
            }

            for (int i = 0; i < packs.Count; i++)
            {
                Dispatch(packs[i]);
            }
        }

        private MessageHandler FindMessageHandler(byte method)
        {
            if (messageHandlers == null)
            {
                return null;
            }
            for (var i = 0; i < messageHandlers.Count; ++i)
            {
                if (messageHandlers[i].Method == method)
                {
                    return messageHandlers[i];
                }
            }
            return null;
        }

        private void Dispatch(byte[] pack)
        {
            ReceiveMessage msg = Deserialize(pack);
            var msgHandler = FindMessageHandler(msg.method);
            if (msgHandler != null)
            {
                msgHandler.Callbacks(msg.data);
            }
            else
            {
                Log("LogicSocketClient Dispath found not callback " + msg.method);
            }
        }

        private ReceiveMessage Deserialize(byte[] pack)
        {
            ReceiveMessage msg = new ReceiveMessage();

            int begin = 0;

            msg.isHash = pack[begin++];
            msg.method = pack[begin++];

            msg.data = new byte[pack.Length - begin];
            Buffer.BlockCopy(pack, begin, msg.data, 0, msg.data.Length);

            return msg;
        }

        public void RegisterMessageHandler(byte method, Void_Bytes callback)
        {
            if (messageHandlers == null)
            {
                messageHandlers = new List<MessageHandler>();
            }
            var mh = FindMessageHandler(method);
            if (mh == null)
            {
                mh = new MessageHandler();
                mh.Method = method;
                mh.Callbacks = callback;
                messageHandlers.Add(mh);
            }
            else
            {
                mh.Callbacks += callback;
            }
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