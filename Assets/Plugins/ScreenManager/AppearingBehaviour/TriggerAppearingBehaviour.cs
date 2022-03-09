using System.Collections;
using ScreenManager.Interfaces;
using UnityEngine;

namespace ScreenManager
{
    public class TriggerAppearingBehaviour : MonoBehaviour, IAppearingScreenBehaviour
    {
        [SerializeField]
        private string _showTrigger = "show";
        [SerializeField]
        private string _hideTrigger = "hide";
        
        [SerializeField] 
        private float _animationDuration = .5f;

        [SerializeField]
        private Animator _animator;

        public IEnumerator SetActiveAsync(bool state)
        {
            while (!_animator.isInitialized || !_animator.isActiveAndEnabled)
            {
                yield return null;
            }

            _animator.SetTrigger(state ? _showTrigger : _hideTrigger);

            yield return new WaitForSeconds(_animationDuration);
        }
    }
}