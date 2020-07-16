using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_Single : EZMonoBehaviour
{
	private Transform thisTra;
	private Button SingleGameBtn;
	private Button NetWorkingGameBtn;
	private Button FriendGameBtn;
	private Button CreateaRoomGameBtn;

	public override void Start()
	{

		thisTra = EZUIGroup.Open(this);
		SingleGameBtn = thisTra.Find("SingleGame").GetComponent<Button>();
		SingleGameBtn.onClick.AddListener(SingleGameMethod);
		NetWorkingGameBtn = thisTra.Find("NetWorkingGame").GetComponent<Button>();
		NetWorkingGameBtn.onClick.AddListener(NetWorkingGameMethod);
		FriendGameBtn = thisTra.Find("FriendGame").GetComponent<Button>();
		FriendGameBtn.onClick.AddListener(FriendGameMethod);
		CreateaRoomGameBtn = thisTra.Find("CreateaRoomGame").GetComponent<Button>();
		CreateaRoomGameBtn.onClick.AddListener(CreateaRoomGameMethod);
	}
	/// <summary>
	/// ������Ϸ
	/// </summary>
	void SingleGameMethod()
	{
		EZComponent.AddConment<Game_Prepare>();
		EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.SingleGame);
	}
	/// <summary>
	/// ����ƥ��
	/// </summary>
	void NetWorkingGameMethod()
	{
		EZComponent.AddConment<Game_Prepare>();
		EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.NetWorKingGame);
	}
	/// <summary>
	/// ���Ѷ�ս
	/// </summary>
	void FriendGameMethod()
	{
		EZComponent.AddConment<Game_Prepare>();
		EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.FriendGame);
	}
	/// <summary>
	/// ��������
	/// </summary>
	void CreateaRoomGameMethod()
	{
		EZComponent.AddConment<Game_Prepare>();
		EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.CreareaRoomGame);
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
