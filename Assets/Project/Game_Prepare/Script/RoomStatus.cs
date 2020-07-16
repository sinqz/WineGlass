using EZFramework;
using NetWorkAndData.APIS;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RoomStatus : MonoBehaviour
{

    public PlayerRoom[] Rooms;
    public Button CloseMatching;

    private void Start()
    {
        EpheMeralActor.mSprite = Resources.Load<Sprite>("UI/onlineImage");
        CloseMatching.onClick.AddListener(CloseMatchingMethod);
    }

    void CloseMatchingMethod()
    {
        EZComponent.AddConment<Game_UnifyUI>();
        EZComponent.GetConment<Game_UnifyUI>().UnifyUI.LoadBeOut.isOn = true;
    }

    [Serializable]
    public class PlayerRoom : GameUser
    {
        public Image mImage;
        public Text mName;

        public void End()
        {
            mImage.sprite = EpheMeralActor.mSprite;
            mName.text = "";
            cupItemId = "";
            diceItemId = "";
            avatarItemId = "";
            id = "";
            itemCount = 0;
            nickName = "";
            userId = "";
        }
    }
}
