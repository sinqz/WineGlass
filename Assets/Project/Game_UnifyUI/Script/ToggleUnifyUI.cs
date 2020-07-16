using EZFramework;
using NetWorkAndData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUnifyUI : MonoBehaviour
{
    #region 三次未摇骰子退赛
    /// <summary>
    /// 三次未摇骰子退赛
    /// </summary>
    public Toggle BeOutOfTheRace;
    #endregion

    #region 匹配退赛弹框
    /// <summary>
    /// 匹配退赛弹框
    /// </summary>
    public Toggle LoadBeOut;
    private Button ToQuit;
    private Button AgainEtc;

    #endregion

    #region 所有玩家被退赛返还入场金
    /// <summary>
    /// 所有玩家被退赛返还入场金
    /// </summary>
    public Toggle WithdrawalNotice;
    #endregion

    void Start()
    {
        LoadBeOut.onValueChanged.AddListener(IsOn => { if (IsOn) { LoadBeOutMethod(); } });
        LoadBeOut.onValueChanged.Invoke(true);
      BeOutOfTheRace.onValueChanged.AddListener(IsOn => { if (IsOn) { BeOutOfTheRaceMethod(); } });
        WithdrawalNotice.onValueChanged.AddListener(IsOn => { if (IsOn) { WithdrawalNoticeMethod(); } });
    }


    void LoadBeOutMethod()
    {
        ToQuit = transform.Find("LoadBeOut/ToQuit").GetComponent<Button>();
        AgainEtc = transform.Find("LoadBeOut/AgainEtc").GetComponent<Button>();
        ToQuit.onClick.AddListener(() => { NetworkController.Instance.Send(new ExitGameRoomRequest()); });
        AgainEtc.onClick.AddListener(() => EZComponent.RemoveConment<Game_UnifyUI>());
    }

    void BeOutOfTheRaceMethod()
    {


    }

    void WithdrawalNoticeMethod()
    {

    }
}
