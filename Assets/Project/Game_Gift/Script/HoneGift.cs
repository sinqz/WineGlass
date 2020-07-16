using NetWorkAndData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneGift : MonoBehaviour
{
    private void Start()
    {
        Init();
    }
    void Init()
    {
        NetworkController.Instance.Send(new SignInRewardListRequest());
    }
}
