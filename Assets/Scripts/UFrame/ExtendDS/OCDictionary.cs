using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Private;

public class OCDictionary : IObjectPoolable
{
    public static ObjectPool<OCDictionary> pool = new ObjectPool<OCDictionary>();

    private Dictionary<string, OCDictionaryData> baseData;

	public OCDictionary (params object[] paramKeyValue)
	{
		baseData = new Dictionary<string, OCDictionaryData> ();
		if ((paramKeyValue.Length % 2) != 0) {
			throw new System.Exception ("JWData initiation error: param number asymmetry");
		}
		for (int i = 0, imax = paramKeyValue.Length; i < imax; i += 2) {
			baseData [paramKeyValue [i].ToString ()] = new OCDictionaryData (paramKeyValue [i + 1]);
		}
	}

    #region Pool
    public OCDictionary()
    {
        baseData = new Dictionary<string, OCDictionaryData>();
    }

    public void InsertObject(string key, object value)
    {
        OCDictionaryData data = OCDictionaryData.pool.New();
        data.ChangeValue(value);

        baseData[key] = data;
    }

    public void OnDeathToPool()
    {
        foreach (var node in baseData)
        {
            OCDictionaryData.pool.Delete(node.Value);
        }
        baseData.Clear();
    }
    #endregion

    public void Insert (string key, object value)
	{
		baseData [key] = new OCDictionaryData (value);
	}

	public void Change (string key, object value)
	{
		if (Contains (key)) {
			baseData [key].ChangeValue (value);
		} else {
			Insert (key, value);
		}
	}

	public bool Contains (string key)
	{
		return baseData.ContainsKey (key);
	}

	private OCDictionaryData GetDataItem (string key)
	{
		if (Contains (key)) {
			return baseData [key];
		} else {
			return null;
		}
	}

	public object this [string key] {
		get {
			return GetObject (key, null);
		}
		set {
			Change (key, value);
		}
	}

	public int GetInt (string key, int defaultValue = -1)
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetInt (defaultValue);
		} else {
			return defaultValue;
		}
	}

	public long GetLong (string key, long defaultValue = -1)
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetLong (defaultValue);
		} else {
			return defaultValue;
		}
	}

	public float GetFloat (string key, float defaultValue = -1)
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetFloat (defaultValue);
		} else {
			return defaultValue;
		}
	}

	public double GetDouble (string key, double defaultValue = -1)
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetDouble (defaultValue);
		} else {
			return defaultValue;
		}
	}

	public bool GetBool (string key, bool defaultValue = false)
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetBool (defaultValue);
		} else {
			return defaultValue;
		}
	}

	public string GetString (string key, string defaultValue = "")
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetString (defaultValue);
		} else {
			return defaultValue;
		}
	}

	public object GetObject (string key, object defaultValue = null)
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetObject ();
		} else {
			return defaultValue;
		}
	}

    public T GetData<T> (string key, T defaultValue = null) where T : class
	{
		OCDictionaryData dataItem = GetDataItem (key);
		if (dataItem != null) {
			return dataItem.GetData<T> (defaultValue);
		} else {
			return defaultValue;
		}
	}

    public string [] GetParamNameList()
    {
        string[] result = null;

        List<string> paramNameList = new List<string>(baseData.Keys);

        result = paramNameList.ToArray();
        return result;
    }
}