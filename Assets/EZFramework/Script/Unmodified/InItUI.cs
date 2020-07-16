using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public class InItUI : EZMonoBehaviour
    {
        public Transform baseParent;

        public Transform ThisGame<T>(T type)
        {
            return baseParent.Find(type.ToString());
        }

        public override void Start()
        {
            baseParent = GameObject.Find("Canvas").transform;
        }

    }
}
