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


    private PlayerInfo playerInfo { get { return GetComponent<PlayerInfo>(); } }

    private Button clickRote;

    /// <summary>
    /// 机器人移动自动处理
    /// </summary>
    /// <param name="m_Player">当前玩家的所有小弟</param>
    /// <param name="Num">摇到的骰子数</param>
    public Player AIRobotRating(List<Player> m_Player)
    {
        List<Player> PlayerList = new List<Player>();//存储不在初始位置的玩家
        if (playerInfo.OnPositionAllStart())//假如玩家飞机全在一个位置，随机派出一架
            return m_Player[Random.Range(0, playerInfo.m_Player.Count)];//测试，先随机一个飞机
        else
        {
            for (int i = 0; i < m_Player.Count; i++)//遍历所有玩家
            {
                if (playerInfo.Number == 6)
                {
                    if (m_Player[i].WorkPlayerLogic.PlayerInitStar())//在初始位置的玩家
                        return m_Player[i];
                    else
                        PlayerList.Add(m_Player[i]);//添加到集合方便分开处理
                }
                else
                {
                    if (!m_Player[i].WorkPlayerLogic.PlayerInitStar())//不在初始位置的玩家
                        PlayerList.Add(m_Player[i]);//添加到集合方便分开处理
                }
            }
        }
        PathCell Cell;
        Player mPlayer;
        for (int k = 0; k < PlayerList.Count; k++)
        {
            Cell = PlayerList[k].WorkPlayerLogic.MoveSetPosition(playerInfo.Number);//得到移动之后停留的位置
            /*权重比较*/
            if (Cell.CellType == PlayerList[k].ColorCell)
            {
                if (Cell.MagicCell == MagicCell.Airport)
                {
                    mPlayer = PlayerList[k];
                    PlayerList.Clear();
                    return mPlayer;
                }
                else
                {
                    mPlayer = PlayerList[k];
                    PlayerList.Clear();
                    return mPlayer;
                }

            }
            else
            {
                if (Cell.m_PlayerList.Count != 0)
                {
                    mPlayer = PlayerList[k];
                    PlayerList.Clear();
                    return mPlayer;
                }
            }
        }
        mPlayer = PlayerList[PlayerList .Count- 1];
        PlayerList.Clear();
        return mPlayer;
    }


    PathCell FinalPosition(PathCell NowStandCell, int Num)
    {
        PathCell NowStands = null;

        while (Num <= 1)
        {

            PathCell m_Cell = NowStandCell;
            NowStands = m_Cell;//得到最终位置
            Num--;
            if (NowStandCell == null)
                return NowStands;
        }
        return NowStands;

    }


    private void Awake()
    {
        //clickRote = GameObject.FindWithTag("ClickRote").GetComponent<Button>();
    }


    IEnumerator IEMoveNext()
    {
        yield return new WaitForSeconds(3f);
        if (PlayerManger.Instance.SixPlayerNext())
        {
            //全部打回初始位置
            Debug.Log("摇到三次六");
            playerInfo.OnPlayerAllStar();
            PlayerManger.Instance.DelayTurnToNext();
            yield break;
        }
        playerInfo.Move(AIRobotRating(playerInfo.m_Player));
    }

    public void MoveNext()
    {
        if (playerInfo.AllVictory())
            return;
        MainPanlUI.Instance.OnEnterDice(false);
        clickRote.onClick.Invoke();
        StartCoroutine(IEMoveNext());
    }

}
