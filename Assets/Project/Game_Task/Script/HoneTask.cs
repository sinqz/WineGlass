using NetWorkAndData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoneTask : MonoBehaviour
{
    public Toggle TaskToggle;
    public Toggle AchievenmentToggle;
    AchievementListRequest request = new AchievementListRequest();
    void Start()
    {
        TaskToggle.onValueChanged.AddListener(IsOn => { if (IsOn) { request.dayQuest = true; NetworkController.Instance.Send(request); } });
        TaskToggle.onValueChanged.Invoke(true);
        AchievenmentToggle.onValueChanged.AddListener(IsOn => { if (IsOn) { request.dayQuest = false; NetworkController.Instance.Send(request); } });
    }

}
