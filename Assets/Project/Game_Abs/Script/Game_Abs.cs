using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData.APIS;
using NetWorkAndData;

public class Game_Abs : EZMonoBehaviour
{
	private Transform thisTra;
	private Button Close;
	private Button RemoveAbs;
	private Text TitleText;

	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);
		Close = thisTra.Find("BGImage/Abs_Close").GetComponent<Button>();
		RemoveAbs = thisTra.Find("BGImage/RemoveAbsOk").GetComponent<Button>();
		TitleText = thisTra.Find("BGImage/ThanksText").GetComponent<Text>();
		Close.onClick.AddListener(CloseMethod);
		RemoveAbs.onClick.AddListener(RemoveAbsMethod);
		RemoveAbsEnd();

	}
	
    void CloseMethod()
	{
		EZComponent.RemoveConment<Game_Abs>();
	}

	//�����Ƴ���������
	void RemoveAbsMethod()
	{
		NetworkController.Instance.Send(new RemoveAdRequest());
	}

	public override void End()
	{
		EZUIGroup.Close(this);

	}
	//����Ƿ�ȥ��
	void RemoveAbsEnd()
	{
		bool IsOn = (bool)SignInResponse.GetSign(APIS.removeAd);
		if (IsOn)
		{ TitleText.text = "���Ѿ�ȥ���������"; RemoveAbs.gameObject.SetActive(false);  }
		else
		{ TitleText.text = "лл��Ĺ���"; RemoveAbs.gameObject.SetActive(true); }
	}

}
