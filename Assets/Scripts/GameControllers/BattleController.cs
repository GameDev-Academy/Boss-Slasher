using ConfigurationProviders;
using Events;
using UniRx;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _looseScreen;
    
    private IConfigurationProvider _configurationProvider;
    private CompositeDisposable _subscriptions;

    private void Start()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<LevelPassEvent>(LevelPassHandler)
        };
    }

    public void Initialize(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
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