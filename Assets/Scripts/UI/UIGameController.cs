using EZFramework;
using NetWorkAndData;
using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//游戏场景专属的UI控制器，专门负责管理游戏场景中的UI控件的逻辑
public class UIGameController : MonoBehaviour
{
    //地图构建
    public Image MapUI;

    public Toggle SinglePlay;//暂停按钮

    public Button SingleDice;//骰子按钮

    public Button SingleClose;//退出按钮

    public Toggle SingleTrusteeShip;//托管按钮

    public InfoPanel[] infoPanel;

    //地图Image
    public Image MapImage;

    public Sprite[] MapSprite { get { return Resources.LoadAll<Sprite>("UI/Map"); } }//地图Sprite数组

    public Sprite[] HeadSprite { get { return Resources.LoadAll<Sprite>("UI/HeadImage"); } }//头像Image数组

    List<string> entry = new List<string>(EpheMeralActor.DictUser.Keys);

    public int[] Num = new int[] { 0, 13, 26, 39 };

    public int[] Me = new int[] { 67, 52, 57, 62 };

    public void OnLoad()
    {
        switch (SceneController.Instance.CurMode)
        {
            case GameMode.Null:
                break;
            case GameMode.Stand:
                StandSingleInItfo();
                break;
            case GameMode.Sever:
                SeverSingleInItInfo();
                break;
        }
    }

    private void Start()
    {
        SingleDice.gameObject.SetActive(false);
        SinglePlay.gameObject.SetActive(false);
        SingleClose.onClick.AddListener(() => { });
    }

    /// <summary>
    /// 在线模式初始化
    /// </summary>
    void SeverSingleInItInfo()
    {
        for (int i = 0; i < infoPanel.Length; i++)
        {
            if (entry[i] == Actor.uid)
            {
                MapImage.sprite = MapSprite[i];
                for (int k = 0; k < infoPanel.Length; k++)
                {
                    infoPanel[k].GetComponent<Image>().sprite = HeadSprite[(i + k) % infoPanel.Length];
                    infoPanel[k].InItInfo(entry[(i + k) % infoPanel.Length]);
                }
            }
        }
        MapSingleInItMethod();
    }

    /// <summary>
    /// 在线模式地图初始化
    /// </summary>
    void MapSingleInItMethod()
    {
        for (int i = 0; i < infoPanel.Length; i++)
        {
            if (entry[i] == Actor.uid)
            {
                for (int k = 0; k < 52; k++)
                {
                    ActorPathAction.Instance.ActorDic[k].gameObject.name = ((k + Num[i]) % 52).ToString();
                }
                for (int L = 52; L < 72; L++)
                {
                    ActorPathAction.Instance.ActorDic[L].gameObject.name = ((L + Me[i]) % 72 + 20 > 71 ? (L + Me[i]) % 72 : (L + Me[i]) % 72 + 20).ToString();
                }
            }
        }
    }

    /// <summary>
    /// 单机模式初始化
    /// </summary>
    void StandSingleInItfo()
    {
        SinglePlay.gameObject.SetActive(true);
    }

    /// <summary>
    /// 扔骰子的方法
    /// </summary>
    public void SingleDiceMethod(string uid)
    {
        //判断如果是自己,则打开摇骰子按钮
        if (Actor.uid == uid)
        {
            SingleDice.gameObject.SetActive(true);
            SingleDice.onClick.AddListener(() =>
            {
                //发送扔骰子请求
                NetworkController.Instance.Send(new PlayTurnRequest());
            });
        }
    }
}
