using UnityEngine;

namespace Player
{
    public class HealthBar : MonoBehaviour 
    {
        [SerializeField] private GameObject[] _healthIcons;
        [SerializeField] private GameObject[] _shieldIcons;

        
        public void SetStartHealth(int startHealth)
        {
            for (var i = startHealth-1; i < _healthIcons.Length; i++)
            {
                _healthIcons[i].SetActive(true);
            }
        }

        public void SetCurrentHealth(int currentValue)
        {
            if (currentValue <= 0)
            {
                return;
            }
            _healthIcons[currentValue-1].SetActive(false);
        }
    }
}