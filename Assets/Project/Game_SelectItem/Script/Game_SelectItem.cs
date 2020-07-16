using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_SelectItem : EZMonoBehaviour
{
    private Transform thisTra;
    private Button CloseClick;

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        CloseClick = thisTra.Find("Close").GetComponent<Button>();
        CloseClick.onClick.AddListener(CloseClickMethod);
    }
   
    void CloseClickMethod()
    {
        EZComponent.RemoveConment<Game_SelectItem>();
      
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
