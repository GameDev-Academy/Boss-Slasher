using IngameStateMachine;

public class BattleState : IState
{
    private UserProfile _userProfile;
    private ConfigurationProvider _configurationProvider;
    
    public BattleState(UserProfile userProfile, ConfigurationProvider configurationProvider)
    {
        _userProfile = userProfile;
        _configurationProvider = configurationProvider;
    }

    public void Dispose()
    {
    }

    public void Initialize(StateMachine stateMachine)
    {
    }

    public void OnEnter()
    {
        // 1. Грузим сцену
        // 2. Предаем в BattleManager наш userProfile
        // 3. Там внутри создаем по этому профилю - какие-то боевые характеристики
        
        // .. GetCharacteristic(Charactestics.Speed);
    }

    public void OnExit()
    {
    }
}