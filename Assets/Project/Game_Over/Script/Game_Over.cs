using UnityEngine;
using UnityEngine.UI;
using EZFramework;



public class Game_Over : EZMonoBehaviour
{
	private Transform thisTra;

	private GameObject m_continue;

	private Text m_game;
	private GameObject GameOverStand;
	private GameObject GameOverSever;

	public override void Start()
	{

		thisTra = EZUIGroup.Open(this);

		m_continue = thisTra.Find("BGImage/Continue_Game").GetComponent<GameObject>();
		m_game = thisTra.Find("BGImage/Game_Text").GetComponent<Text>();
		GameOverStand = thisTra.Find("DeathImageStand").gameObject;
		GameOverSever = thisTra.Find("DeathImageSever").gameObject;
	}

	public void SetGameMethod(GameMode mGameMode)
	{
        switch (mGameMode)
        {
            case GameMode.Stand:
				GameOverStand.SetActive(true);
				GameOverSever.SetActive(false);
				break;
            case GameMode.Sever:
				GameOverStand.SetActive(false);
				GameOverSever.SetActive(true);
				break;
        }
    }

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
