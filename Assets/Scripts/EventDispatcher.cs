using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventDispatcher : EventDispatcherGeneric<EventNullType, EventArgs>
{
    private static EventDispatcher _instance;
    public static EventDispatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventDispatcher();
            }
            return _instance;
        }
    }

    public void ResetInstance()
    {
        _instance = null;
    }
}
public enum EventNullType
{
    NONE,

    EVENT_SCORE,
}

public class EventArgs { }
public class EventXXXArgs : EventArgs
{
    public string[] strArr;
}

public class EventNull : EventArgs
{
    public int intValue;
}