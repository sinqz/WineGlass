using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
//游戏界面UI
public class MainPanlUI : MonoSingletonTemplateScript<MainPanlUI>
{
    private CanvasGroup TempCanvasGroup { get { return GetComponent<CanvasGroup>(); } }

    /// <summary>
    /// 玩家是否可以摇骰子
    /// </summary>
    public void OnEnterDice(bool IsNone)
    {
        TempCanvasGroup.interactable = IsNone;
        TempCanvasGroup.blocksRaycasts = IsNone;
    }
}
