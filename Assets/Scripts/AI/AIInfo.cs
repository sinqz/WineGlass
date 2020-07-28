using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class AIInfo : MonoBehaviour
{
    /*机器人或者托管实现规则
     * 尽量让目的地是与自己颜色一致的格子
     * 在目的地是与自己颜色一致的时候，选择能跨越飞行的，这样能多飞行一次
     * 尽量让目的地撞击敌机
     * 如果目的地有多架敌机，则另外考虑
     * 如果在场只有两架以下的飞机，尽量让飞机从机场起飞
     * 尽量避开敌机跨越飞行的格子
     * 与敌机很近的时候，尽量保持在敌机后边
     * 与敌机很近并且在前边的时候，尽量不要走在敌机的颜色的格子上
     * 与敌机很近并且在前边的时候，如果能一下走得很远，可以选择
     * 根据以上情况给每架敌机评分，分数高的走棋
     */

    private PlayerInfo mPlayerInfo { get { return GetComponent<PlayerInfo>(); } }
    private List<Player> PlayerList = new List<Player>();//存储不在初始位置的玩家
    private List<Player> StartPlayer = new List<Player>();//存储在初始位置的玩家
    private int SixCount { get { return mPlayerInfo.SixCount; } set { mPlayerInfo.SixCount = value; } }
    /// <summary>
    /// 机器人移动自动处理
    /// </summary>
    /// <param name="m_Player">当前玩家的所有小弟</param>
    /// <param name="Num">摇到的骰子数</param>
    public Player AIRobotRating()
    {
        PlayerList.Clear();
        StartPlayer.Clear();//初始化
        if (mPlayerInfo.StandPositionAllStart())//假如玩家飞机全在起始位置，随机派出
            return mPlayerInfo.m_Player[Random.Range(0, mPlayerInfo.m_Player.Count)];
        else
        {
            for (int i = 0; i < mPlayerInfo.m_Player.Count; i++)//遍历所有玩家
            {

                if (!mPlayerInfo.m_Player[i].mPlayerLogic.StandInItPos())//不在初始位置的玩家
                    PlayerList.Add(mPlayerInfo.m_Player[i]);//添加到集合方便分开处理
                else
                    StartPlayer.Add(mPlayerInfo.m_Player[i]);
            }
        }
        PathCell Cell;
        for (int k = 0; k < PlayerList.Count; k++)
        {
            Cell = PlayerList[k].mPlayerLogic.AIMoveSetPosition();//得到移动之后停留的位置
            if (Cell.NextCell == null)//到达终点,优先移动
                return PlayerList[k];
            /*权重比较*/
            if (Cell.CellType == PlayerList[k].ColorCell)//可以跳跃的位置,优先移动
            {
                if (Cell.MagicCell == MagicCell.Airport)
                    return PlayerList[k];
                else
                    return PlayerList[k];
            }
            else
            {
                if (Cell.m_PlayerList.Count != 0)//判断到达的位置是否有其他玩家,撞子
                    return PlayerList[k];
            }
        }
        for (int i = 0; i < StartPlayer.Count; i++)
        {
            if (EpheMeralActor.StandDiceCount >= 5)
                return StartPlayer[i];//上述条件都不满足,摇到大于等于5的点数则优先走未出场的杯子
        }
        return PlayerList[PlayerList.Count - 1];
    }

    IEnumerator IEMoveNext()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("AIInfo");
        EpheMeralActor.StandDiceCount = Random.Range(1, 7);//随机摇骰子
        Debug.Log("骰子点数:" + EpheMeralActor.StandDiceCount);
        //执行骰子动画
        int Count = EpheMeralActor.StandDiceCount;
        yield return new WaitForSeconds(2f);
        if (Count >= 5)
        {
            if (Count == 6)
            {
                ++SixCount;
                if (SixCount >= 3)
                {
                    SixCount = 0;
                    mPlayerInfo.StandPlayerAllStar();
                    SceneGameController.Instance.UIGameControl.StandTakeToMove(); //直接下一名玩家
                    yield break;
                }
                --EpheMeralActor.playCount;
                AIRobotRating().mPlayerLogic.StandMoveNext();
                Debug.Log("获得再次摇骰子的机会");
                yield break;
            }
            AIRobotRating().mPlayerLogic.StandMoveNext();
        }
        else
        {
            if (mPlayerInfo.StandPositionAllStart())
                SceneGameController.Instance.UIGameControl.StandTakeToMove(); //直接下一名玩家
            else
                AIRobotRating().mPlayerLogic.StandMoveNext();
        }
        SixCount = 0;
    }

    public void StandAIMoveNext()
    {
        StartCoroutine(IEMoveNext());
    }
}
