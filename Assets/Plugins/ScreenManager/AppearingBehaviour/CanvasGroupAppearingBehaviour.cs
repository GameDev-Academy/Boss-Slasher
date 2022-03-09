using System.Collections;
using MicroRx.Core;
using ScreenManager.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace ScreenManager
{
    public class CanvasGroupAppearingBehaviour : IAppearingScreenBehaviour
    {
        private readonly GameObject _gameObject;
        private readonly CanvasGroup _canvasGroup;

        public CanvasGroupAppearingBehaviour(GameObject gameObject)
        {
            _gameObject = gameObject;
            _canvasGroup = _gameObject.AddComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        public IEnumerator SetActiveAsync(bool state)
        {
            var duration = .5f;
            var time = 0f;

            while (time <= duration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(state ? 0 : 1, state ? 1 : 0, time / duration);
                yield return null;
            }
        }
    }
}
