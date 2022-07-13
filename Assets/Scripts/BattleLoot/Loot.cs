using System.Collections.Generic;
using ScreenManager;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up loot
    /// </summary>
    public class Loot : MonoBehaviour

    {
        private readonly int _pickedHash = Animator.StringToHash("Picked");

        [SerializeField] private List<MeshRenderer> _meshRenderers;
        [SerializeField] private float _destroyTimeDelay = 1f;

        [Header("Animator can be null")] [SerializeField]
        private Animator _animator;

        private LootAction _lootAction;
        private bool _isPicked;


        private void Awake()
        {
            _lootAction = GetComponent<LootAction>();
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

                if (_animator != null)
                {
                    _animator.SetTrigger(_pickedHash);
                }

                _lootAction.Execute();

                _meshRenderers.ForEach(meshRenderer => meshRenderer.enabled = false);

                this.DoAfterDelay(() => Destroy(gameObject), _destroyTimeDelay);
            }
        }
    }
}