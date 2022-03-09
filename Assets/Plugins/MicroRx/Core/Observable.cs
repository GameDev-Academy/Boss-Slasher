using System;

namespace MicroRx.Core
{
    public static partial class Observable
    {
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext, Action onCompleted = null)
        {
            var actionObserver = new ActionObserver<T>(onNext, onCompleted);
            return observable.Subscribe(actionObserver);
        }
        
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T, T> onNext, Action onCompleted = null)
        {
            var actionObserver = new ActionObserver<T>(onNext, onCompleted);
            return observable.Subscribe(actionObserver);
        }

        private class ActionObserver<T> : IObserver<T>
        {
            private readonly Action<T, T> _onNext;
            private readonly Action _onCompleted;
            private T _prevValue;

            public ActionObserver(Action<T,T> onNext, Action onCompleted)
            {
                _onNext = onNext;
                _onCompleted = onCompleted;
            }
            
            public ActionObserver(Action<T> onNext, Action onCompleted)
            {
                _onNext = (prevValue, currentValue) => onNext(currentValue);
                _onCompleted = onCompleted;
            }

            public void OnCompleted() => _onCompleted?.Invoke();

            public void OnError(Exception error) => throw error;

            public void OnNext(T value)
            {
                _onNext?.Invoke(_prevValue, value);
                _prevValue = value;
            }
        }
    }
}
