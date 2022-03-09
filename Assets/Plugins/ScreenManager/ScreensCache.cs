#define SCREEN_MANAGER_DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using ScreenManager.Enums;
using ScreenManager.Events;
using ScreenManager.Interfaces;
using SimpleBus.Extensions;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.Assertions;
using Debug = UnityEngine.Debug;

namespace ScreenManager
{
    /// <summary>
    /// It's an intermediate layer between ScreenManager and IScreenLoader,
    /// which just stores references for all loaded screens and preventing
    /// loading twice the same screen, so you don't need to check it at an IScreenLoader.
    /// </summary>
    public class ScreensCache : IDisposable
    {
        private readonly Dictionary<ScreenId, IScreen> _screens = new Dictionary<ScreenId, IScreen>();
        private readonly HashSet<ScreenId> _waitingForLoad = new HashSet<ScreenId>();
        private readonly IScreenLoader _loader;
        private readonly IDisposable _subscriptions;
        private ScreenUnloadingHelper _unloadingHelper;

        public ScreensCache(IScreenLoader screenLoader)
        {
            _loader = screenLoader;
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<GetScreenDebugDataEvent>(GetScreenDebugDataHandler),
            };
        }

        private void GetScreenDebugDataHandler(GetScreenDebugDataEvent getScreenDebugData)
        {
            getScreenDebugData.ScreensCache = _screens;
        }

        public void LoadScreen(ScreenId id, bool showLoadingIndicator = true)
        {
            if (_waitingForLoad.Contains(id))
            {
                Log($"Screen {id} is already waiting for load.");
                return;
            }
            
            Log($"Start LoadScreen = {id}. Has loaded screen : {_screens.ContainsKey(id)}.");
            _waitingForLoad.Add(id);

            if (!_screens.ContainsKey(id))
            {
                _loader.StartLoad(id, showLoadingIndicator);
            }
            else
            {
                var screen = _screens[id];
                ScreenLoadedEvent.Create(screen, id).Publish(EventStreams.UserInterface);
            }
        }

        public IScreen GetScreen(ScreenId id)
        {
            Assert.IsTrue(_screens.ContainsKey(id), $"Screen {id} isn't loaded.");

            return _screens[id];
        }

        /// <summary>
        /// HACK to fix issue when destroyed scene is still alive in the cache
        /// </summary>
        /// <param name="id"></param>
        public AsyncOperation ForceUnload(ScreenId id)
        {
            AsyncOperation result = null;
            _waitingForLoad.Remove(id);

            if (IsLoaded(id))
            {
                var screen = GetScreen(id);
                result = _loader.Unload(screen);
            }
            
            _screens.Remove(id);
            return result;
        }

        public void Unload(ScreenId id)
        {
            Log($"Unload screen with id - {id}");
            
            // We cannot unload active screens
            if (!_unloadingHelper.IsAllowedToUnload(id))
            {
                _waitingForLoad.Remove(id);
                Debug.Log($"Unloading scene for screen {id} was skipped, it's in the appearing queue or still active in stack");
                return;
            }

            if (IsLoaded(id))
            {
                var screen = GetScreen(id);
                _loader.Unload(screen);
            }
            else
            {
                _waitingForLoad.Remove(id);
            }
            
            _screens.Remove(id);
        }

        public bool IsLoaded(ScreenId id)
        {
            return _screens.ContainsKey(id);
        }

        public void OnScreenLoaded(ScreenLoadedEvent eventData)
        {
            var screen = eventData.Screen;
            Log($"OnScreenLoaded - {eventData.Id}");

            if (!_waitingForLoad.Contains(eventData.Id) && 
                _unloadingHelper.IsAllowedToUnload(eventData.Id))
            {
                Log($"Loading was canceled. Will unload the scene for the screen {eventData.Id}");
                Unload(eventData.Id);
            }
            else
            {
                if (!_screens.ContainsKey(eventData.Id))
                {
                    _screens[eventData.Id] = screen;
                }

                _waitingForLoad.Remove(eventData.Id);
            }
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
            _loader.Dispose();
        }

        [Conditional("SCREEN_MANAGER_DEBUG")]
        private void Log(string message)
        {
            Debug.Log(message);
        }

        public void Clear()
        {
             _waitingForLoad.Clear();
        }

        public void SetUnloadingHelper(ScreenUnloadingHelper unloadingHelper)
        {
            _unloadingHelper = unloadingHelper;
        }
    }
}
