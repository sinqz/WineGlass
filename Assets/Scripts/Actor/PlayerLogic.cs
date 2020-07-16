using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerLogic : MonoBehaviour
{
    //当前玩家的初始位置
    public PathCell StarPos;

    //玩家到达的位置
    public PathCell NowStand { get; set; }

    //玩家的属性
    public Player m_Player { get { return GetComponent<Player>(); } }

    public int NumTake { get { return 6; } }//刚开始玩家摇到6才可以出棋

    private float moveSpeed = 1;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }//玩家的移动速度


    private float moveJump = 3;
    public float MoveJump { get { return moveJump; } set { moveJump = value; } }//玩家跳跃的高度

    private int num;
    public int Num { get { return num; } set { num = value; } }//移动的步数

    private int NumAir;//特殊步数


    private void Awake()
    {
        //初始化当前位置
        NowStand = StarPos;
    }

    /// <summary>
    /// 玩家移动脚本
    /// </summary>
    public void MoveNext()
    {
        if (PlayerInitStar())
            OnPlayerStartMove();
        else
            OnPlayerEndMove();
    }


    void OnPlayerEndMove()
    {
        StartCoroutine(IEOnEndMove());
    }


    //玩家起始
    public void OnPlayerStartMove()
    {
        if (num < NumTake)//是否可以起飞
        {
            //不可起飞，轮到下一名玩家
            PlayerManger.Instance.DelayTurnToNext();
            return;
        }
        MoveAnim(NowStand.NextCell);
        PlayerManger.Instance.DelayTurnToNext();
    }


    //玩家移动
    IEnumerator IEOnEndMove()
    {

        if (NowStand.NextCell == null)//如果当前玩家下一步格子为空，说明已经到达顶部
        {
            if (num < 1)//玩家登顶
            {
                Debug.Log("玩家胜利一颗棋子,获得一次投掷机会");
                NowStand.m_PlayerList.Remove(m_Player);
                m_Player.Victory = true;//当前小兵胜利
                yield break;
            }
            StartCoroutine(IERetreat());
            yield break;
        }
        if (num < 1)
        {
            yield break;
        }
        num--;//步数减一
        if (NowStand.CellType == m_Player.ColorCell && NowStand.ColorCell != null)//当前格子和玩家颜色相同，并且可以登顶的格子不为空，则开始登顶
            MoveAnim(NowStand.ColorCell);//转向
        else
            MoveAnim(NowStand.NextCell);//下一格移动
        yield return new WaitForSeconds(moveSpeed);
        yield return StartCoroutine(IEOnEndMove());
    }

    /// <summary>
    /// 到达顶部步数却没使用完毕，则向后退
    /// </summary>
    /// <returns></returns>
    IEnumerator IERetreat()
    {
        if (num >= 1)
        {
            num--;
            MoveAnim(NowStand.LastCell);//玩家向后退
            yield return new WaitForSeconds(moveSpeed);
            StartCoroutine(IERetreat());
            yield break;
        }
        yield break;
    }


    //走到了特殊位置的格子
    bool EMagicAllCell()
    {

        bool MagicNone = false;
        if (NowStand.CellType == m_Player.ColorCell)//格子颜色相同
        {
            switch (NowStand.MagicCell)
            {
                case MagicCell.Airport:
                    NumAir = 2;//特殊格子移动两步停下
                    StartCoroutine(EndAllColorCell());
                    MagicNone = true;
                    break;
                case MagicCell.Jump:
                    NumAir = 1;//跳跃格子跳一次
                    StartCoroutine(EndAllColorCell());//跳一次
                    MagicNone = true;
                    break;
            }
        }
        Debug.Log("走到了特殊格子");
        Debug.Log("NumAir" + NumAir);
        return MagicNone;
    }


    //玩家最终停留位置是否可以飞行(包含特殊格子处理)
    IEnumerator EndAllColorCell()
    {
        if (NumAir >= 1)
        {
            yield return new WaitForSeconds(moveSpeed);
            NumAir--;
            MagicAnim(NowStand.JumpCell);
            StartCoroutine(EndAllColorCell());
        }
        yield break;
    }


    //玩家向指定格子移动
    public virtual void MoveAnim(PathCell NowStandCell)
    {
        Debug.Log("Num:" + num);

        if (NowStand.m_PlayerList.Contains(m_Player))//安全判断
            NowStand.m_PlayerList.Remove(m_Player);//先移除当前路径本玩家
        PathCell m_Cell = NowStandCell;
        if (m_Cell != null)
        {
            NowStand = m_Cell;
            Vector3 dir = (m_Cell.transform.position - transform.position);
            Vector3 dirMove = dir + transform.position;
            transform.DOJump(dirMove, MoveJump, 1, 1);
            if (num < 1)
            {
                if (EMagicAllCell())
                    return;
                Debug.Log("当前位置停留MoveAnim");
                NowStand.m_PlayerList.Add(m_Player);//新停留位置添加此元素
                NowStand.NowStandAll();
                PlayerManger.Instance.DelayTurnToNext();
            }
        }
    }


    //特殊移动处理
    void MagicAnim(PathCell NowStandCell)
    {
        if (NowStand.m_PlayerList.Contains(m_Player))//安全判断
            NowStand.m_PlayerList.Remove(m_Player);//先移除当前路径本玩家
        PathCell m_Cell = NowStandCell;
        if (m_Cell != null)
        {
            NowStand = m_Cell;
            Vector3 dir = (m_Cell.transform.position - transform.position);
            Vector3 dirMove = dir + transform.position;
            transform.DOJump(dirMove, MoveJump, 1, 1);
            if (NumAir < 1)
            {
                Debug.Log("当前位置停留MagicAnim");
                NowStand.m_PlayerList.Add(m_Player);//新停留位置添加此元素
                NowStand.NowStandAll();
                PlayerManger.Instance.DelayTurnToNext();
            }
        }

    }

    /// <summary>
    /// 玩家直接移动到设定的位置
    /// </summary>
    public PathCell MoveSetPosition(int Number)
    {
        PathCell cell = NowStand;
        for (int i = 0; i < Number; i++)
        {
            if (cell.NextCell != null)
                cell = cell.NextCell;
        }
        return cell;
    }



    //玩家回到初始位置的方法
    public void InitStartPosition()
    {
        StartCoroutine(IEInitStartPosition());
    }

    IEnumerator IEInitStartPosition()
    {
        yield return new WaitForSeconds(moveSpeed);
        MoveAnim(StarPos);
    }


    /// <summary>
    /// 判断当前位置是否为初始位置
    /// </summary>
    /// <returns></returns>
    public bool PlayerInitStar()
    {
        return NowStand == StarPos ? true : false;
    }

}
