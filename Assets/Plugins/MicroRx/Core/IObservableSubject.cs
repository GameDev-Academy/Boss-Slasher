using System;

namespace MicroRx.Core
{
    public interface IObservableSubject<T> : IObservable<T>, IObserver<T>
    {
        T CurrentValue { get; set; }
    }
}
