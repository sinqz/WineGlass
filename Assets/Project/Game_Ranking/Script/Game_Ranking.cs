using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_Ranking : EZMonoBehaviour
{
	private Transform thisTra;

	private Text m_ranking;
	private Button RankingClose;

	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);
		m_ranking = thisTra.Find("Hone_Ranking/Ranking_Text").GetComponent<Text>();
		RankingClose = thisTra.Find("Hone_Ranking/Home_Ranking_Close").GetComponent<Button>();
		RankingClose.onClick.AddListener(Close);
	}

	void Close()
	{
		EZComponent.AddConment<Game_Lobby>();
		EZComponent.RemoveConment<Game_Ranking>();
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
