using UnityEngine;
using Events;
using UniRx;
using User;
using ConfigurationProviders;
using BattleCharacteristics;
using BattleLoot;
using Player;
using ScreenManager;

namespace GameControllers
{
    /// <summary>
    /// The class is responsible for starting the combat part of the game.
    /// Gets the stats from MetaState and create player with the given stats
    /// </summary>
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _looseScreen;
        [SerializeField] private float _delayLoseScreen = 1f;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private TargetFollowingCamera _camera;
        [SerializeField] private BattleServiceInstaller _serviceInstaller;
        [SerializeField] private PlayerHealthPresenter _playerHealthPresenter;

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
            _playerHealthPresenter.Initialize(player.GetComponent<PlayerHealth>());
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
            ServiceLocator.Instance.GetSingle<IBattleWeaponService>().ResetWeapon();

            if (eventData.IsDungeonPassed)
            {
                _winScreen.SetActive(true);

                _moneyService.Receive(_dungeonMoney.Money.Value);
            }
            else
            {
                this.DoAfterDelay(() => { _looseScreen.SetActive(true); }, _delayLoseScreen);
            }
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}