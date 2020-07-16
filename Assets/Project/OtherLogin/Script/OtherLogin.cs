using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class OtherLogin : EZMonoBehaviour
{
	private Transform thisTra;

	private Button CloseBtn;//������һ��

	private Button QQBtn;//QQ��¼

	private Button FaceBookBtn;//FaceBook��¼

	public override void Start()
	{
		thisTra = EZUIGroup.Open(this);
		CloseBtn = thisTra.Find("Close").GetComponent<Button>();
		QQBtn = thisTra.Find("QQ").GetComponent<Button>();
		FaceBookBtn = thisTra.Find("FaceBook").GetComponent<Button>();
		CloseBtn.onClick.AddListener(() => { EZComponent.RemoveConment<OtherLogin>();EZComponent.AddConment<MenuLogin>(); });
		QQBtn.onClick.AddListener(QQLogin);
		FaceBookBtn.onClick.AddListener(FaceBookLogin);
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}


	private void QQLogin()
	{
		Debug.Log("QQ��¼�ķ���");
	}


	private void FaceBookLogin()
	{
		Debug.Log("FaceBook��¼�ķ���");
	}


}
