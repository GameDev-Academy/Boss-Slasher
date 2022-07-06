using System;
using UnityEngine;
using Events;
using UniRx;
using User;
using ConfigurationProviders;
using BattleCharacteristics;
using BattleLoot;

/// <summary>
/// BattleController - класс, который отвечает за старт боевой части игры.
/// Получает характеристики из MetaState и инстанциирует игрока с заданными характеристиками
/// </summary>
namespace GameControllers
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _looseScreen;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private TargetFollowingCamera _camera;
        [SerializeField] private BattleServiceInstaller _serviceInstaller;

        private CompositeDisposable _subscriptions;
        private BattleCharacteristicsManager _battleCharacteristicsManager;
        private IMoneyService _moneyService;
        private IDungeonMoneyService _dungeonMoney;

        private void Awake()
        {
            _serviceInstaller.RegisterService();
        }

        private void Start()
        {
            var configurationProvider = ServiceLocator.Instance.GetSingle<IConfigurationProvider>();
            var characteristicsService = ServiceLocator.Instance.GetSingle<ICharacteristicsService>();
            _battleCharacteristicsManager =
                new BattleCharacteristicsManager(configurationProvider, characteristicsService);

            _moneyService = ServiceLocator.Instance.GetSingle<IMoneyService>();
            _dungeonMoney = ServiceLocator.Instance.GetSingle<IDungeonMoneyService>();

            var gameFactory = ServiceLocator.Instance.GetSingle<IGameFactory>();
            var player = gameFactory.CreatePlayer(_playerStartPosition.position, _battleCharacteristicsManager);

            _camera.SetTarget(player.transform);

            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<DungeonPassEvent>(DungeonPassHandler)
            };
        }

        public void OpenMetaGame()
        {
            EventStreams.UserInterface.Publish(new OpenMetaGameEvent());
        }


        private void DungeonPassHandler(DungeonPassEvent eventData)
        {
            if (eventData.IsDungeonPassed)
            {
                //TODO: Показать экран победы и там есть кнопка перехода дальше, в ней уже будем обращаться к стейту
                _winScreen.SetActive(true);

                _moneyService.Receive(_dungeonMoney.Money.Value);
            }
            else
            {
                //TODO: Показать экран проигрыша и там есть кнопка в ней уже будем обращаться к стейту
                _looseScreen.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}