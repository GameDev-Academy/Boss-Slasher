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
    
    private CompositeDisposable _subscriptions;
    private IConfigurationProvider _configurationProvider;
    private ICharacteristicsService _characteristicsService;
    private BattleCharacteristicsManager _battleCharacteristicsManager;
    
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

    //TODO: Внутри создаем по этому профилю - боевые характеристики
    // .. GetCharacteristic(Charactestics.Speed);
}