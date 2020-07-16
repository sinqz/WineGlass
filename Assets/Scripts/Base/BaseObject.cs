using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//游戏中所有游戏对象的父类，包含了一些基础的共有字段
public class BaseObject : Reference
{
    public GameUser User { get; set; }
}
