using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryUtil
{
    public static int GetNonNullCount<TKey, TValue>(this Dictionary<TKey, TValue> dict)
    {
        int count = 0;
        foreach(var v in dict.Values)
        {
            if (v != null)
            {
                count++;
            }
        }
        return count;
    }
}
