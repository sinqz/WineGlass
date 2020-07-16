using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class MenuBG : EZMonoBehaviour
{
	private Transform thisTra;
	private Button UseragreeMent;//用户协议
	private Button PrivacyPolicy;//隐私政策
	public override void Start()
	{

		thisTra = EZUIGroup.Open(this);
		UseragreeMent = thisTra.Find("BGImage/AnnotationText/UseragreeMent").GetComponent<Button>();
		PrivacyPolicy = thisTra.Find("BGImage/AnnotationText/PrivacyPolicy").GetComponent<Button>();
		UseragreeMent.onClick.AddListener(() => { Debug.Log("用户协议"); });
		PrivacyPolicy.onClick.AddListener(() => { Debug.Log("隐私政策"); });
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
