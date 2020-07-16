using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System.Runtime.CompilerServices;
using NetWorkAndData;

public class Game_UnifyUI : EZMonoBehaviour
{
    private Transform thisTra;
    public ToggleUnifyUI UnifyUI;
    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        UnifyUI = thisTra.Find("ToggleUnifyUI").GetComponent<ToggleUnifyUI>();
    }


    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
