using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public struct Events {
    public static void OnEvent(EventTriggerType ett, Component s, UnityAction<BaseEventData> action)
    {
        var trigger = s.gameObject.AddComponent<EventTrigger>();
        var eventType = new EventTrigger.Entry {eventID = ett};
        eventType.callback.AddListener(action);
        trigger.triggers.Add(eventType);
    }
}
