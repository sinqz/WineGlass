using UnityEngine;
using UnityEngine.UI;

public class UIScrollViewController : MonoBehaviour
{

    public ScrollRect scroll;
    public Toggle NaxtToggle;
    public Toggle PreviosToggle;


    void Start()
    {
        scroll.onValueChanged.AddListener(Vect => { Vect.x = scroll.horizontalNormalizedPosition; ScrollRectMethod(Vect.x); });
    }

    void ScrollRectMethod(float HorLayoutPos)
    {
        if (HorLayoutPos < 0.9f)
            NaxtToggle.SetIsOnWithoutNotify(true);
        else
            PreviosToggle.SetIsOnWithoutNotify(true);

    }

}
