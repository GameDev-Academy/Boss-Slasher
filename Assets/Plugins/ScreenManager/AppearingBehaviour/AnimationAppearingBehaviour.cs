using System.Collections;
using MicroRx.Core;
using ScreenManager.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace ScreenManager
{
    public class AnimationAppearingBehaviour : MonoBehaviour, IAppearingScreenBehaviour
    {
        [SerializeField]
        private string _keyName = "show";
        
        [FormerlySerializedAs("_hideAnimationDuration")] 
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

            _animator.SetBool(_keyName, state);

            yield return new WaitForSeconds(_animationDuration);
        }
    }
}
