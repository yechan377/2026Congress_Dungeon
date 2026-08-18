using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    OnPlayerDead,
    OnEnemyDead,
    OnHpChanged,
    OnMpChanged,
    OnGoldChanged,
    OnExpChanged,
    OnLevelUp,
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

/*
    add 어떤 이벤트 타입에 저장할 건지에 대한 변수와
    어떤 함수로 받을 건지에 대한 변수를 받음
    받은 이벤트 타입을 key로 하는 value가 없으면 new로 만들어서 할당
    아니면 그냥 할당

    remove 어떤 이벤트 타입에서 뺄건지에 대한 변수와
    어떤 함수를 뺄건지에 대한 변수를 받음 
    받은 이벤트 타입을 key로 하는 value가 없으면 return
    아니면 remove

    notificatiion 어떤 이벤트에 대한건지 누가 보낸건지 어떤 값들인지 
    변수로 보냄 type에 대한 value가 없으면 return
    foreach로 onevent 자료형에 등록한 함수들 대입
    대입한 함수 type sender args로 호출 받은 쪽에서 이 데이터들로 구현
 */
