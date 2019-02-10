using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

namespace UFrame.LUA
{
    public class ArrayData
    {
        public object[] Values { get; private set; }

        List<string> array = null;


        public int Count { get { return Values.Length; } }

        public ArrayData(LuaTable table)
        {
            Values = new object[table.Length];
            LuaArrayTable v = table.ToArrayTable();
            for(int i = 1, iMax = v.Length; i <= iMax; ++i)
            {
                Collect(i, v[i]);
            }
            v.Dispose();
        }

        // LuaArrayTable的索引是从1开始的，这里要对索引减1
        public void Collect(int key, object val)
        {
            Values[key - 1] = val;
        }

        public int GetInt(int id, int defaultValue = 0)
        {
            try
            {
                return System.Convert.ToInt32(Values[id]);
            }
            catch
            {

            }
            return defaultValue;
        }

        public double GetDouble(int id, double defaultValue = 0)
        {
            try
            {
                return System.Convert.ToDouble(Values[id]);
            }
            catch
            {

            }
            return defaultValue;
        }

        public string GetString(int id, string defaultValue = "")
        {
            try
            {
                return System.Convert.ToString(Values[id]);
            }
            catch
            {

            }
            return defaultValue;
        }

        public List<string> GetStringArray()
        {
            if (array == null)
            {
                array = new List<string>();
                for (int i = 0; i < Values.Length; i++)
                {
                    array.Add(System.Convert.ToString(Values[i]));
                }
            }
            return array;
        }

        public bool Contain(string id)
        {
            GetStringArray();
            if (array.Contains(id))
            {
                return true;
            }
            return false;

        }

        // for csv

        public ArrayData(string tableLine)
        {
            string strLine = null;
            if (tableLine.StartsWith("[") && tableLine.EndsWith("]"))
            {
                strLine = tableLine.Substring(1, tableLine.Length - 2);
            }
            else
            {
                Values = new object[0];
                return;
            }

            string[] _tmp = strLine.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);// CSV.CSVFormation.SplitDataLine (strLine);
            Values = new object[_tmp.Length];
            for (int i = 0, imax = _tmp.Length; i < imax; i++)
            {
                if (_tmp[i].StartsWith("\"") && _tmp[i].EndsWith("\""))
                {
                    Values[i] = _tmp[i].Substring(1, _tmp[i].Length - 2);
                }
                else
                {
                    if (_tmp[i].Contains("."))
                    {
                        Values[i] = (System.Convert.ToDouble(_tmp[i]));
                    }
                    else
                    {
                        Values[i] = (System.Convert.ToInt32(_tmp[i]));
                    }
                }
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append("[");
            for (int i = 0; i < Values.Length; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(",");
                }
                if (Values[i] is string)
                {
                    stringBuilder.Append("\"" + System.Convert.ToString(Values[i]) + "\"");
                }
                else
                {
                    stringBuilder.Append(System.Convert.ToString(Values[i]));
                }
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}