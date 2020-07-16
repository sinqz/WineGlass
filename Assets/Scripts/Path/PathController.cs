using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//路径管理器，路径的公有字段
public class PathController : MonoBehaviour
{
   
    //在路径上添加随机路障，代码在此处添加

    private PathCell[] RoadPath { get { return MapPing.Instance.MapDifferent; } }



}
