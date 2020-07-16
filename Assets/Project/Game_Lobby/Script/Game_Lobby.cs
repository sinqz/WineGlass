using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System;
using NetWorkAndData.APIS;

public class Game_Lobby : EZMonoBehaviour
{
	private Transform thisTra;
	private Button CloseAdMob;//ȥ���
	private Button LuckyBox;//ä��
	private Button Task;//����
	private Button KnapSack;//����
	private Button ShopPing;//�̵�
	private Button RingKing;//���а�
	private Button Friend;//����
	private Button HeadImage;//ͷ��
	private Button NickName;//�޸�����
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
		Name.text = Actor.nickName;//����
		UserID.text = Actor.userId;//ID
    }

    //���ѵĵ���¼�
    private void FriendMethod()
	{
		Debug.Log("���ѵĵ���¼�");
		EZComponent.AddConment<Game_Friend>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//�̵�ĵ���¼�
	private void RingKingMethod()
	{
		Debug.Log("���а�ĵ���¼�");
		EZComponent.AddConment<Game_Ranking>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//���а�ĵ���¼�
	private void ShopPingMethod()
	{
		Debug.Log("�̵�ĵ���¼�");
		EZComponent.AddConment<Game_ShopPing>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//�����ĵ���¼�
	private void KnapSackMethod()
	{
		Debug.Log("�����ĵ���¼�");
		EZComponent.AddConment<Game_Knapsack>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//ȥ���ķ���
	private void CloseAdMobMethod()
	{
		Debug.Log("ȥ���ķ���");
		EZComponent.AddConment<Game_Abs>();
	}

	//ä�еĵ���¼�
	private void LuckyBoxMethod()
	{
		Debug.Log("ä�еĵ���¼�");
		EZComponent.AddConment<Game_MagicBox>();
		EZComponent.RemoveConment<Game_Lobby>();
	}

	//����ĵ���¼�
	private void TaskMethod()
	{
		Debug.Log("����ĵ���¼�");
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
