using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class UILoading : EZMonoBehaviour
{
    //父级UILoading
    private Transform thisTra;
    private string[] loadingString = new string[]
    {   "我是随机的第一句话",
        "我是随机的第二句话",
        "我是随机的第三句话",
        "我是随机的第四句话"
    };
    //所有可能读条时显示的背景
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
    //背景文字
    private Text BKString;
    //背景图片
    private Image BKImage;
    //滚动条
    private Slider ProgressSlider;
    //加载百分比进度文本
    private Text ProgressValue;

    float Timer = 0;
    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        BKString = thisTra.GetComponentInChildren<Text>();
        BKImage = thisTra.GetComponentInChildren<Image>();
        ProgressSlider = thisTra.GetComponentInChildren<Slider>();
        ProgressValue = thisTra.Find("ProText").GetComponent<Text>();
        //随机一段话
        BKString.text = LoadingString;
        BKImage.sprite = LoadingImage[Random.Range(0, LoadingImage.Length)];
    }

    public override void Update()
    {
        //每3秒切换一个图片或文字
        Timer += Time.deltaTime;
        if (Timer >= 1.5f)
        {
            //随机一段话
            BKString.text = LoadingString;
            BKImage.sprite = LoadingImage[Random.Range(0, LoadingImage.Length)];
            Timer = 0;
        }
    }


    public override void End()
    {
        EZUIGroup.Close(this);
    }

    //同步场景切换进度条数据
    public void UpdateProgressValue(int _Value)
    {
        //更新进度条
        ProgressSlider.value = (float)_Value / 100;
        //同步文本
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
