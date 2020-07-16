using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData;

public class Game_Task : EZMonoBehaviour
{
	private Transform thisTra;

	private Button TaskClose;

	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);
		TaskClose = thisTra.Find("Hone_Task/Home_Task_Close").GetComponent<Button>();
		TaskClose.onClick.AddListener(Close);
	}

	void Close()
	{
		EZComponent.AddConment<Game_Lobby>();
		EZComponent.RemoveConment<Game_Task>();

	}

	public override void End()
	{

		EZUIGroup.Close(this);
	}

}
