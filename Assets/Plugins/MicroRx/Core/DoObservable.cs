using System;

namespace MicroRx.Core
{
    public static partial class Observable
    {
        public static IDisposable Do<T>(this IObservable<T> observable, Action onNext, Action onCompleted = null)
        {
            var actionObserver = new DoObserver<T>(onNext, onCompleted);
            return observable.Subscribe(actionObserver);
        }

        public class DoObserver<T> : IObserver<T>
        {
            private Action _onNext;
            private Action _onCompleted;

            public DoObserver(Action onNext, Action onCompleted)
            {
                _onNext = onNext;
                _onCompleted = onCompleted;
            }

            public void OnCompleted() => _onCompleted?.Invoke();

            public void OnError(Exception error) => throw error;

            public void OnNext(T value) => _onNext?.Invoke();
        }
    }
}