using System;

namespace MicroRx.Core
{
    public static partial class Observable
    {
        public static IObservable<T> Where<T>(this IObservable<T> observable, Predicate<T> predicate, Action onCompleted = null)
        {
            var whereObservable = new WhereOperator<T>(observable, predicate, onCompleted);
            return whereObservable;
        }

        public class WhereOperator<T> : IObservable<T>
        {
            private IObservable<T> _observable;
            private Predicate<T> _predicate;
            private Action _onCompleted;

            public WhereOperator(IObservable<T> observable, Predicate<T> predicate, Action onCompleted)
            {
                _onCompleted = onCompleted;
                _predicate = predicate;
                _observable = observable;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                return _observable.Subscribe(new WhereObserver<T>(observer, _predicate, _onCompleted));
            }
        }

        private class WhereObserver<T> : IObserver<T>
        {
            private Predicate<T> _predicate;
            private Action _onCompleted;
            private IObserver<T> _observer;

            public WhereObserver(IObserver<T> observer, Predicate<T> predicate, Action onCompleted)
            {
                _observer = observer;
                _predicate = predicate;
                _onCompleted = onCompleted;
            }

            public void OnCompleted() => _onCompleted?.Invoke();

            public void OnError(Exception error) => throw error;

            public void OnNext(T value)
            {
                if (_predicate?.Invoke(value) == true)
                {
                    _observer.OnNext(value);
                }
            }
        }
    }
}
