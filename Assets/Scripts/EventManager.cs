using System.Collections.Generic;
using UnityEngine;
public enum EventType
{
    OnPlayerDead,
    OnEnemyDead,
}
public class EventManager : SingletonManagers<EventManager>
{
    public delegate void OnEvent(EventType type, Component sender, params object[] args);
    private Dictionary<EventType, List<OnEvent>> listeners = new();

    public void AddListener(EventType type, OnEvent callback)
    {
        if (!listeners.ContainsKey(type))
        {
            listeners.Add(type, new List<OnEvent>());
        }
        listeners[type].Add(callback);
    }
    public void RemoveListener(EventType type, OnEvent callback)
    {
        if (!listeners.ContainsKey(type))
        {
            return;
        }
        listeners[type].Remove(callback);
    }
    public void Notification(EventType type, Component sender, params object[] args)
    {
        if (!listeners.ContainsKey(type)) return;
        foreach (OnEvent onEvent in listeners[type])
        {
            onEvent(type, sender, args);
        }
    }
}