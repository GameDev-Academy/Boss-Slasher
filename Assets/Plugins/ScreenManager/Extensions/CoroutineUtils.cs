using System;
using System.Collections;
using UnityEngine;

namespace ScreenManager.Extensions
{
    public static class CoroutineUtils
    {
        public static Coroutine StartThrowingTimeLimitedCoroutine(
            this MonoBehaviour coroutineHolder,
            IEnumerator enumerator, 
            Action<Exception> onException,
            float timeLimit = float.MaxValue)
        {
            return coroutineHolder.StartCoroutine(RunThrowingTimeLimitedIterator(enumerator, onException, timeLimit));
        }

        private static IEnumerator RunThrowingTimeLimitedIterator(
            IEnumerator enumerator,
            Action<Exception> onException, 
            float timeLimit = float.MaxValue)
        {
            while (timeLimit > 0)
            {
                object current;

                try
                {
                    if (enumerator.MoveNext() == false)
                    {
                        break;
                    }

                    current = enumerator.Current;
                }
                catch (Exception exception)
                {
                    onException?.Invoke(exception);
                    yield break;
                }

                yield return current;

                timeLimit -= Time.deltaTime;
            }

            if (timeLimit < 0)
            {
                onException?.Invoke(new Exception($"TimeLimit {timeLimit} (sec) was reached. Iterator has been stopped."));
            }
        }
    }
}