using EZFramework;
using NetWorkAndData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoneShoping : MonoBehaviour
{
    public Toggle CoinToggle;
    public Toggle DiamondToggle;
    public Toggle ItemToggle;
    public Toggle CupToggle;
    public Toggle DiceToggle;
    StoreItemRequest request = new StoreItemRequest();
    private void Start()
    {
        InIt();
    }

    void InIt()
    {
        CoinToggle.onValueChanged.AddListener(IsOn => { if (IsOn) { request.categoryType = CategoryType.coin; NetworkController.Instance.Send(request); } });
        CoinToggle.onValueChanged.Invoke(true);
        DiamondToggle.onValueChanged.AddListener(IsOn => { if (IsOn) { request.categoryType = CategoryType.diamond; NetworkController.Instance.Send(request); } });
        ItemToggle.onValueChanged.AddListener(IsOn => { if (IsOn) { request.categoryType = CategoryType.item; NetworkController.Instance.Send(request); } });
        CupToggle.onValueChanged.AddListener(IsOn => { if (IsOn) { request.categoryType = CategoryType.cup; NetworkController.Instance.Send(request); } });
        DiceToggle.onValueChanged.AddListener(IsOn => { if (IsOn) { request.categoryType = CategoryType.dice; NetworkController.Instance.Send(request); } });
    }
}
