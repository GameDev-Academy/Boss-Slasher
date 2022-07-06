using System.Collections;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up loot
    /// </summary>
    public class Loot : MonoBehaviour

    {
        private readonly int _isPicked = Animator.StringToHash("Picked");

        [SerializeField] private GameObject _lootRender;

        private Animator _animator;
        private LootAction _lootAction;
        private bool _picked;


        private void Awake()
        {
            _lootAction = GetComponent<LootAction>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                if (_picked)
                {
                    return;
                }
                _picked = true;
                
                _animator.SetTrigger(_isPicked);
                _lootAction.Execute();
                _lootRender.SetActive(false);
                
                StartCoroutine(StartDestroyTimer());
            }
        }
        
        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1);
            
            Destroy(gameObject);
        }
    }
}