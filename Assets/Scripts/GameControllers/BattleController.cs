using BattleCharacteristics;
using ConfigurationProviders;
using UnityEngine;
using User;

/// <summary>
/// BattleController - класс, который отвечает за старт боевой части игры.
/// Получает характеристики из MetaState и инстанциирует игрока с заданными характеристиками
/// </summary>
public class BattleController : MonoBehaviour
{
    private IConfigurationProvider _configurationProvider;
    private ICharacteristicsService _characteristicsService;
    private BattleCharacteristicsManager _battleCharacteristicsManager;
    
    public void Initialize(IConfigurationProvider configurationProvider, ICharacteristicsService characteristicsService)
    {
        _configurationProvider = configurationProvider;
        _characteristicsService = characteristicsService;

        _battleCharacteristicsManager =
            new BattleCharacteristicsManager(_configurationProvider, _characteristicsService);
    }
    
    //TODO: Внутри создаем по этому профилю - боевые характеристики
    // .. GetCharacteristic(Charactestics.Speed);
}