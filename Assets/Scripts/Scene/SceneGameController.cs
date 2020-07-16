using LitJson;
using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏场景专属控制器
public class SceneGameController : MonoSingletonTemplateScript<SceneGameController>, IEventListener
{
    public UIGameController UIGameControl;

    public ActorPathAction MapActor;
    //玩家脚本
    public PlayerInfo[] PlayerScript;

    protected override void Awake()
    {
        base.Awake();
        MapActor.OnLoad();
        UIGameControl.OnLoad();
    }
    private void Start()
    {
        //将当前对象添加到监听者对象
        EventController.Instance.AddListener(this);
    }

    /// <summary>
    /// 选择移动杯子的方法
    /// </summary>
    public void SingCupMethod()
    {
        for (int i = 0; i < PlayerScript.Length; i++)
        {
            if (PlayerScript[i].User.id == Actor.uid)
            {
                for (int k = 0; k < PlayerScript[i].m_Player.Count; k++)
                {
                    //给当前要移动的杯子注册点击事件点击事件
                    PlayerScript[i].m_Player[k].DoAtionCup(true);
                }
            }
        }

    }

    /// <summary>
    /// 杯子不可点击的方法
    /// </summary>
    public void StopSingCupMethod()
    {
        for (int i = 0; i < PlayerScript.Length; i++)
        {
            if (PlayerScript[i].User.id == Actor.uid)
            {
                for (int k = 0; k < PlayerScript[i].m_Player.Count; k++)
                {
                    PlayerScript[i].m_Player[k].DoAtionCup(false);
                }
            }
        }

    }

    /// <summary>
    /// 玩家杯子出场的方法
    /// </summary>
    public void TakeSingCupMethod(Hashtable result)
    {
        int id = int.Parse(result[APIS.id].ToString());
        string userId = result[APIS.userId].ToString();
        for (int i = 0; i < PlayerScript.Length; i++)
        {
            if (userId == PlayerScript[i].User.id)
                PlayerScript[i].m_Player[id].WorkPlayerLogic.TaskNextCup();
        }
    }

    /// <summary>
    /// 杯子移动的方法
    /// </summary>
    /// <param name="result"></param>
    public void MoveCupMethod(Hashtable result)
    {
        int id = int.Parse(result[APIS.id].ToString());
        string userId = result[APIS.userId].ToString();
        JsonData data = JsonMapper.ToObject(result.toJson());
        string[] pos = new string[data[APIS.pos].Count];
        for (int i = 0; i < data[APIS.pos].Count; i++)
        {
            pos[i] = data[APIS.pos][i].ToString();
        }
        for (int i = 0; i < PlayerScript.Length; i++)
        {
            if (userId == PlayerScript[i].User.id)
                PlayerScript[i].m_Player[id].WorkPlayerLogic.MoveNextCup(pos);
        }

    }


    public void OnEventTrigger(Event _Event)
    {
        switch (_Event.EventID)
        {
            case EventID.DoAction://给杯子添加点击事件
                SingCupMethod();
                break;
            case EventID.TakeCup://杯子出场事件通报(包含自己也同步出场)
                TakeSingCupMethod((Hashtable)_Event.EventObj);
                break;
            case EventID.MoveCup://杯子移动事件(包含自己也同步移动)
                MoveCupMethod((Hashtable)_Event.EventObj);
                break;
            case EventID.PlayTurn://扔骰子事件
                Hashtable result = _Event.EventObj as Hashtable;
                UIGameControl.SingleDiceMethod(result[APIS.userId].ToString());
                break;
        }
    }
}
