using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class MenuBG : EZMonoBehaviour
{
	private Transform thisTra;
	private Button UseragreeMent;//�û�Э��
	private Button PrivacyPolicy;//��˽����
	public override void Start()
	{

		thisTra = EZUIGroup.Open(this);
		UseragreeMent = thisTra.Find("BGImage/AnnotationText/UseragreeMent").GetComponent<Button>();
		PrivacyPolicy = thisTra.Find("BGImage/AnnotationText/PrivacyPolicy").GetComponent<Button>();
		UseragreeMent.onClick.AddListener(() => { Debug.Log("�û�Э��"); });
		PrivacyPolicy.onClick.AddListener(() => { Debug.Log("��˽����"); });
	}

	public override void End()
	{
		EZUIGroup.Close(this);
	}

}
