using System;
using System.Collections.Generic;
using MicroRx.Disposables;

namespace MicroRx.Core
{
    public class Subject<T> : IObservableSubject<T>
    {
        public T CurrentValue
        {
            get => _currentValue;
            set => OnNext(value);
        }

        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();
        private T _currentValue;

        public Subject(T value)
        {
            _currentValue = value;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            observer.OnNext(_currentValue);
            return new AnonymousDisposable(() => _observers.Remove(observer));
        }

        public void OnCompleted()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            foreach (var observer in _observers)
            {
                observer.OnError(error);
            }
        }

        public void OnNext(T value)
        {
            if (_currentValue != null && _currentValue.Equals(value))
            {
                return;
            }

            _currentValue = value;
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }

        public override string ToString()
        {
            return _currentValue.ToString();
        }
    }
}
