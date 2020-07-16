using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System;
using NetWorkAndData.APIS;

public class Game_Lobby : EZMonoBehaviour
{
	private Transform thisTra;
	private Button CloseAdMob;//去广告
	private Button LuckyBox;//盲盒
	private Button Task;//任务
	private Button KnapSack;//背包
	private Button ShopPing;//商店
	private Button RingKing;//排行榜
	private Button Friend;//好友
	private Button HeadImage;//头像
	private Button NickName;//修改名字
	private Text Name;
	private Text UserID;

	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);

		CloseAdMob = thisTra.Find("CloseAdmobBtn").GetComponent<Button>();
		LuckyBox = thisTra.Find("LuckyBoxBtn").GetComponent<Button>();
		Task = thisTra.Find("TaskBtn").GetComponent<Button>();
		KnapSack = thisTra.Find("KnapSackBtn").GetComponent<Button>();
		ShopPing = thisTra.Find("ShopPingBtn").GetComponent<Button>();
		RingKing = thisTra.Find("RinkingBtn").GetComponent<Button>();
		Friend = thisTra.Find("FriendBtn").GetComponent<Button>();
		Name = thisTra.Find("Head_Image_Name/Name").GetComponent<Text>();
		UserID = thisTra.Find("Head_Image_Name/ID").GetComponent<Text>();
		HeadImage = thisTra.Find("Head_Image_Name/HeadImage").GetComponent<Button>();
		NickName = thisTra.Find("Head_Image_Name/NickName").GetComponent<Button>();
		
		CloseAdMob.onClick.AddListener(CloseAdMobMethod);
		LuckyBox.onClick.AddListener(LuckyBoxMethod);
		Task.onClick.AddListener(TaskMethod);
		KnapSack.onClick.AddListener(KnapSackMethod);
		ShopPing.onClick.AddListener(ShopPingMethod);
		RingKing.onClick.AddListener(RingKingMethod);
		Friend.onClick.AddListener(FriendMethod);
		HeadImage.onClick.AddListener(HeadImageMethod);
		NickName.onClick.AddListener(NickNameMethod);
	}

    public override void LateUpdate()
    {
		Name.text = Actor.nickName;//姓名
		UserID.text = Actor.userId;//ID
    }

    //好友的点击事件
    private void FriendMethod()
	{
		Debug.Log("好友的点击事件");
		EZComponent.AddConment<Game_Friend>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//商店的点击事件
	private void RingKingMethod()
	{
		Debug.Log("排行榜的点击事件");
		EZComponent.AddConment<Game_Ranking>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//排行榜的点击事件
	private void ShopPingMethod()
	{
		Debug.Log("商店的点击事件");
		EZComponent.AddConment<Game_ShopPing>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//背包的点击事件
	private void KnapSackMethod()
	{
		Debug.Log("背包的点击事件");
		EZComponent.AddConment<Game_Knapsack>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//去广告的方法
	private void CloseAdMobMethod()
	{
		Debug.Log("去广告的方法");
		EZComponent.AddConment<Game_Abs>();
	}

	//盲盒的点击事件
	private void LuckyBoxMethod()
	{
		Debug.Log("盲盒的点击事件");
		EZComponent.AddConment<Game_MagicBox>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//任务的点击事件
	private void TaskMethod()
	{
		Debug.Log("任务的点击事件");
		EZComponent.AddConment<Game_Task>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	private void HeadImageMethod()
	{
		EZComponent.AddConment<Game_HomePage>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	private void NickNameMethod()
	{
		EZComponent.AddConment<Game_EditData>();
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
