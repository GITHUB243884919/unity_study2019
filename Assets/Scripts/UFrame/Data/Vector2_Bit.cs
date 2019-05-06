
namespace UFrame.Data
{
    public class Vector2_Bit
    {
        int bitData = 0;

        public Vector2_Bit()
        {

        }

        public Vector2_Bit(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2_Bit(int bitData)
        {
            this.bitData = bitData;
        }

        public int x {
            get
            {
                return bitData >> 16;
            }

            set
            {
                bitData += (value << 16);
            }
        }

        public int y
        {
            get {
                return bitData & 0x0000FFFF;
            }

            set {
                bitData += value; 
            }
        }

        public int BitData
        {
            get { return bitData; }
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }
}

