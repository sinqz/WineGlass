using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public class EZUIGroup
    {
        private static Transform parent;

        private static Dictionary<string, GameObject> keyValuePairs = new Dictionary<string, GameObject>();

        public static Transform Parent
        {
            get
            {
                if (parent == null)
                {
                    //parent = EZComponent.GetConment<InItUI>().baseParent;
                    parent = GameObject.Find("Canvas").transform;
                }
                return parent;
            }
        }

        public static Transform Open(string name)
        {
            return Creat(name);
        }

        public static Transform Open<T>(T type)
        {
            return Creat(type.ToString());
        }


        public static Transform Creat(string name)
        {
            if (keyValuePairs.ContainsKey(name))
            {
                return default;
            }

            var game = Resources.Load(name) as GameObject;
            if (game != null)
            {
                var obj = MonoBehaviour.Instantiate(game, Parent);
                obj.name = name;
                keyValuePairs.Add(name, obj);
                return obj.transform;
            }
            return default;
        }

        public static void Open<T>(string name, T value)
        {
            var game = Resources.Load(name) as GameObject;
            if (game != null)
            {
                var obj = MonoBehaviour.Instantiate(game, Parent);
                obj.GetComponent<IOpen>().Open(value);
                keyValuePairs.Add(name, obj);
            }
        }

        public static void Close<T>(T name)
        {
            Destory(name.ToString());
        }


        public static void Close(string name)
        {
            Destory(name);
        }

        public static void Destory(string name)
        {
            if (keyValuePairs.ContainsKey(name))
            {
                MonoBehaviour.Destroy(keyValuePairs[name]);
                keyValuePairs.Remove(name);
            }
        }

    }
}
