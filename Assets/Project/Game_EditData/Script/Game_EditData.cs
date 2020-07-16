using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData;
using NetWorkAndData.APIS;

public class Game_EditData : EZMonoBehaviour
{
    private Transform thisTra;
    private InputField FieldnickName;
    private string UserItemId;
    private Button Close;
    private Button Edit_End;
    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        FieldnickName = thisTra.Find("ModifyName").GetComponent<InputField>();
        Close = thisTra.Find("CloseBtn").GetComponent<Button>();
        Edit_End = thisTra.Find("Edit_End").GetComponent<Button>();
        Edit_End.onClick.AddListener(NickNameMethod);
        Close.onClick.AddListener(CloseMethod);
    }

    void NickNameMethod()
    {
        ChangeNickNameRequest changeNick = new ChangeNickNameRequest();
        changeNick.nickName = FieldnickName.text;
        EpheMeralActor.nickName = changeNick.nickName;//给临时数据保存
        changeNick.userItemId = "5ee9e81e922c8317aad2e974";
        NetworkController.Instance.Send(changeNick);
    }

    void CloseMethod()
    {
        EZComponent.RemoveConment<Game_EditData>();
    }


    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
