using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_Play : EZMonoBehaviour
{
	private Transform thisTra;

	public override void Start()
	{

		thisTra = EZUIGroup.Open(this);


	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
