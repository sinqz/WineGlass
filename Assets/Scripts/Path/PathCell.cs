using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//路径标识枚举
public enum colorCell
{
    BULE = 0,
    YELLOW = 1,
    RAD = 2,
    GREEN = 3,
}


//特殊路径
public enum MagicCell
{
    Jump,
    Airport = 2,//飞机场可以飞两步
}


public class PathCell : MonoBehaviour
{
    [Header("当前路段标识")]
    public colorCell CellType;

    [Header("下一步的格子")]
    public PathCell NextCell;

    [Header("上一步的格子")]
    public PathCell LastCell;

    [Header("登顶的格子")]
    public PathCell ColorCell;

    [Header("当前可以跳跃的格子")]
    public PathCell JumpCell;

    [Header("特殊格子")]
    public MagicCell MagicCell;

    [Header("当前格子上存在的玩家数量")]
    public List<Player> m_PlayerList = new List<Player>();

    public RectTransform rect { get { return GetComponent<RectTransform>(); } }

    //格子自己的初始化方法
    public void Init()
    {
        int Num = int.Parse(gameObject.name);
        CellType = (colorCell)(Num % 3);
    }

    /// <summary>
    /// 同一路径多个玩家处理方法
    /// </summary>
    public bool NowStandAll()
    {
        //先判断当前路径敌人的数量
        if (m_PlayerList.Count <= 1)
            return false;
        for (int i = m_PlayerList.Count - 2; i >= 0; i--)
        {
            //判断最后一个玩家和当前路径的所有玩家是否为同一阵营,并且最后一名玩家的阵营和当前路径的阵营不同（避免跳子的时候路过此路径把当前路径上的其他玩家击飞）
            if (m_PlayerList[m_PlayerList.Count - 1].ColorCell != m_PlayerList[i].ColorCell)
                m_PlayerList[i].WorkPlayerLogic.InitStartPosition();
            //不是同一阵营的玩家，则让其他玩家回到初始位置（此处添加玩家道具逻辑，可以不用返回到起始位置）
        }
        return true;
    }

}
