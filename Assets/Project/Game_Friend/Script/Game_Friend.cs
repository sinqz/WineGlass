using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData.APIS;

public class Game_Friend : EZMonoBehaviour
{
	private Transform thisTra;

	private Text m_friend;
	private Button FriendClose;
	private Text UserID;
	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);
		m_friend = thisTra.Find("Hone_Friend/Friend_Text").GetComponent<Text>();
		FriendClose = thisTra.Find("Home_Friend_Close").GetComponent<Button>();
		UserID = thisTra.Find("Hone_Friend/FriendBGImage/FriendController/Init/ID/Text").GetComponent<Text>();
		FriendClose.onClick.AddListener(Close);
	}

    public override void LateUpdate()
    {
		UserID.text = Actor.userId;
    }

    void Close()
	{
		EZComponent.AddConment<Game_Lobby>();
		EZComponent.RemoveConment<Game_Friend>();
	}


	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
