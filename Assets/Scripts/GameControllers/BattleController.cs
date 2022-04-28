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
    private IConfigurationProvider _configurationProvider;
    private ICharacteristicsService _characteristicsService;
    private BattleCharacteristicsManager _battleCharacteristicsManager;
    private Player _player;
    private IInputService _inputService;
    
    private void Start()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<LevelPassEvent>(LevelPassHandler)
        };
    }

    public void Initialize(IConfigurationProvider configurationProvider, ICharacteristicsService characteristicsService)
    {
        _configurationProvider = configurationProvider;
        _characteristicsService = characteristicsService;

        _battleCharacteristicsManager =
            new BattleCharacteristicsManager(_configurationProvider, _characteristicsService);
        
        _inputService = new JoystickInput();
        _inputService.Init();
        
        _player = Instantiate(_playerPrefab, _playerStartPosition.position, Quaternion.identity);
        _player.Initialize(_inputService, _battleCharacteristicsManager);
        _camera.SetTarget(_player.transform);
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