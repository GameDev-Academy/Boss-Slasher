using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for the flight of coins on the canvas
    /// </summary>
    public class LootParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _pickupFxPrefab;
        
        
        private void PlayFX()
        {
            if (_pickupFxPrefab != null)
            {
                _pickupFxPrefab.Play();
            }
        }
    }
}