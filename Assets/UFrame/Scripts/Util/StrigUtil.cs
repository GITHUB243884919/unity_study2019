using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtil
{
    /// <summary>
    /// 数字字符串分割成单个数字
    /// 考虑通用性，实现的是把字符串分割成规定字符串数组， 
    /// 比如输入 12345，规定长度2，那么输出 [1,2] [3,4] [5]
    /// 在这里是分割成单个数字，调用时，规定长度都是1
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitLen"></param>
    /// <returns></returns>
    public string[] SplitString(string str, int splitLen)
    {
        int strLen = str.Length;
        int countPart = (strLen + splitLen - 1) / splitLen;
        string[] parts = new string[countPart];
        for (int i = 0; i < parts.Length; i++)
        {
            splitLen = splitLen <= str.Length ? splitLen : str.Length;
            parts[i] = str.Substring(0, splitLen);
            str = str.Substring(splitLen, str.Length - splitLen);
        }
        return parts;
    }
}
