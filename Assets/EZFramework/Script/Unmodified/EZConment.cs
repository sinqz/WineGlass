using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EZFramework
{
    public class EZComponent
    {
        private static Dictionary<string, EZMonoBehaviour> Lifecycles { get; set; } = new Dictionary<string, EZMonoBehaviour>();

        private static List<string> Keys { get; set; } = new List<string>();

        public static T AddConment<T>() where T : EZMonoBehaviour, new()
        {
            var key = typeof(T).ToString();
            if (!Lifecycles.ContainsKey(key))
            {
                var value = (T)Activator.CreateInstance(typeof(T));
                Lifecycles.Add(key, value);
                Keys.Add(key);
                return value;
            }
            return default;
        }

        public static void RemoveConment<T>() where T : EZMonoBehaviour, new()
        {
            var key = typeof(T).ToString();
            if (Lifecycles.ContainsKey(key))
            {
                Lifecycles[key].End();
                Lifecycles.Remove(key);
                Keys.Remove(key);
            }
        }

        public static T GetConment<T>() where T : EZMonoBehaviour, new()
        {
            var key = typeof(T).ToString();
            if (Lifecycles.ContainsKey(key))
            {
                return (T)Lifecycles[key];
            }
            return null;
        }

        internal static void Update()
        {
            for (int i = 0; i < Lifecycles.Keys.Count; i++)
            {
                var key = Keys[i];
                Lifecycles[key].Update();
            }
        }

        internal static void LateUpdate()
        {
            for (int i = 0; i < Lifecycles.Keys.Count; i++)
            {
                var key = Keys[i];
                Lifecycles[key].LateUpdate();
            }
        }
    }
}