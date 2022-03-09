using System;
using System.Collections;
using System.Collections.Generic;
using ScreenManager.Enums;
using ScreenManager.Events;
using ScreenManager.Interfaces;
using SimpleBus.Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace ScreenManager.Loaders.Scenes
{
    public class SceneAsScreenLoader : IScreenLoader
    {
        private class SceneLoadRequest
        {
            public ScreenId Type;
            public bool ShowLoadingIndicator;
        }

        private readonly IScreenSettingsProvider _loadingSettingsProvider;
        private readonly Dictionary<ScreenId, Action<float>> _setLoadingProgressCallbacks = new Dictionary<ScreenId, Action<float>>();
        private readonly Queue<SceneLoadRequest> _sceneLoadRequests = new Queue<SceneLoadRequest>();
        private bool _isLoadingInProgress;

        public SceneAsScreenLoader(IScreenSettingsProvider loadingSettingsProvider)
        {
            _loadingSettingsProvider = loadingSettingsProvider;
        }

        public void StartLoad(ScreenId type, bool showLoadingIndicator)
        {
            _sceneLoadRequests.Enqueue(new SceneLoadRequest() {Type = type, ShowLoadingIndicator = showLoadingIndicator});

            if (!_isLoadingInProgress)
            {
                LoadNextScene();
            }
        }

        private void LoadNextScene()
        {
            if (_sceneLoadRequests.Count == 0)
            {
                return;
            }

            _isLoadingInProgress = true;

            var request = _sceneLoadRequests.Dequeue();

            var type = request.Type;
            var showLoadingIndicator = request.ShowLoadingIndicator;

            var loadingInformation = _loadingSettingsProvider.Get(type) as SceneScreenSettings;
            Assert.IsNotNull(loadingInformation, $"Settings for screen '{type}' was not found!");

            if (SceneManager.GetSceneByName(loadingInformation.Path).isLoaded)
            {
                OnSceneLoaded(SceneManager.GetSceneByName(loadingInformation.Path), type, loadingInformation);
                return;
            }

            Debug.Log($"LoadNextScene: ScreenId '{type}'. Scene '{loadingInformation.Path}'");

            LoadScene(type, loadingInformation);
        }

        private void LoadScene(ScreenId type, SceneScreenSettings settings)
        {
            CoroutinesHolder.Instance.StartCoroutine(LoadScene(settings.Path, 
                scene => OnSceneLoaded(scene, type, settings), 
                error => OnFailSceneLoading(error, type, settings)));
        }

        private IEnumerator LoadScene(string sceneName, Action<Scene> onSceneLoadedSuccessfully, Action<string> onLoadSceneFailed)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            onSceneLoadedSuccessfully?.Invoke(SceneManager.GetSceneByName(sceneName));
        }

        private void OnFailSceneLoading(string message, ScreenId type, SceneScreenSettings settings)
        {
            Debug.LogError($"Cannot load scene for screen '{type}. Error: {message}");

            if (_setLoadingProgressCallbacks.ContainsKey(type))
            {
                _setLoadingProgressCallbacks[type].Invoke(1);
            }

            new FailScreenLoadingEvent(type).Publish(EventStreams.UserInterface);
            _isLoadingInProgress = false;

            LoadNextScene();
        }

        private void OnSceneLoaded(Scene scene, ScreenId type, SceneScreenSettings settings)
        {
            Debug.Log($"Scene loaded {scene.name}");

            IScreen sceneScreen = null;
            try
            {
                sceneScreen = SceneUtils.GetAComponentInSceneChildren<IScreen>(scene);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }

            if (sceneScreen == null)
            {
                OnFailSceneLoading("bad configuration", type, settings);
                return;
            }

            ((IScreenIdSetter)sceneScreen).Id = type;

            if (_setLoadingProgressCallbacks.ContainsKey(type))
            {
                _setLoadingProgressCallbacks[type].Invoke(1);
            }

            ScreenLoadedEvent.Create(sceneScreen, type).Publish(EventStreams.UserInterface);

            _isLoadingInProgress = false;
            LoadNextScene();
        }


        public AsyncOperation Unload(IScreen screen)
        {
            var path = _loadingSettingsProvider.Get(screen.Id).Path;

            var scene = SceneManager.GetSceneByName(path);
            if (!scene.isLoaded)
            {
                return null;
            }

            var result = SceneManager.UnloadSceneAsync(scene);

            Debug.Log($"Unloaded scene {scene.name}");
            return result;
        }

        public void Dispose()
        {
        }
    }
}
