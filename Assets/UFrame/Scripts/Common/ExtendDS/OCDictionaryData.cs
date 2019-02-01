using UnityEngine;
using System.Collections;

namespace Private
{
	public class OCDictionaryData : IObjectPoolable
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

		private object basedata;
		private DataType dataType;

        #region Pool
        public static ObjectPool<OCDictionaryData> pool = new ObjectPool<OCDictionaryData>();

        public OCDictionaryData()
        {
        }

        public void OnDeathToPool()
        {
            basedata = null;
        }
        #endregion

        public OCDictionaryData (int basedata)
		{
			this.basedata = basedata;
			dataType = DataType.Int;
		}

		public OCDictionaryData (long basedata)
		{
			this.basedata = basedata;
			dataType = DataType.Long;
		}

		public OCDictionaryData (float basedata)
		{
			this.basedata = basedata;
			dataType = DataType.Float;
		}

		public OCDictionaryData (double basedata)
		{
			this.basedata = basedata;
			dataType = DataType.Double;
		}

		public OCDictionaryData (bool basedata)
		{
			this.basedata = basedata;
			dataType = DataType.Bool;
		}

		public OCDictionaryData (object basedata)
		{
			ChangeValue (basedata);
		}

		public void ChangeValue (object newData)
		{
			this.basedata = newData;
			if (newData is int) {
				dataType = DataType.Int;
			} else if (newData is long) {
				dataType = DataType.Long;
			} else if (newData is float) {
				dataType = DataType.Float;
			} else if (newData is double) {
				dataType = DataType.Double;
			} else if (newData is bool) {
				dataType = DataType.Bool;
			} else {
				dataType = DataType.Refrence;
			}
		}

		public object GetObject ()
		{
			return basedata;
		}

		public int GetInt (int defaultValue)
		{
			switch (dataType) {
			case DataType.Int:
			case DataType.Long:
			case DataType.Float:
			case DataType.Double:
				return System.Convert.ToInt32 (basedata);
			default:
				return defaultValue;
			}
		}

		public long GetLong (long defaultValue)
		{
			switch (dataType) {
			case DataType.Int:
			case DataType.Long:
			case DataType.Float:
			case DataType.Double:
				return System.Convert.ToInt64 (basedata);
			default:
				return defaultValue;
			}
		}

		public float GetFloat (float defaultValue)
		{
			switch (dataType) {
			case DataType.Int:
			case DataType.Long:
			case DataType.Float:
			case DataType.Double:
				return System.Convert.ToSingle (basedata);
			default:
				return defaultValue;
			}
		}

		public double GetDouble (double defaultValue)
		{
			switch (dataType) {
			case DataType.Int:
			case DataType.Long:
			case DataType.Float:
			case DataType.Double:
				return System.Convert.ToDouble (basedata);
			default:
				return defaultValue;
			}
		}

		public bool GetBool (bool defaultValue)
		{
			if (dataType == DataType.Bool) {
				return System.Convert.ToBoolean (basedata);
			} else {
				return defaultValue;
			}
		}

		public string GetString (string defaultValue)
		{
			try {
				return System.Convert.ToString (basedata);
			} catch {
			}
			return defaultValue;
		}

		public T GetData<T> (T defaultValue = null) where T : class
		{
			if (dataType == DataType.Refrence && basedata != null) {
				return basedata as T;
			} else {
				return defaultValue;
			}
		}
	}
}