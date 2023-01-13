using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class EventCenter : BaseSingle<EventCenter>
{
    private readonly Dictionary<int, UnityAction> eventNonDic = new Dictionary<int, UnityAction>();
    public void Bind(int name, UnityAction action)
    {
        if (eventNonDic.ContainsKey(name))
        {
            eventNonDic[name] += action;
        }
        else
        {
            eventNonDic[name] = action;
        }
    }
    public void Fire(int name)
    {
        if (eventNonDic.ContainsKey(name))
        {
            eventNonDic[name]?.Invoke();
        }
    }
    public void UnBind(int name, UnityAction action)
    {
        if (eventNonDic.ContainsKey(name))
        {
            eventNonDic[name] -= action;
        }
    }

    private readonly Dictionary<int, IEventAction> eventActionDic = new Dictionary<int, IEventAction>();
    public void Bind<T>(int name, UnityAction<T> action)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventOne<T>).actions += action;
        }
        else
        {
            eventActionDic[name] = new EventOne<T>(action);
        }
    }


    public void Fire<T>(int name, T obj)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventOne<T>).actions?.Invoke(obj);
        }
    }
    public void UnBind<T>(int name, UnityAction<T> action)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventOne<T>).actions -= action;
        }
    }

    public void Bind<T1, T2>(int name, UnityAction<T1, T2> action)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventTwo<T1, T2>).actions += action;
        }
        else
        {
            eventActionDic[name] = new EventTwo<T1, T2>(action);
        }
    }
    public void UnBind<T1, T2>(int name, UnityAction<T1, T2> action)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventTwo<T1, T2>).actions -= action;
        }
    }
    public void Fire<T1, T2>(int name, T1 t1, T2 t2)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventTwo<T1, T2>).actions?.Invoke(t1, t2);
        }
    }
    public void Clear()
    {
        eventActionDic.Clear();
        eventNonDic.Clear();
    }

}


public class EventName
{
    public const int EN_updateScore = 10001;
    public const int EN_gameOver = 10002;
    public const int EN_getLife = 10003;
}