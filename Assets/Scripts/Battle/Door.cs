using UnityEngine;

namespace Battle
{
    public class Door : MonoBehaviour
    {
        private Animator _animator;
        private readonly int _isOpenedHash = Animator.StringToHash("IsOpened");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void IsOpen(bool value)
        {
            _animator.SetBool(_isOpenedHash, value);
        }
    }
}