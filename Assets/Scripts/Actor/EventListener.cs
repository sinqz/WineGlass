using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Dynamic;

public class EventListener : MonoBehaviour, IPointerClickHandler
{
    public Action OnClick;

    public bool longPressTriggered = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!longPressTriggered)
        {
            if (OnClick != null)
                OnClick();
        }
    }

    public static EventListener AddEventListenr(GameObject obj)
    {
        EventListener listener = obj.GetComponent<EventListener>();
        if (listener == null)
            listener = obj.AddComponent<EventListener>();
        return listener;
    }

    public static void RemoveEventListenr(GameObject obj)
    {
        EventListener listener = obj.GetComponent<EventListener>();
        if (listener != null)
            Destroy(listener);
    }

}
