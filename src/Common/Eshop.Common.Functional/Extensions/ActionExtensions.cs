namespace Eshop.Common.Functional;

public static class ActionExtensions
{
    public static Action<TValue> Append<TValue>(this Action<TValue> initialAction, Action<TValue> nextAction)
    {
        return initialAction += nextAction;
    }

    public static Action<TValue> Append<TValue>(this Action<TValue> initialAction, params Action<TValue>[] actions)
    {
        var resultAction = initialAction;

        foreach (var action in actions)
        {
            resultAction += action;
        }

        return resultAction;
    }
}
