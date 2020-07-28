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

    protected override void Awake()
    {
        base.Awake();
        SceneController.Instance.CurMode = GameMode.Stand;//测试使用
        MapActor.OnLoad();
        UIGameControl.OnLoad();
    }
    private void Start()
    {
        //将当前对象添加到监听者对象
        EventController.Instance.AddListener(this);
    }

    /// <summary>
    /// 在线模式选择移动杯子的方法打开杯子点击事件
    /// </summary>
    public void SingCupMethod()
    {
        for (int i = 0; i < UIGameControl.infoPanel.Length; i++)
        {
            if (UIGameControl.infoPanel[i].Info.User.id == Actor.uid)
            {
                for (int k = 0; k < UIGameControl.infoPanel[i].Info.m_Player.Count; k++)
                {
                    //给当前要移动的杯子注册点击事件点击事件
                    UIGameControl.infoPanel[i].Info.m_Player[k].SeverDoAtionCup(true);
                }
            }
        }

    }

    /// <summary>
    /// 在线模式关闭杯子点击事件
    /// </summary>
    public void StopCupMethod()
    {
        for (int i = 0; i < UIGameControl.infoPanel.Length; i++)
        {
            if (UIGameControl.infoPanel[i].Info.User.id == Actor.uid)
            {
                for (int k = 0; k < UIGameControl.infoPanel[i].Info.m_Player.Count; k++)
                {
                    //给当前要移动的杯子注册点击事件点击事件
                    UIGameControl.infoPanel[i].Info.m_Player[k].SeverDoAtionCup(false);
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
        for (int i = 0; i < UIGameControl.infoPanel.Length; i++)
        {
            if (userId == UIGameControl.infoPanel[i].Info.User.id)
                UIGameControl.infoPanel[i].Info.m_Player[id].mPlayerLogic.ServerTaskNextCup();
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
        for (int i = 0; i < UIGameControl.infoPanel.Length; i++)
        {
            if (userId == UIGameControl.infoPanel[i].Info.User.id)
                UIGameControl.infoPanel[i].Info.m_Player[id].mPlayerLogic.MoveNextCup(pos);
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
