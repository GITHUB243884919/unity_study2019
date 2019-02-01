namespace UFrame.Network
{
	internal sealed class NetBuffer
	{
        public static int MTU = 512;
		#region Fields

		private byte [] bytes;
		private int size;

		#endregion

		#region Properties

		internal byte [] Bytes {
			get { return bytes; }
		}

		internal int Size {
			get { return size; }
			set { size = value; }
		}

		internal int Space {
			get { return bytes.Length - size; }
		}

		#endregion

		#region Constructors

		internal NetBuffer ()
		{
			size = 0;
			bytes = new byte [MTU];
		}

		#endregion

		#region Methods

		internal void Expand (int length)
		{
			var nlength = bytes.Length + length;
			var array = new byte [nlength];
			for (var i = 0; i < bytes.Length; ++i) {
				array [i] = bytes [i];
			}
			for (var i = bytes.Length; i < nlength; ++i) {
				array [i] = 0;
			}
			bytes = array;
		}

		internal void WriteByte (byte b)
		{
			bytes [size++] = b;
		}

		internal void WriteInt (int v)
		{
			bytes [size++] = (byte) (v >> 24);
			bytes [size++] = (byte) (v >> 16);
			bytes [size++] = (byte) (v >> 8);
			bytes [size++] = (byte) (v);
		}

        internal void WriteInt64(long v)
        {
            bytes[size++] = (byte)(v >> 56);
            bytes[size++] = (byte)(v >> 48);
            bytes[size++] = (byte)(v >> 40);
            bytes[size++] = (byte)(v >> 32);
            bytes[size++] = (byte)(v >> 24);
            bytes[size++] = (byte)(v >> 16);
            bytes[size++] = (byte)(v >> 8);
            bytes[size++] = (byte)(v);
        }

        internal void WriteBytes (byte [] bs)
		{
			for (var i = 0; i < bs.Length; ++i) {
				bytes [size++] = bs [i];
			}
		}

        internal void WriteBytes(byte[] bs, int len)
        {
            for (var i = 0; i < len; ++i)
            {
                bytes[size++] = bs[i];
            }
        }

        #endregion
    }
}