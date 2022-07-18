using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up loot
    /// </summary>
    public class Loot : MonoBehaviour

    {
        private readonly int _pickedHash = Animator.StringToHash("Picked");

        [Header("Animator can be null")] [SerializeField]
        private Animator _animator;

        private LootAction[] _lootActions;
        private bool _isPicked;


        private void Awake()
        {
            _lootActions = GetComponents<LootAction>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (_isPicked)
            {
                return;
            }

            if (collider.CompareTag(Tags.PLAYER))
            {
                _isPicked = true;

                PlayAnimation();
                ExecuteActions();
                Destroy(gameObject);
            }
        }

        private void PlayAnimation()
        {
            if (_animator != null)
            {
                _animator.SetTrigger(_pickedHash);
            }
        }

        private void ExecuteActions()
        {
            for (int i = 0; i < _lootActions.Length; i++)
            {
                _lootActions[i].Execute();
            }
        }
    }
}