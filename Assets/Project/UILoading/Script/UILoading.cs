using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class UILoading : EZMonoBehaviour
{
    //����UILoading
    private Transform thisTra;
    private string[] loadingString = new string[]
    {   "��������ĵ�һ�仰",
        "��������ĵڶ��仰",
        "��������ĵ����仰",
        "��������ĵ��ľ仰"
    };
    //���п��ܶ���ʱ��ʾ�ı���
    private string LoadingString
    {
        get
        {
            int TempCount = Random.Range(0, loadingString.Length);
            return loadingString[TempCount];
        }
    }
    private Sprite[] LoadingImage
    {
        get
        {
            return Resources.LoadAll<Sprite>("UI/LoadingBK");
        }
    }
    //��������
    private Text BKString;
    //����ͼƬ
    private Image BKImage;
    //������
    private Slider ProgressSlider;
    //���ذٷֱȽ����ı�
    private Text ProgressValue;

    float Timer = 0;
    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        BKString = thisTra.GetComponentInChildren<Text>();
        BKImage = thisTra.GetComponentInChildren<Image>();
        ProgressSlider = thisTra.GetComponentInChildren<Slider>();
        ProgressValue = thisTra.Find("ProText").GetComponent<Text>();
        //���һ�λ�
        BKString.text = LoadingString;
        BKImage.sprite = LoadingImage[Random.Range(0, LoadingImage.Length)];
    }

    public override void Update()
    {
        //ÿ3���л�һ��ͼƬ������
        Timer += Time.deltaTime;
        if (Timer >= 1.5f)
        {
            //���һ�λ�
            BKString.text = LoadingString;
            BKImage.sprite = LoadingImage[Random.Range(0, LoadingImage.Length)];
            Timer = 0;
        }
    }


    public override void End()
    {
        EZUIGroup.Close(this);
    }

    //ͬ�������л�����������
    public void UpdateProgressValue(int _Value)
    {
        //���½�����
        ProgressSlider.value = (float)_Value / 100;
        //ͬ���ı�
        ProgressValue.text = _Value + "%";
    }

    public void ShowImage(bool ShowImage)
    {
        if (ShowImage)
            BKImage.color = Color.white;
        else
            BKImage.color = Color.clear;
    }
}
