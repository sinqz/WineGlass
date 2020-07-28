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
    public Text TrusteeText;

    public InfoPanel[] infoPanel;

    public Text TestText;

    //地图Image
    public Image MapImage;

    public Sprite[] MapSprite { get { return Resources.LoadAll<Sprite>("UI/Map"); } }//地图Sprite数组

    public Sprite[] HeadSprite { get { return Resources.LoadAll<Sprite>("UI/HeadImage"); } }//头像Image数组

    List<string> entry = new List<string>(EpheMeralActor.DictUser.Keys);

    private int[] Num = new int[] { 0, 13, 26, 39 };//地图占位

    private int[] Me = new int[] { 67, 52, 57, 62 };//地图占位

    public void OnLoad()
    {
        SingleDice.gameObject.SetActive(false);
        SinglePlay.gameObject.SetActive(false);
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
        SingleClose.onClick.AddListener(() => { });
        SingleTrusteeShip.onValueChanged.AddListener(IsOn => TrusteeShipMethod(IsOn));
        SinglePlay.onValueChanged.AddListener(IsOn => { if (IsOn) Time.timeScale = 0; else Time.timeScale = 1; });
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
        SingleDice.onClick.AddListener(() =>
        {
            //发送扔骰子请求
            NetworkController.Instance.Send(new PlayTurnRequest());
        });
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

        for (int i = 0; i < infoPanel.Length; i++)//除了初始化自己其他都设为机器人
        {
            if (i == 0) infoPanel[i].InItInfo();
            else infoPanel[i].Info.RoleType = RoleType.AI;
        }
        StartCoroutine(StandIEGamePlay());
        SingleDice.onClick.AddListener(() => StandDiceAnimation());
    }

    /// <summary>
    /// 在线模式扔骰子的方法
    /// </summary>
    public void SingleDiceMethod(string uid)
    {
        //判断如果是自己,则打开摇骰子按钮
        if (Actor.uid == uid)
        {
            SingleDice.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 单机游戏起始协程
    /// </summary>
    IEnumerator StandIEGamePlay()
    {
        yield return new WaitForSeconds(1f);//延时一秒开始游戏
        EpheMeralActor.playCount = Random.Range(0, 3); //随机一名玩家开始游戏
        infoPanel[EpheMeralActor.playCount].Info.StandPlayNext();
        Debug.Log("开始游戏,随机的玩家:" + EpheMeralActor.playCount);
    }

    /// <summary>
    /// 下名玩家移动
    /// </summary>
    public void StandTakeToMove()
    {
        StartCoroutine(IEStandTakeToMove());
    }

    /// <summary>
    /// 轮流携程
    /// </summary>
    /// <returns></returns>
    IEnumerator IEStandTakeToMove()
    {
        yield return new WaitForSeconds(0.7f);
        Debug.Log("轮到下名玩家移动");
        EpheMeralActor.playCount = (EpheMeralActor.playCount + 1) % infoPanel.Length;
        if (!EpheMeralActor.IsTrust)//如果是自己并且用户取消掉了托管,则取消托管
            infoPanel[0].Info.SetRoleType(RoleType.PLAYER);
        yield return new WaitForSeconds(0.1f);
        infoPanel[EpheMeralActor.playCount].Info.StandPlayNext();
    }

    /// <summary>
    /// 在线模式骰子动画
    /// </summary>
    public void NetWorkDiceAnimation(int DiceCount)
    {

    }

    /// <summary>
    /// 单机模式骰子动画
    /// </summary>
    public void StandDiceAnimation()
    {
        EpheMeralActor.TrusteeAction.Clear();//清除掉存储的方法
        SingleDice.gameObject.SetActive(false);
        AudioController.SPlayer.PlaySound(AudioBase.PLAY_DICE);//播放摇骰子动画
        EpheMeralActor.StandDiceCount = Random.Range(1, 7);
        Debug.Log("骰子点数:" + EpheMeralActor.StandDiceCount);
        infoPanel[0].Info.StandDotionCup();//打开杯子点击事件
    }

    /// <summary>
    /// 游戏托管方法
    /// </summary>
    void TrusteeShipMethod(bool IsOn)
    {
        if (IsOn) TrusteeText.text = "托管中..."; else TrusteeText.text = "托管";
        switch (SceneController.Instance.CurMode)
        {
            case GameMode.Null:
                break;
            case GameMode.Stand:
                if (IsOn)
                {
                    infoPanel[0].Info.SetRoleType(RoleType.AI);
                    EpheMeralActor.IsTrust = true;
                }
                else
                    EpheMeralActor.IsTrust = false;
                break;
            case GameMode.Sever:
                break;
        }
    }
    private void Update()
    {
        if (EpheMeralActor.IsTrust && EpheMeralActor.TrusteeAction.Count != 0)
        {
            EpheMeralActor.TrusteeAction.Dequeue()?.Invoke();
        }
    }

    private void LateUpdate()
    {
        TestText.text = $"移动玩家:{Name[EpheMeralActor.playCount < 0 ? 0 : EpheMeralActor.playCount]}\n移动点数:{EpheMeralActor.StandDiceCount}";
        TestText.color = Colors[EpheMeralActor.playCount < 0 ? 0 : EpheMeralActor.playCount];
    }

    string[] Name = new string[] { "红色", "黄色", "蓝色", "绿色" };
    Color[] Colors = new Color[] { Color.red, Color.yellow, Color.blue, Color.green };

}
