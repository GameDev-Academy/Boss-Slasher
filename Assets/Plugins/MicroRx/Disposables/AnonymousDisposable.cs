using System;

namespace MicroRx.Disposables
{
    public class AnonymousDisposable : IDisposable
    {
        private readonly Action _dispose;
        private bool _isDisposed;

        public AnonymousDisposable(Action dispose)
        {
            _dispose = dispose;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _dispose?.Invoke();
            }
        }
    }
}
