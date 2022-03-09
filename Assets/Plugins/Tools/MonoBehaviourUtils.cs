using System;
using System.Collections;
using UnityEngine;

namespace ScreenManager
{
    public static class MonoBehaviourUtils
    {
        public static void DoAfterRealtimeDelay(this MonoBehaviour monoBehaviour, Action action, float delay)
        {
            monoBehaviour.StartCoroutine(DoAfterRealtimeDelay(action, delay));
        }
        
        public static void DoAfterDelay(this MonoBehaviour monoBehaviour, Action action, float delay)
        {
            monoBehaviour.StartCoroutine(DoAfterDelay(action, delay));
        }

        public static void DoFrameLater(this MonoBehaviour monoBehaviour, Action action)
        {
            monoBehaviour.StartCoroutine(DoFrameLater(action));
        }

        public static void DoEndOfFrame(this MonoBehaviour monoBehaviour, Action action)
        {
            monoBehaviour.StartCoroutine(DoEndOfFrame(action));
        }

        public static Coroutine Await(this MonoBehaviour monoBehaviour, Func<bool> condition, Func<bool> cancelCondition, Action callback)
        {
            return monoBehaviour.StartCoroutine(Await(condition, cancelCondition, callback));
        }

        private static IEnumerator Await(Func<bool> condition, Func<bool> cancelCondition, Action callback)
        {
            while (true)
            {
                try
                {
                    if (cancelCondition != null && cancelCondition.Invoke())
                    {
                        yield break;
                    }

                    if (condition == null || condition.Invoke())
                    {
                        break;
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.Message);
                    yield break;
                }

                yield return null;
            }

            callback?.Invoke();
        }

        private static IEnumerator DoAfterRealtimeDelay(Action action, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            action?.Invoke();
        }
        
        private static IEnumerator DoAfterDelay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        private static IEnumerator DoFrameLater(Action action)
        {
            yield return null;
            action?.Invoke();
        }
        
        private static IEnumerator DoEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }

        public static T GetComponentAlways<T>(this MonoBehaviour monoBehaviour) where T : MonoBehaviour
        {
            var result = monoBehaviour.GetComponent<T>();
            if (result == null)
            {
                result = monoBehaviour.gameObject.AddComponent<T>();
            }

            return result;
        }
    }
}