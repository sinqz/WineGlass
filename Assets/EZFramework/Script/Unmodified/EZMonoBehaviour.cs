using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public class EZMonoBehaviour
    {
        public EZMonoBehaviour()
        {
            Start();
        }

        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }

        public virtual void End() { }
    }
}