﻿using ConfigurationProviders;
using IngameStateMachine;

public class ShoppingState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    private ISceneLoadingService _sceneLoader;
    
    public ShoppingState(IConfigurationProvider configurationProvider, ISceneLoadingService sceneLoader)
    {
        _configurationProvider = configurationProvider;
        _sceneLoader = sceneLoader;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
   
    public void OnEnter()
    {
        _sceneLoader.LoadSceneAndFind<ShoppingScreen>(SceneNames.WEAPON_MENU_SCENE); 
        //TODO: При нажатии кнопки выхода из Магазина сюда прилетает ивент или сделать колбек на метод OnWeaponShopExitButtonPressed
    }
   
    private void OnWeaponShopExitButtonPressed()
    {
        _stateMachine.Enter<MetaGameState>();
    }
    
    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}