using EZFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectltemController : MonoBehaviour
{
    public Button Open;//开始匹配

    private void Start()
    {
        Open.onClick.AddListener(OpenMethod);
    }

    void OpenMethod()
    {
        //进入房间道具处理
        //EZComponent.GetConment<Game_Prepare>().request.readyItems.Add("");
        NetworkController.Instance.Send(EZComponent.GetConment<Game_Prepare>().request);
    }

}
