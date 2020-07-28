using System.Collections;
using UnityEngine;
using DG.Tweening;
using NetWorkAndData.APIS;
/// <summary>
/// 玩家移动脚本,包含联网和单机
/// </summary>
public sealed class PlayerLogic : MonoBehaviour
{
    //当前玩家的初始位置
    public PathCell StarPos;
    //玩家到达的位置
    public PathCell NowStand { get; set; }
    public float MoveSpeed { get; set; } = 0.5f; //玩家的移动速度
    public float MoveTime { get; set; } = 0.6f;//移动的时间间隔
    public float MoveJump { get; set; } = 50;//玩家跳跃的高度
    private int NumAir;//特殊步数
    private bool IsOn;//是否到达过顶部,此判断是为了让到达顶部后退的杯子不能在跳跃

    private void Awake()
    {
        //初始化当前位置
        NowStand = StarPos;
    }

    /// <summary>
    /// 单机模式判断玩家位置是否都在起始位置
    /// </summary>
    /// <returns></returns>
    public bool StandInItPos()
    {
        return NowStand == StarPos ? true : false;
    }

    /// <summary>
    /// 单机模式移动脚本
    /// </summary>
    public void StandMoveNext()
    {
        EpheMeralActor.TrusteeAction.Clear();
        if (StandInItPos())
            StartCoroutine(StandOpenMove()); //杯子出场,清空步数
        else
            StartCoroutine(StandEndMove());
    }

    IEnumerator StandOpenMove()
    {
        if (EpheMeralActor.StandDiceCount < 5)
        {
            EpheMeralActor.StandDiceCount = 0;
            SceneGameController.Instance.UIGameControl.StandTakeToMove();
            yield break;
        }
        EpheMeralActor.StandDiceCount = 0;
        StandMoveAnim(NowStand.NextCell);
    }

    /// <summary>
    /// 单机模式玩家移动
    /// </summary>
    /// <returns></returns>
    IEnumerator StandEndMove()
    {
        if (NowStand.NextCell == null)//如果当前玩家下一步格子为空，说明已经到达顶部
        {
            if (EpheMeralActor.StandDiceCount < 1)//玩家登顶
            {
                GetComponent<Player>().Victory = true;//当前小兵胜利
                SceneGameController.Instance.UIGameControl.infoPanel[EpheMeralActor.playCount].Info.AllVictory();
                NowStand.m_PlayerList.Remove(GetComponent<Player>());
                Debug.Log("玩家胜利一颗棋子");
                yield break;
            }
            StartCoroutine(StandIERetreat());
            IsOn = true;
            yield break;
        }
        if (EpheMeralActor.StandDiceCount < 1)
            yield break;
        --EpheMeralActor.StandDiceCount;//步数减一
        if (NowStand.CellType == GetComponent<Player>().ColorCell && NowStand.ColorCell != null)//当前格子和玩家颜色相同，并且可以登顶的格子不为空，则开始登顶
            StandMoveAnim(NowStand.ColorCell);//转向
        else
            StandMoveAnim(NowStand.NextCell);//下一格移动
        yield return new WaitForSeconds(MoveTime);
        StartCoroutine(StandEndMove());
    }

    /// <summary>
    /// 到达顶部步数却没使用完毕，则向后退
    /// </summary>
    /// <returns></returns>
    IEnumerator StandIERetreat()
    {
        if (EpheMeralActor.StandDiceCount >= 1)
        {
            EpheMeralActor.StandDiceCount--;
            StandMoveAnim(NowStand.LastCell);//玩家向后退
            yield return new WaitForSeconds(MoveTime);
            StartCoroutine(StandIERetreat());
            yield break;
        }
        yield break;
    }

    /// <summary>
    /// 走到了特殊位置的格子
    /// </summary>
    /// <returns></returns>
    bool StandEMagicAllCell()
    {
        if (NowStand.CellType == GetComponent<Player>().ColorCell && !IsOn)//玩家颜色和格子颜色相同,并且没登过顶
        {
            switch (NowStand.MagicCell)
            {
                case MagicCell.Airport:
                    NumAir = 2;//特殊格子移动两步停下
                    StartCoroutine(StandEndAllColorCell());
                    return true;
                case MagicCell.Jump:
                    NumAir = 1;//跳跃格子跳一次
                    StartCoroutine(StandEndAllColorCell());//跳一次
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 玩家最终停留位置是否可以飞行(包含特殊格子处理)
    /// </summary>
    /// <returns></returns>
    IEnumerator StandEndAllColorCell()
    {
        if (NumAir >= 1)
        {
            yield return new WaitForSeconds(MoveTime);
            NumAir--;
            StandMagicAnim(NowStand.JumpCell);
            StartCoroutine(StandEndAllColorCell());
        }
        yield break;
    }

    /// <summary>
    /// 单机模式玩家向指定格子移动
    /// </summary>
    /// <param name="NowStandCell"></param>
    void StandMoveAnim(PathCell NowStandCell)
    {
        Debug.Log("玩家剩余步数:" + EpheMeralActor.StandDiceCount);

        if (NowStand.m_PlayerList.Contains(GetComponent<Player>()))//安全判断
            NowStand.m_PlayerList.Remove(GetComponent<Player>());//先移除当前路径本玩家
        if (NowStandCell != null)
        {
            NowStand = NowStandCell;
            GetComponent<ImageOverturn>().FlipHorizontal = NowStand.Xturn;
            transform.DOLocalJump(NowStandCell.rect.anchoredPosition3D, MoveJump, 1, MoveSpeed);
            if (NowStand == StarPos)//如果是被打回到初始位置,则不需要添加停留位置
                return;
            if (EpheMeralActor.StandDiceCount < 1)
            {
                if (StandEMagicAllCell())
                    return;
                NowStand.m_PlayerList.Add(GetComponent<Player>());//新停留位置添加此元素
                NowStand.NowStandAll();
                SceneGameController.Instance.UIGameControl.StandTakeToMove();
            }
        }
    }

    /// <summary>
    /// 单机模式特殊格子移动动画处理
    /// </summary>
    /// <param name="NowStandCell"></param>
    void StandMagicAnim(PathCell NowStandCell)
    {
        if (NowStand.m_PlayerList.Contains(GetComponent<Player>()))//安全判断
            NowStand.m_PlayerList.Remove(GetComponent<Player>());//先移除当前路径本玩家
        if (NowStandCell != null)
        {
            NowStand = NowStandCell;
            GetComponent<ImageOverturn>().FlipHorizontal = NowStand.Xturn;
            transform.DOLocalJump(NowStandCell.rect.anchoredPosition3D, MoveJump, 1, MoveSpeed);
            if (NumAir < 1)
            {
                NowStand.m_PlayerList.Add(GetComponent<Player>());//新停留位置添加此元素
                NowStand.NowStandAll();
                SceneGameController.Instance.UIGameControl.StandTakeToMove();
            }
        }

    }


    /// <summary>
    /// AI逻辑,给定点数,求出最终位置
    /// </summary>
    public PathCell AIMoveSetPosition()
    {
        PathCell cell = NowStand;
        for (int i = 0; i < EpheMeralActor.StandDiceCount; i++)
        {
            if (cell.NextCell != null)
                cell = cell.NextCell;
        }
        return cell;
    }


    /// <summary>
    /// 玩家被打回到初始位置的方法
    /// </summary>
    public void InItMoveStart()
    {
        if (!StandInItPos())
            StandMoveAnim(StarPos);
    }


    /// <summary>
    /// 杯子移动
    /// </summary>
    public void MoveNextCup(string[] pos)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Debug.Log(pos[i]);
            for (int k = 0; k < ActorPathAction.Instance.ActorDic.Count; k++)
            {
                if (pos[i] == ActorPathAction.Instance.ActorDic[k].name)
                {
                    StandMoveAnim(ActorPathAction.Instance.ActorDic[k]);
                    //EpheMeralActor.Actions.Enqueue(() => { MoveAnim(ActorPathAction.Instance.ActorDic[k]); });
                }
            }
        }
        //StartCoroutine(LoadQueue());
    }

    IEnumerator LoadQueue()
    {
        while (EpheMeralActor.Actions.Count != 0 && EpheMeralActor.Actions != null)
        {
            EpheMeralActor.Actions.Dequeue()?.Invoke();
            yield return new WaitForSeconds(1f);
        }
    }


    /// <summary>
    /// 杯子出场
    /// </summary>
    public void ServerTaskNextCup()
    {
        Debug.Log("杯子出场");
        ServerMoveAnim(StarPos.NextCell);
    }

    public void ServerMoveAnim(PathCell NowStandCell)
    {
        NowStand = NowStandCell;
        if (NowStand != null)
        {
            Vector3 dirMove = NowStand.rect.anchoredPosition3D;
            transform.DOLocalJump(dirMove, MoveJump, 1, MoveSpeed);
        }
    }
}
