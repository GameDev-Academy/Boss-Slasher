using System;
using UnityEngine;

namespace WeaponsSettings
{
    [Serializable]
    public class WeaponSettings
    {
        public string Id => _id;
        public GameObject Prefab => _prefab;
        public int Cost => _cost;

        [SerializeField] 
        private string _id;
        
        [SerializeField] 
        private GameObject _prefab;

        [SerializeField] 
        private int _cost = 100;
    }
}