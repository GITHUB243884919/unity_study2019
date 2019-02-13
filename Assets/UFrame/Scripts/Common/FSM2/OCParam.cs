using UnityEngine;
using System.Collections;

namespace UFrame.FSM
{
    public class ObjectParam 
    {
        private enum DataType
        {
            Int,
            Long,
            Float,
            Double,
            Bool,
            Refrence,
        }

        private object data;
        private DataType dataType;

        public ObjectParam()
        {
        }

        public void Set(object data)
        {
            this.data = data;
            if (data is int)
            {
                dataType = DataType.Int;
            }
            else if (data is long)
            {
                dataType = DataType.Long;
            }
            else if (data is float)
            {
                dataType = DataType.Float;
            }
            else if (data is double)
            {
                dataType = DataType.Double;
            }
            else if (data is bool)
            {
                dataType = DataType.Bool;
            }
            else
            {
                dataType = DataType.Refrence;
            }
        }

        public int GetInt(int defaultValue)
        {
            switch (dataType)
            {
                case DataType.Int:
                case DataType.Long:
                case DataType.Float:
                case DataType.Double:
                    return System.Convert.ToInt32(data);
                default:
                    return defaultValue;
            }
        }

        public long GetLong(long defaultValue)
        {
            switch (dataType)
            {
                case DataType.Int:
                case DataType.Long:
                case DataType.Float:
                case DataType.Double:
                    return System.Convert.ToInt64(data);
                default:
                    return defaultValue;
            }
        }

        public float GetFloat(float defaultValue)
        {
            switch (dataType)
            {
                case DataType.Int:
                case DataType.Long:
                case DataType.Float:
                case DataType.Double:
                    return System.Convert.ToSingle(data);
                default:
                    return defaultValue;
            }
        }

        public double GetDouble(double defaultValue)
        {
            switch (dataType)
            {
                case DataType.Int:
                case DataType.Long:
                case DataType.Float:
                case DataType.Double:
                    return System.Convert.ToDouble(data);
                default:
                    return defaultValue;
            }
        }

        public bool GetBool(bool defaultValue)
        {
            if (dataType == DataType.Bool)
            {
                return System.Convert.ToBoolean(data);
            }
            else
            {
                return defaultValue;
            }
        }

        public string GetString(string defaultValue)
        {
            try
            {
                return System.Convert.ToString(data);
            }
            catch
            {
            }
            return defaultValue;
        }

        public T GetRefrence<T>(T defaultValue = null) where T : class
        {
            if (dataType == DataType.Refrence && data != null)
            {
                return data as T;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}