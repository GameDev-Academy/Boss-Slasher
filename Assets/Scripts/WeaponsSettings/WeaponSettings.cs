using System;
using UnityEngine;

namespace WeaponsSettings
{
    [Serializable]
    public class WeaponSettings
    {
        public string Id { get; set; }
        public GameObject Prefab => _prefab;
        public int Cost => _cost;

        [SerializeField] 
        private GameObject _prefab;

        [SerializeField] 
        private int _cost;
    }
}