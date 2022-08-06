using TMPro;
using UnityEngine;

namespace BattleLoot
{
    public class Popup : MonoBehaviour
    {
        public TextMeshPro Text { get; private set; }

        private void Awake()
        {
            Text = GetComponent<TextMeshPro>();
        }
    }
}