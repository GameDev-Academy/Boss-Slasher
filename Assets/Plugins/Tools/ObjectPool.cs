using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenManager
{
    public class ObjectPool<T> where T : class, new()
    {
        private readonly Action<T> _reset;
        private readonly Stack<T> _pool;
        private readonly HashSet<T> _usedObjects = new HashSet<T>();

        public ObjectPool(Action<T> reset, int initialCapacity = 16)
        {
            _pool = new Stack<T>(initialCapacity);
            _reset = reset;
        }

        public T Get()
        {
            var item = _pool.Count == 0 ? new T() : _pool.Pop();
            _reset?.Invoke(item);
            _usedObjects.Add(item);
            return item;
        }

        public void Release(T item)
        {
            if (item != null)
            {
                if (_usedObjects.Contains(item))
                {
                    _usedObjects.Remove(item);
                    _pool.Push(item); 
                }
                else
                {
                    Debug.LogWarning("Attempt to release not used item.");
                }
            }
        }
    }
}
