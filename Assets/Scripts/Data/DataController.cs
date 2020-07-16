using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//数据控制器，专门负责游戏中各类游戏对象的数据从外部文件加载与管理
public class DataController : MonoSingletonTemplateScript<DataController>
{

    public void OnLoad()
    {
        DontDestroyOnLoad(this);
    }

    //加载玩家数据
    public void LoadPlayerData(Player m_Player)
    {
        //确定玩家数据路径
    }

}
