using System.Collections;
using TMPro;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up loot
    /// </summary>
    public class LootPiece : MonoBehaviour
    {
        private const string Player = "Player";

        [SerializeField] private ParticleSystem _pickupFxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;
        [SerializeField] private GameObject _coin;

        private Loot _loot;
        private bool _picked;
        private ILootDataService _lootData;

        private void Awake()
        {
            _lootData = ServiceLocator.Instance.GetSingle<ILootDataService>();
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Player))
            {
                Pickup();
            }
        }

        private void Pickup()
        {
            if (_picked)
            {
                return;
            }
            _picked = true;
            
            UpdateData();
            HideLoot();
            PlayPickupFX();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateData()
        {
            _lootData.Collect(_loot.Value);
        }

        private void HideLoot()
        {
            _coin.SetActive(false);
        }

        private void PlayPickupFX()
        {
            _pickupFxPrefab.Play();
        }

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickupPopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1);
            
            Destroy(gameObject);
        }
    }
}