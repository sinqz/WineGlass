using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

//玩家
public class PlayerInfo : MonoBehaviour
{
    [Header("玩家的棋子")]
    //玩家的棋子
    public List<Player> m_Player;

    //角色类型，区分玩家和AI
    private RoleType mRoleType;
    public RoleType RoleType
    {
        get { return mRoleType; }
        set { mRoleType = value; }
    }


    public GameUser User { get; set; }

    /// <summary>
    /// 玩家摇到6的次数
    /// </summary>
    public int SixCount { get; set; }



    private void Start()
    {
        if (mRoleType == RoleType.AI)
            gameObject.AddComponent<AIInfo>();
    }

    /// <summary>
    /// 单机在线模式通用方法玩家全部被打回到初始位置
    /// </summary>
    public void StandPlayerAllStar()
    {
        for (int i = 0; i < m_Player.Count; i++)
        {
            m_Player[i].mPlayerLogic.InItMoveStart();
        }
    }

    /// <summary>
    /// 判断玩家的棋子是否全在起始
    /// </summary>
    /// <returns></returns>
    public bool StandPositionAllStart()
    {
        for (int i = 0; i < m_Player.Count; i++)
        {
            if (!m_Player[i].mPlayerLogic.StandInItPos()) { return false; }
        }
        return true;
    }

    /// <summary>
    /// 执行移动方法
    /// </summary>
    public void StandPlayNext()//执行行动
    {
        if (EpheMeralActor.GameOver)
        { Debug.Log("StandPlayNext结束!"); return; }
        if (RoleType == RoleType.AI)//判断当前玩家是否为AI
            GetComponent<AIInfo>().StandAIMoveNext();//执行AI的运动逻辑
        else//打开玩家的骰子按钮,并注册点击事件
            StartCoroutine(StandDiceOpen());
    }

    /// <summary>
    /// 打开摇骰子方法
    /// </summary>
    IEnumerator StandDiceOpen()
    {
        Debug.Log("StandDiceOpen");
        EpheMeralActor.TrusteeAction.Enqueue(() => //摇骰子方法保存起来,用户点击时使用
        {
            SceneGameController.Instance.UIGameControl.SingleDice.onClick.Invoke();//自动摇骰子

        });
        yield return new WaitForSeconds(1.3f);
        SceneGameController.Instance.UIGameControl.SingleDice.gameObject.SetActive(true);
    }

    /// <summary>
    /// 单机模式杯子事件(骰子点击事件)
    /// </summary>
    /// <param name="diceCount"></param>
    public void StandDotionCup()
    {
        if (EpheMeralActor.StandDiceCount >= 5)
        {
            if (EpheMeralActor.StandDiceCount == 6)
            {
                ++SixCount;
                if (SixCount >= 3)
                {
                    SixCount = 0;
                    StandPlayerAllStar();
                    SceneGameController.Instance.UIGameControl.StandTakeToMove(); //直接下一名玩家
                    return;
                }
                --EpheMeralActor.playCount;//获得再次摇骰子的机会
                StandOpenDoctionCup();
                Debug.Log("获得再次摇骰子的机会");
                return;
            }
            StandOpenDoctionCup();
        }
        else
        {
            if (StandPositionAllStart())
                SceneGameController.Instance.UIGameControl.StandTakeToMove(); //直接下一名玩家
            else
                StandOpenDoctionCup();
        }
        SixCount = 0;
    }

    /// <summary>
    /// 单机模式打开杯子点击事件
    /// </summary>
    void StandOpenDoctionCup()
    {
        for (int i = 0; i < m_Player.Count; i++)
            m_Player[i].StandDoAtionCup(true);
        EpheMeralActor.TrusteeAction.Clear();
        EpheMeralActor.TrusteeAction.Enqueue(() =>
        {
            StandCloseDoctionCup();
            GetComponent<AIInfo>().AIRobotRating().mPlayerLogic.StandMoveNext();
            Debug.Log("执行了杯子移动的方法");
        });
    }

    /// <summary>
    /// 单机模式关闭骰子点击事件
    /// </summary>
    public void StandCloseDoctionCup()
    {
        for (int i = 0; i < m_Player.Count; i++)
        {
            m_Player[i].StandDoAtionCup(false);
        }
    }


    /// <summary>
    ///判断当前玩家已无飞机(包含游戏结束处理)
    /// </summary>
    /// <returns></returns>
    public void AllVictory()
    {
        for (int i = 0; i < m_Player.Count; i++)
        {
            if (m_Player[i].Victory)
            {
                Debug.Log("胜利一颗棋子:" + i);
                Destroy(m_Player[i].gameObject);
                m_Player.Remove(m_Player[i]);
            }
        }
        if (m_Player.Count == 0)
        {
            Debug.Log("游戏结束");
            EpheMeralActor.GameOver = true;
        }
    }

    /// <summary>
    /// 设置托管
    /// </summary>
    /// <param name="mRoleType"></param>
    public void SetRoleType(RoleType mRoleType)
    {
        RoleType = mRoleType;
        switch (mRoleType)
        {
            case RoleType.PLAYER:
                if (GetComponent<AIInfo>() != null)
                    Destroy(GetComponent<AIInfo>());
                break;
            case RoleType.AI:
                if (GetComponent<AIInfo>() == null)
                    gameObject.AddComponent<AIInfo>();
                break;
        }
    }

}
