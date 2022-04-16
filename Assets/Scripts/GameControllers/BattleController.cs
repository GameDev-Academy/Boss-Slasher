using BattleCharacteristics;
using ConfigurationProviders;
using Events;
using UniRx;
using UnityEngine;
using User;

namespace GameControllers
{
    /// <summary>
    /// BattleController - класс, который отвечает за старт боевой части игры.
    /// Получает характеристики из MetaState и инстанциирует игрока с заданными характеристиками
    /// </summary>
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _looseScreen;
        [SerializeField] private GameObject _initialPoint;
    
        private CompositeDisposable _subscriptions;
        private IConfigurationProvider _configurationProvider;
        private ICharacteristicsService _characteristicsService;
        private BattleCharacteristicsManager _battleCharacteristicsManager;
        private Player.Player _playerPrefab;

  
        private void Start()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<LevelPassEvent>(LevelPassHandler),
            };
            InstantiatePlayer();
        }
   
        public void Initialize(IConfigurationProvider configurationProvider, ICharacteristicsService characteristicsService,
            Player.Player playerPrefab)
        {
            _configurationProvider = configurationProvider;
            _characteristicsService = characteristicsService;
            _playerPrefab = playerPrefab;
        
            _battleCharacteristicsManager =
                new BattleCharacteristicsManager(_configurationProvider, _characteristicsService);
        }

        private void LevelPassHandler(LevelPassEvent eventData)
        {
            if (eventData.IsLevelPassed)
            {
                //TODO: Показать экран победы и там есть кнопка перехода дальше, в ней уже будем обращаться к стейту
                _winScreen.SetActive(true);
            }
            else
            {
                //TODO: Показать экран проигрыша и там есть кнопка в ней уже будем обращаться к стейту
                _looseScreen.SetActive(true);
            }
        }

        private void InstantiatePlayer()
        { 
            var player = Instantiate(_playerPrefab, _initialPoint.transform.position, Quaternion.identity);
            EventStreams.UserInterface.Publish(new PlayerInstantiatedEvent(player));
        }
        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }

        //TODO: Внутри создаем по этому профилю - боевые характеристики
        // .. GetCharacteristic(Charactestics.Speed);
    }
}