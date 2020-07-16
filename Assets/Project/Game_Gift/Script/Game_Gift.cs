using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_Gift : EZMonoBehaviour
{
	private Transform thisTra;
	private Button Close;

	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);
		Close = thisTra.Find("BGImage/Close").GetComponent<Button>();
		Close.onClick.AddListener(CloseMethod);
	}
	void CloseMethod()
	{
		EZComponent.RemoveConment<Game_Gift>();
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
