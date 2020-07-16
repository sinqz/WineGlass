using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EZFramework
{

    public class EZEvent
    {
        private static Dictionary<string, IActionRun> pairs = new Dictionary<string, IActionRun>();
        public static void Add(string key, UnityAction action)
        {
            if (!pairs.ContainsKey(key))
            {
                EZUnityAction zLUnityAction = new EZUnityAction(action);
                pairs.Add(key, zLUnityAction);
            }
        }
        public static void Add<T>(string key, UnityAction<T> action)
        {
            if (!pairs.ContainsKey(key))
            {
                EZUnityAction<T> zLUnityAction = new EZUnityAction<T>(action);
                pairs.Add(key, zLUnityAction);
            }
        }
        public static void Add<T, K>(string key, UnityAction<T, K> action)
        {
            if (!pairs.ContainsKey(key))
            {
                EZUnityAction<T, K> zLUnityAction = new EZUnityAction<T, K>(action);
                pairs.Add(key, zLUnityAction);
            }
        }
        public static void Run(string key)
        {
            if (pairs.ContainsKey(key))
            {
                var value = (EZUnityAction)pairs[key];
                value.Run();
            }
        }
        public static void Run<T>(string key, T t)
        {
            if (pairs.ContainsKey(key))
            {
                var value = (EZUnityAction<T>)pairs[key];
                value.Run(t);
            }
        }
        public static void Run<T, K>(string key, T t, K k)
        {
            if (pairs.ContainsKey(key))
            {
                var value = (EZUnityAction<T, K>)pairs[key];
                value.Run(t, k);
            }
        }
        public static void Remove(string key)
        {
            if (pairs.ContainsKey(key))
            {
                pairs.Remove(key);
            }
        }
    }
    internal interface IActionRun { }
    internal class EZUnityAction : IActionRun
    {
        public UnityAction UnityAction;
        public EZUnityAction(UnityAction action)
        {
            UnityAction = action;
        }
        public void Run()
        {
            UnityAction.Invoke();
        }
    }
    internal class EZUnityAction<T> : IActionRun
    {
        public UnityAction<T> UnityAction;

        public EZUnityAction(UnityAction<T> action)
        {
            UnityAction = action;
        }

        public void Run(T t)
        {
            UnityAction.Invoke(t);
        }
    }
    internal class EZUnityAction<T, K> : IActionRun
    {
        public UnityAction<T, K> UnityAction;
        public EZUnityAction(UnityAction<T, K> action)
        {
            UnityAction = action;
        }
        public void Run(T t, K k)
        {
            UnityAction.Invoke(t, k);
        }
    }
}






