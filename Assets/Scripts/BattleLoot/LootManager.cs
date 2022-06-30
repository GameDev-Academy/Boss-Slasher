using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace BattleLoot
{
    public class LootManager : MonoBehaviour
    {
        [SerializeField] private List<LootPiece> _loots;
        [SerializeField] private int _minLootValue = 10;
        [SerializeField] private int _maxLootValue = 100;


        private void Start()
        {
            GenerateLoot();
        }

        
        private void GenerateLoot()
        {
            var random = new Random();

            foreach (var loot in _loots)
            {
                loot.Initialize(new Loot
                {
                    Value = random.Next(_minLootValue, _maxLootValue)
                });
            }
        }
    }
}