using NetWorkAndData.APIS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    //头像框背景
    public Image InfoPanelImage;

    //名字
    public Text NickName;

    //头像Sprite
    public Image HeadSprite;

    //ID
    public Text UserID;

    //道具卡图标
    public GameObject SingleItem;

    //道具卡数量
    public Text Single_IDItem;

    //Single_ID
    public Text Single_ID;

    public PlayerInfo Info;

    /// <summary>
    /// 多人对战同步初始化
    /// </summary>
    /// <param name="uid"></param>
    public void InItInfo(string uid)
    {
        Info.User = new GameUser();
        Info.User.id = EpheMeralActor.DictUser[uid].id;
        Info.User.cupItemId = EpheMeralActor.DictUser[uid].cupItemId;
        Info.User.diceItemId = EpheMeralActor.DictUser[uid].diceItemId;
        Info.User.avatarItemId = EpheMeralActor.DictUser[uid].avatarItemId;
        Info.User.itemCount = EpheMeralActor.DictUser[uid].itemCount;
        Info.User.nickName = EpheMeralActor.DictUser[uid].nickName;
        Info.User.userId = EpheMeralActor.DictUser[uid].userId;

        NickName.text = Info.User.nickName;
        int IDCount = Info.User.itemCount;
        if (IDCount > 0)
        {
            Single_ID.gameObject.SetActive(false);
            SingleItem.SetActive(true);
            Single_IDItem.text = IDCount.ToString();
            UserID.gameObject.SetActive(true);
            UserID.text = Info.User.userId;
        }
        else
        {
            SingleItem.SetActive(false);
            UserID.gameObject.SetActive(false);
            Single_ID.gameObject.SetActive(true);
            Single_ID.text = Info.User.userId;
        }
    }

    /// <summary>
    /// 单机模式只对自己赋值
    /// </summary>
    public void InItInfo()
    {
        NickName.text = Actor.nickName;
        int IDCount = Actor.itemCount;
        if (IDCount > 0)
        {
            Single_ID.gameObject.SetActive(false);
            SingleItem.SetActive(true);
            Single_IDItem.text = IDCount.ToString();
            UserID.gameObject.SetActive(true);
            UserID.text = Actor.userId;
        }
        else
        {
            SingleItem.SetActive(false);
            UserID.gameObject.SetActive(false);
            Single_ID.gameObject.SetActive(true);
            Single_ID.text = Actor.userId;
        }
    }
}
