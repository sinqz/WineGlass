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
	/// 单机游戏
	/// </summary>
	void SingleGameMethod()
	{
		EZComponent.AddConment<Game_Prepare>();
		EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.SingleGame);
	}
	/// <summary>
	/// 在线匹配
	/// </summary>
	void NetWorkingGameMethod()
	{
		EZComponent.AddConment<Game_Prepare>();
		EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.NetWorKingGame);
	}
	/// <summary>
	/// 好友对战
	/// </summary>
	void FriendGameMethod()
	{
		EZComponent.AddConment<Game_Prepare>();
		EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.FriendGame);
	}
	/// <summary>
	/// 创建房间
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
