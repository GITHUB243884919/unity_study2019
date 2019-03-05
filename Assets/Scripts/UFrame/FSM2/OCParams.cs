using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UFrame.FSM
{
    public class ObjectParams
    {
        private Dictionary<string, ObjectParam> baseData;

        public ObjectParams()
        {
            baseData = new Dictionary<string, ObjectParam>();
        }

        public void Insert(string key, object value)
        {
            ObjectParam data = new ObjectParam();
            data.Set(value);

            baseData[key] = data;
        }


        public void Change(string key, object value)
        {
            if (Contains(key))
            {
                baseData[key].Set(value);
            }
            else
            {
                Insert(key, value);
            }
        }

        public bool Contains(string key)
        {
            return baseData.ContainsKey(key);
        }


        //public object this[string key]
        //{
        //    get
        //    {
        //        return GetObject(key, null);
        //    }
        //    set
        //    {
        //        Change(key, value);
        //    }
        //}

        public int GetInt(string key, int defaultValue = -1)
        {
            ObjectParam dataItem = GetDataItem(key);
            if (dataItem != null)
            {
                return dataItem.GetInt(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        public long GetLong(string key, long defaultValue = -1)
        {
            ObjectParam dataItem = GetDataItem(key);
            if (dataItem != null)
            {
                return dataItem.GetLong(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        public float GetFloat(string key, float defaultValue = -1)
        {
            ObjectParam dataItem = GetDataItem(key);
            if (dataItem != null)
            {
                return dataItem.GetFloat(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        public double GetDouble(string key, double defaultValue = -1)
        {
            ObjectParam dataItem = GetDataItem(key);
            if (dataItem != null)
            {
                return dataItem.GetDouble(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            ObjectParam dataItem = GetDataItem(key);
            if (dataItem != null)
            {
                return dataItem.GetBool(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        public string GetString(string key, string defaultValue = "")
        {
            ObjectParam dataItem = GetDataItem(key);
            if (dataItem != null)
            {
                return dataItem.GetString(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        public T GetRefrence<T>(string key, T defaultValue = null) where T : class
        {
            ObjectParam dataItem = GetDataItem(key);
            if (dataItem != null)
            {
                return dataItem.GetRefrence<T>(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        public string[] GetParamNameList()
        {
            string[] result = null;

            List<string> paramNameList = new List<string>(baseData.Keys);

            result = paramNameList.ToArray();
            return result;
        }

        private ObjectParam GetDataItem(string key)
        {
            if (Contains(key))
            {
                return baseData[key];
            }
            else
            {
                return null;
            }
        }
    }
}
