using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace EZFramework
{
    public class EZCmmonFunction : MonoBehaviour
    {
        /// <summary>
        /// 获取toggleGroup的值
        /// </summary>
        /// <param name="group"></param>
        /// <param name="action"></param>
        public static void GetToggleGroupValue(ToggleGroup group, Action<string> action)
        {
            foreach (var item in group.ActiveToggles())
            {
                if (item.isOn)
                {
                    var value = item.GetComponentInChildren<Text>().text;
                    action(value);
                }
            }
        }

    }
}
