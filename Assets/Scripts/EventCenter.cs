using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class EventCenter : BaseSingle<EventCenter>
{
    private readonly Dictionary<string, UnityAction> eventNonDic = new Dictionary<string, UnityAction>();
    public void Bind(string name, UnityAction action)
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
    public void Fire(string name)
    {
        if (eventNonDic.ContainsKey(name))
        {
            eventNonDic[name]?.Invoke();
        }
    }
    public void UnBind(string name, UnityAction action)
    {
        if (eventNonDic.ContainsKey(name))
        {
            eventNonDic[name] -= action;
        }
    }

    private readonly Dictionary<string, IEventAction> eventActionDic = new Dictionary<string, IEventAction>();
    public void Bind<T>(string name, UnityAction<T> action)
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

    internal void Fire(object eN_gameOver)
    {
        throw new NotImplementedException();
    }

    public void Fire<T>(string name, T obj)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventOne<T>).actions?.Invoke(obj);
        }
    }
    public void UnBind<T>(string name, UnityAction<T> action)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventOne<T>).actions -= action;
        }
    }

    public void Bind<T1, T2>(string name, UnityAction<T1, T2> action)
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
    public void UnBind<T1, T2>(string name, UnityAction<T1, T2> action)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventTwo<T1, T2>).actions -= action;
        }
    }
    public void Fire<T1, T2>(string name, T1 t1, T2 t2)
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
    public const string EN_updateScore = "EN_updateScore";
    public const string EN_gameOver = "EN_gameOver";
    public const string EN_getLife = "EN_getLife";
}