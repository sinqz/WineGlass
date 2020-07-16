using NetWorkAndData;
using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 杯子人机区分
/// </summary>
public enum RoleType
{
    PLAYER,
    AI,
}

/// <summary>
/// 杯子当前的状态
/// </summary>
public enum CupStateType
{
    Flight,//飞行状态
    Victory//胜利状态
}

public class Player : MonoBehaviour
{
    /// <summary>
    /// 杯子id 0-3
    /// </summary>
    public int id { get { return int.Parse(gameObject.name); } }

    /// <summary>
    /// 杯子是否出场
    /// </summary>
    public bool IsOnTake;

    public NetWorkPlayerLogic WorkPlayerLogic { get { return GetComponent<NetWorkPlayerLogic>(); } }

    //小兵的阵营
    public colorCell mColorCell;

    public colorCell ColorCell
    {
        get { return mColorCell; }
        set { mColorCell = value; }
    }

    //当前小兵获得胜利
    private bool mVictory;
    public bool Victory
    {
        get { return mVictory; }
        set { mVictory = value; }
    }

    public void DoAtionCup(bool IsOn)
    {
        if (IsOn)
            EventListener.AddEventListenr(gameObject).OnClick = CupFunction;
        else
            EventListener.RemoveEventListenr(gameObject);
    }

    void CupFunction()
    {
        //此时杯子不可再次点击
        SceneGameController.Instance.StopSingCupMethod();
        if (!IsOnTake)
        {
            //选择完杯子,发送杯子移动的ID
            NetworkController.Instance.Send(new TakeOffRequest(id));
            EpheMeralActor.mPlayer = this;
            IsOnTake = true;
            return;
        }
        //选择完杯子
        NetworkController.Instance.Send(new MoveRequest(id));
        EpheMeralActor.mPlayer = this;
    }
}
