using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_MagicBox : EZMonoBehaviour
{
	private Transform thisTra;

	private Button Close;

	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);
		Close = thisTra.Find("Home_Knapsack_Close").GetComponent<Button>();
		Close.onClick.AddListener(CloseMethod);
	}

	void CloseMethod()
	{
		EZComponent.RemoveConment<Game_MagicBox>();
		EZComponent.AddConment<Game_Lobby>();
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
