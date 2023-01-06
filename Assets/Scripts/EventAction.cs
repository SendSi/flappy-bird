using UnityEngine.Events;

public interface IEventAction { }
public class EventOne<T> : IEventAction
{
    public UnityAction<T> actions;
    public EventOne(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventTwo<T1, T2> : IEventAction
{
    public UnityAction<T1, T2> actions;
    public EventTwo(UnityAction<T1, T2> action)
    {
        actions += action;
    }
}