using EZFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家管理器
public class PlayerManger : MonoSingletonTemplateScript<PlayerManger>
{

    public PlayerInfo[] m_PlayerInfo;//场景中的四名玩家

    public PlayerInfo tempPlayer { get; set; }//存储当前行动中的玩家信息

    public int NumPlay { get { return Random.Range(0, 4); } }//随机一名玩家

    public int movePost { get; set; }//当前行动中的玩家在PlayerInfo中的下标

    public static int NumMobile { get; set; }//允许当前行动玩家移动的步数

    private int SixNum;//摇到六的次数

    private void Start()
    {
        StartCoroutine(InItGame());
    }


    IEnumerator InItGame()
    {
        yield return new WaitForSeconds(3f);
        tempPlayer = m_PlayerInfo[Random.Range(0, m_PlayerInfo.Length)];//开局随机一名玩家
        tempPlayer.PlayNext();
    }



    /// <summary>
    /// 轮到下名玩家行动
    /// </summary>
    public void DelayTurnToNext()
    {
        StartCoroutine(TurnToNext());
    }

    IEnumerator TurnToNext()
    {
        //2秒后轮到下个玩家移动
        yield return new WaitForSeconds(3f);
        movePost = (movePost + 1) % m_PlayerInfo.Length;
        tempPlayer = m_PlayerInfo[movePost];//获得行动的玩家
        tempPlayer.PlayNext();
    }

    //玩家是否三次摇到六方法
    public bool SixPlayerNext()
    {
        if (tempPlayer == m_PlayerInfo[movePost] && NumMobile == 6)
        {
            SixNum++;//次数+=1
            if (SixNum > 2)
            {
                SixNum = 0;
                return true;
            }
            else
            {
                movePost--;//玩家继续是自己
                return false;
            }
        }
        SixNum = 0;
        return false;
    }


}
