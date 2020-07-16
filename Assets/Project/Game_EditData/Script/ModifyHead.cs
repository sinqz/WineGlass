using LitJson;
using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyHead : MonoBehaviour
{
    public ToggleGroup Group;
    public GameObject HeadTemp;
    public Dictionary<string, GameObject> HeadDic = new Dictionary<string, GameObject>();//头像
    private void Start()
    { Init(); }
    void Init()
    {
        //JsonData data = JsonMapper.ToObject(SignInResponse.GetSign(APIS.items).ToJson());
        //for (int i = 0; i < data.Count; i++)
        //{
        //    //实例化头像
        //    GameObject tempSprite = Instantiate(HeadTemp);
        //    tempSprite.transform.SetParent(transform);
        //    tempSprite.name = data[i][APIS.itemId].ToString();//头像名字重新赋值
        //    tempSprite.GetComponent<Image>().sprite = Actor.HeadSprite[i];//头像赋值
        //    tempSprite.GetComponent<Toggle>().group = Group;
        //    HeadDic.Add(data[i][APIS.uid].ToString(), tempSprite);//存储到头像字典中
        //}
    }
}
