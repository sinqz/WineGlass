using System.Collections;
using UnityEngine;
//常规版单位转换（带小数）
public class Utility
{
    /// <summary>
    /// 数字单位转换
    /// </summary>
    /// <param name="value">数值</param>
    /// <param name="decimalsNum">保留几位小数</param>
    /// <returns></returns>
    public static string TransUnit(long value)
    {
        if (value >= 1000000000000)
            return (long)((value * 1f / 1000000000000) * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1) + "T";
        else if (value >= 1000000000)
            return (long)((value * 1f / 1000000000) * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1) + "B";
        else if (value >= 1000000)
            return (long)((value * 1f / 1000000) * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1) + "M";
        else if (value >= 1000)
            return (long)((value * 1f / 1000) * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1) + "K";
        else
            return value.ToString();
    }
}
