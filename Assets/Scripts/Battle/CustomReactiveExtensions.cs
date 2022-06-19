using System;
using UniRx;

public static class CustomReactiveExtensions
{
    public static IDisposable SubscribeWhenTrue(this IObservable<bool> observable, Action action)
    {
        return observable.Where(value => value).Subscribe(_ => action?.Invoke());
    }
}