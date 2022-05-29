using System;
using System.Collections.Generic;

public class ServiceLocator : IDisposable
{
    public static ServiceLocator Instance { get; private set; }

    private readonly Dictionary<Type, IService> _services = new();

    public ServiceLocator()
    {
        Instance = this;
    }

    public void RegisterSingle<TService>(TService service) where TService : IService
    {
        _services[typeof(TService)] = service;
    }

    public TService GetSingle<TService>() where TService : class, IService
    {
        return _services[typeof(TService)] as TService;
    }

    public void Dispose()
    {
        foreach (IService service in _services.Values)
        {
            if (service is IDisposable disposableService)
            {
                disposableService.Dispose();
            }
        }
    }
}