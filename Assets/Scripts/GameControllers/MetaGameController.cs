﻿using ConfigurationProviders;
using Events;
using UnityEngine;
using UpgradeButtons;
using User;

namespace GameControllers
{
    /// <summary>
    /// Инициализирует UpgradeButtonsView и UserMoneyPresenter сервисами
    /// </summary>
    public class MetaGameController : MonoBehaviour
    {
        [SerializeField] 
        private UpgradeButtonsView _upgradeButtonsView;

        [SerializeField] 
        private UserMoneyPresenter _userMoneyPresenter;

        public void Initialize(
            IConfigurationProvider configurationProvider,
            ICharacteristicsService characteristicsService, 
            IMoneyService moneyService)
        {
            _upgradeButtonsView.Initialize(configurationProvider, characteristicsService, moneyService);
            _userMoneyPresenter.Initialize(moneyService);
        }

        public void OpenShop()
        {
            EventStreams.UserInterface.Publish(new OpenShopEvent());
        }

        public void StartBattle()
        {
            EventStreams.UserInterface.Publish(new StartBattleEvent());
        }
    }
}