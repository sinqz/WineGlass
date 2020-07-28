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
    public bool IsOnTake { get; set; }

    public PlayerLogic mPlayerLogic { get { return GetComponent<PlayerLogic>(); } }

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

    /// <summary>
    /// 杯子点击事件绑定和解除
    /// </summary>
    /// <param name="IsOn"></param>
    public void SeverDoAtionCup(bool IsOn)
    {
        if (IsOn)
            EventListener.AddEventListenr(gameObject).OnClick = ServerCupFunction;
        else
            EventListener.RemoveEventListenr(gameObject);
    }

    void ServerCupFunction()
    {
        //此时杯子不可再次点击
        SceneGameController.Instance.StopCupMethod();
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


    /// <summary>
    /// 单机模式杯子点击事件
    /// </summary>
    public void StandDoAtionCup(bool IsOn)
    {
        if (IsOn)
            EventListener.AddEventListenr(gameObject).OnClick = StandCupFunction;
        else
            EventListener.RemoveEventListenr(gameObject);
    }

    /// <summary>
    /// 单机模式杯子事件
    /// </summary>
    void StandCupFunction()
    {
        SceneGameController.Instance.UIGameControl.infoPanel[0].Info.StandCloseDoctionCup();
        mPlayerLogic.StandMoveNext();
    }
}
