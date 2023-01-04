using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class EventDispatcherGeneric<T, E>
{
    protected int IDNum = 0;
    public delegate void EventDelegate(E args);//消息传递系统标准委托

    protected int mInternalObject = 0;//默认的键值，这个东西估计是为了以后扩展分布式的，直接留着吧，现在用不着

    protected Dictionary<object, Dictionary<T, ArrayList>> mEventsWithObject = new Dictionary<object, Dictionary<T, ArrayList>>();

    public void RegisterEvent(T evt, EventDelegate callback)
    {
        RegisterEvent(mInternalObject, evt, callback);
    }

    public void FireEvent(T evt, E args)
    {
        FireEvent(mInternalObject, evt, args);
    }
    public void UnregisterEvent(T evt, EventDelegate callback)
    {
        UnregisterEvent(mInternalObject, evt, callback);
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="evt">事件类型MLLogicEventType</param>
    /// <param name="callback">回调函数位置</param>
    public void RegisterEvent(object obj, T evt, EventDelegate callback)
    {
        Dictionary<T, ArrayList> events = null;

        //	添加锁确保同时只有一个线程访问存储事件列表
        //	同一台机器上已经注册事件列表
        try
        {
            Monitor.Enter(mEventsWithObject);
            IDNum++;
            if (mEventsWithObject.TryGetValue(obj, out events))
            {
                ArrayList del = null;
                if (events.TryGetValue(evt, out del))
                {
                    del.Add(callback);
                }
                else
                {
                    del = new ArrayList();
                    del.Add(callback);
                    events.Add(evt, del);
                }
            }
            else
            {
                //同一个机器上没有注册事件列表
                events = new Dictionary<T, ArrayList>();//注册事件列表
                ArrayList del = new ArrayList();
                del.Add(callback);//将事件放入该类型列表中
                events.Add(evt, del);//每种事件分别存放
                mEventsWithObject.Add(obj, events);//加入当前机器存放事件列表
            }
        }
        finally
        {
            Monitor.Exit(mEventsWithObject);
        }
    }

    public void FireEvent(object obj, T evt, E args)
    {
        Dictionary<T, ArrayList> events = null;
        try
        {
            Monitor.Enter(mEventsWithObject);
            if (mEventsWithObject.TryGetValue(obj, out events))
            {
                ArrayList del = null;
                if (events.TryGetValue(evt, out del))
                {
                    foreach (EventDelegate evtdel in del)
                    {
                        //执行委托方法,客户端注册过的回调方法都会调用一次，并将参数args发送过去
                        evtdel(args);
                    }
                }
            }
        }
        finally
        {
            Monitor.Exit(mEventsWithObject);
        }
    }

    public void UnregisterEvent(object obj, T evt, EventDelegate callback)
    {
        Dictionary<T, ArrayList> events = null;
        try
        {
            Monitor.Enter(mEventsWithObject);

            if (mEventsWithObject.TryGetValue(obj, out events))
            {
                ArrayList del = null;
                if (events.TryGetValue(evt, out del))
                {
                    events.Remove(evt);//因为每个注册事件目前没有KEY，所以无法删除具体每条事件，只能将同一个类型事件整体删除
                }
            }
        }
        finally
        {
            Monitor.Exit(mEventsWithObject);
        }
    }

    public void UnregisterAllEvent()
    {
        mEventsWithObject.Clear();
    }
}