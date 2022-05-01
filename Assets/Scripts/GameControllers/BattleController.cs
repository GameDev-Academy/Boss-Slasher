using UnityEngine;
using Events;
using UniRx;
using User;
using ConfigurationProviders;
using BattleCharacteristics;

/// <summary>
/// BattleController - класс, который отвечает за старт боевой части игры.
/// Получает характеристики из MetaState и инстанциирует игрока с заданными характеристиками
/// </summary>
public class BattleController : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _looseScreen;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private TargetFollowingCamera _camera;    
    
    private CompositeDisposable _subscriptions;
    private BattleCharacteristicsManager _battleCharacteristicsManager;
    private Player _player;
    
    private void Start()
    {
        var configurationProvider = ServiceLocator.Instance.GetSingle<IConfigurationProvider>();
        var characteristicsService = ServiceLocator.Instance.GetSingle<ICharacteristicsService>();
        _battleCharacteristicsManager = new BattleCharacteristicsManager(configurationProvider, characteristicsService);


        var inputService = ServiceLocator.Instance.GetSingle<IInputService>();
        _player = Instantiate(_playerPrefab, _playerStartPosition.position, Quaternion.identity);
        _player.Initialize(inputService, _battleCharacteristicsManager);
        _camera.SetTarget(_player.transform);
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<LevelPassEvent>(LevelPassHandler)
        };
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
    
    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}