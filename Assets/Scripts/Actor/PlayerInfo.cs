using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

//玩家
public class PlayerInfo : BaseObject
{
    [Header("玩家的棋子")]
    //玩家的棋子
    public List<Player> m_Player;









































































































    public int Number { get { return PlayerManger.NumMobile; } }

    public bool IsRun { get; set; }//是否允许移动

    private string[] Chess = new string[] { "PlayR", "PlayG", "PlayB", "PlayY" };

    //角色类型，区分玩家和AI
    public RoleType mRoleType;
    public RoleType RoleType
    {
        get { return mRoleType; }
        set { mRoleType = value; }
    }

    private void Awake()
    {
        if (mRoleType == RoleType.AI)
            gameObject.AddComponent<AIInfo>();
    }

    /// <summary>
    /// 玩家全部被打回到初始位置
    /// </summary>
    public void OnPlayerAllStar()
    {
        for (int i = 0; i < m_Player.Count; i++)
        {
            if (!m_Player[i].WorkPlayerLogic.PlayerInitStar())
                m_Player[i].WorkPlayerLogic.InitStartPosition();
        }
    }

    /// <summary>
    /// 判断玩家的棋子是否全在起始
    /// </summary>
    /// <returns></returns>
    public bool OnPositionAllStart()
    {
        for (int i = 0; i < m_Player.Count; i++)
        {
            if (!m_Player[i].WorkPlayerLogic.PlayerInitStar()) { return false; }
        }
        return true;
    }

    public void PlayNext()//执行行动
    {
        if (RoleType == RoleType.AI)//判断当前玩家是否为AI
            GetComponent<AIInfo>().MoveNext();//执行AI的运动逻辑
        else//打开玩家的摇骰子面板，玩家自行处理
            MainPanlUI.Instance.OnEnterDice(true);
    }

    void Update()
    {

        if (IsRun)
        {

            if (AllVictory())
            {
                IsRun = false;
                return;
            }
            if (RoleType != RoleType.AI)
            {
                if (PlayerManger.Instance.SixPlayerNext())
                {
                    //全部打回初始位置
                    OnPlayerAllStar();
                    IsRun = false;
                    PlayerManger.Instance.DelayTurnToNext();
                    return;
                }
                MainPanlUI.Instance.OnEnterDice(false);//关闭骰子面板
                if (PlayerManger.NumMobile != 6 && OnPositionAllStart())//如果玩家摇到的骰子不为6且此时玩家的所有骰子都在原地则直接跳到下个玩家
                {
                    IsRun = false;
                    PlayerManger.Instance.DelayTurnToNext();
                    return;
                }
                OnChess();//摇完骰子开启摄线检测，选择人物移动
                IsRun = false;
            }
            else
            {
                IsRun = false;
            }
        }
    }

    //人机交互
    IEnumerator ClickChess()
    {
        Player m_Player = null;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // bool isColler = Physics.Raycast(ray, out hit, LayerMask.GetMask(Chess[PlayerManger.Instance.movePost]));//匹配模式开启
                bool isColler = Physics.Raycast(ray, out hit, LayerMask.GetMask(Chess[1]));
                if (isColler)
                {
                    m_Player = hit.collider.gameObject.GetComponent<Player>();
                    if (m_Player != null)
                    {
                        Move(m_Player);
                        break;
                    }
                }
            }
            yield return null;
        }

    }

    public void OnChess()
    {
        StartCoroutine(ClickChess());
    }


    /// <summary>
    ///判断当前玩家已无飞机(包含游戏结束处理)
    /// </summary>
    /// <returns></returns>
    public bool AllVictory()
    {
        if (m_Player.Count == 0)
        {
            //游戏结束逻辑
            //GameController.Instance.GameOver(this);
            return true;
        }
        for (int i = 0; i < m_Player.Count; i++)
        {
            if (m_Player[i].Victory)
            {
                Destroy(m_Player[i].gameObject);
                m_Player.Remove(m_Player[i]);
            }
        }
        return false;
    }

    /// <summary>
    /// 设置托管
    /// </summary>
    /// <param name="mRoleType"></param>
    public void SetRoleType(RoleType mRoleType)
    {
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

    /// <summary>
    /// 移动脚本
    /// </summary>
    /// <param name="mPlayer">移动的玩家</param>
    /// <param name="MoveNumber">移动的步数</param>
    public void Move(Player mPlayer)
    {
        mPlayer.WorkPlayerLogic.Num = Number;
        mPlayer.WorkPlayerLogic.MoveNext();
    }
    /// <summary>
    /// 移动脚本重载
    /// </summary>
    /// <param name="mPlayer">移动的玩家</param>
    /// <param name="MoveNumber">移动的步数</param>
    public void Move(Player mPlayer, float moveSpeed, float jumpSpeed)
    {
        mPlayer.WorkPlayerLogic.Num = Number;
        mPlayer.WorkPlayerLogic.MoveJump = jumpSpeed;
        mPlayer.WorkPlayerLogic.MoveSpeed = moveSpeed;
        mPlayer.WorkPlayerLogic.MoveNext();
    }
}
