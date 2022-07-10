using System.Collections;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up loot
    /// </summary>
    public class Loot : MonoBehaviour

    {
        private readonly int _pickedHash = Animator.StringToHash("Picked");

        [SerializeField] private MeshRenderer _lootRender;
        [Header("Animator can be null")]
        [SerializeField] private Animator _animator;

        private LootAction _lootAction;
        private bool _isPicked;


        private void Awake()
        {
            _lootAction = GetComponent<LootAction>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                if (_isPicked)
                {
                    return;
                }
                _isPicked = true;

                Animate();
                _lootAction.Execute(collider);
                _lootRender.enabled = false;
                
                StartCoroutine(StartDestroyTimer());
            }
        }

        private void Animate()
        {
            if (_animator != null)
            {
                _animator.SetTrigger(_pickedHash);
            }
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1);
            
            Destroy(gameObject);
        }
    }
}