using EZFramework;
using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainController : MonoBehaviour
{
    private void Awake()
    {
        //获得游戏信息初始值
        Actor.mAcrot();
    }

    private void Start()
    {
        EZComponent.AddConment<Game_Lobby>();
        EZComponent.AddConment<Game_Head>();
        EZComponent.AddConment<Game_Single>();
    }

}
