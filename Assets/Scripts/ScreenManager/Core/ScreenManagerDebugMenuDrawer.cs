using System;
using System.Collections.Generic;
using ScreenManager;
using ScreenManager.Enums;
using ScreenManager.Events;
using SimpleBus.Extensions;
using UnityEngine;

public class ScreenManagerDebugMenuDrawer : IDisposable
{
    private int _indentSpace = 20;
    private readonly Dictionary<ScreenId, bool> _queueFoldouts = new Dictionary<ScreenId, bool>();
    private readonly Dictionary<int, bool> _appearingRequestsFoldouts = new Dictionary<int, bool>();
    private readonly IDisposable _subscription;
    private ScreenId _topScreen;

    public ScreenManagerDebugMenuDrawer()
    {
        _subscription = EventStreams.UserInterface.Subscribe<TopScreenChangedEvent>(TopScreenChangedHandler);
    }

    private void TopScreenChangedHandler(TopScreenChangedEvent eventData)
    {
        _topScreen = eventData.Id;
    }

    public void Draw()
    {
        var screenManagerData = new GetScreenDebugDataEvent();
        screenManagerData.Publish(EventStreams.UserInterface);

        var getScreenManagerState = new GetScreenManagerStateEvent();
        getScreenManagerState.Publish(EventStreams.UserInterface);

        GUILayout.Label($"[Top Screen] {_topScreen} | Is In Progress : {getScreenManagerState?.IsInProgress.CurrentValue}");

        var screenQueue = screenManagerData.ScreenQueue;
        if (screenManagerData.ScreenStack != null)
        {
            Indent(1, () => DrawStack(screenManagerData, screenQueue));
        }

        if (screenManagerData.ScreenQueue != null)
        {
            Indent(1, () => DrawQueue(screenManagerData));
        }

        if (screenManagerData.ScreensCache != null)
        {
            Indent(1, () => DrawScreenCache(screenManagerData));
        }

        if (screenManagerData.AppearingScreensRequests != null)
        {
            Indent(1, () => DrawAppearingProcessor(screenManagerData));
        }
    }

    private void DrawAppearingProcessor(GetScreenDebugDataEvent screenManagerData)
    {
        GUILayout.BeginVertical("box");
        GUILayout.Label("Appearing requests:");
        if (screenManagerData.IsAppearingProcessorWorking)
        {
            GUILayout.Label("Appearing Processor is working...");
        }

        int index = 0;
        foreach (var request in screenManagerData.AppearingScreensRequests)
        {
            var foldoutState = _appearingRequestsFoldouts.ContainsKey(index) && _appearingRequestsFoldouts[index];

            _appearingRequestsFoldouts[index] = GUILayout.Toggle(foldoutState,
                $"{index} - [H:{request.ScreensToHide.Count}, S:{request.ScreensToShow.Count}, U:{request.ScreensToUnload.Count}]");

            if (_appearingRequestsFoldouts[index])
            {
                GUILayout.Label("Show:");
                DrawList(request.ScreensToShow);
                GUILayout.Label("Hide:");
                DrawList(request.ScreensToHide);
                GUILayout.Label("Unload:");
                DrawList(request.ScreensToUnload);
            }

            index++;
        }

        GUILayout.EndVertical();
    }

    private static void DrawScreenCache(GetScreenDebugDataEvent screenManagerData)
    {
        GUILayout.BeginVertical("box");
        GUILayout.Label("Screen Cache:");
        foreach (var pair in screenManagerData.ScreensCache)
        {
            var screenState = pair.Value == null ? "null" : pair.Value.State.CurrentValue.ToString();

            GUILayout.Label($"{pair.Key} - [{screenState}]");
        }

        GUILayout.EndVertical();
    }

    private void DrawQueue(GetScreenDebugDataEvent screenManagerData)
    {
        GUILayout.BeginVertical("box");
        GUILayout.Label("Screen Queue:");
        foreach (var pair in screenManagerData.ScreenQueue)
        {
            DrawQueueFoldout(pair.Key, pair.Value, null);
            DrawQueueElement(pair.Key, pair.Value);
        }

        GUILayout.EndVertical();
    }

    private void DrawStack(GetScreenDebugDataEvent screenManagerData, Dictionary<ScreenId, List<QueueScreenData>> screenQueue)
    {
        GUILayout.BeginVertical("box");
        GUILayout.Label("Screen Stack:");
        int index = 0;
        foreach (var screenData in screenManagerData.ScreenStack)
        {
            var type = screenData.Id;
            var state = screenManagerData.ScreensCache.ContainsKey(type) ? screenManagerData.ScreensCache[type].State.CurrentValue.ToString() : "Unknown";

            GUILayout.BeginHorizontal();

            var label = $"[{index}] {type} [HidePrev:{screenData.ShouldHidePrevious}, State:{state}] ({screenData.ScreenGuid})";
            GUILayout.Label(label);

            if (GUILayout.Button("Close", GUILayout.Width(100)))
            {
                CloseScreensByTypeEvent.Create(type).Publish(EventStreams.UserInterface);
            }

            GUILayout.EndHorizontal();

            index++;
        }

        GUILayout.EndVertical();
    }

    private void DrawQueueElement(ScreenId id, List<QueueScreenData> screenQueue)
    {
        if (_queueFoldouts[id] && screenQueue != null)
        {
            Indent(2, () => DrawQueueElementScreens(screenQueue));
        }
    }

    private static void DrawQueueElementScreens(List<QueueScreenData> screenQueue)
    {
        GUILayout.BeginVertical();

        var isBlocked = false;
        foreach (var queueElement in screenQueue)
        {
            var isReadyToShow = queueElement.IsReadyToShow();
            var isExpired = queueElement.IsExpired();

            var defaultColor = GUI.color;
            if (isBlocked)
            {
                GUI.color = Color.red;
            }

            GUILayout.Label($"{queueElement.ScreenData.Id}|P:{queueElement.Priority}|B:{queueElement.IsBlockingQueue} [Ready:{isReadyToShow}, Expired:{isExpired}] ({queueElement.ScreenData.ScreenGuid})");

            GUI.color = defaultColor;

            isBlocked = isBlocked || queueElement.IsBlockingQueue;
        }

        GUILayout.EndVertical();
    }

    private void DrawQueueFoldout(ScreenId id, List<QueueScreenData> screenQueue, string customTitle)
    {
        var foldout = !_queueFoldouts.ContainsKey(id) || _queueFoldouts[id];
        _queueFoldouts[id] = GUILayout.Toggle(foldout, customTitle ?? $"[{id}] (Count:{screenQueue.Count})");
    }

    private void DrawList(IEnumerable<ScreenData> screens)
    {
        foreach (var screenData in screens)
        {
            GUILayout.Label($"{screenData.Id} [HidePrev:{screenData.ShouldHidePrevious}] ({screenData.ScreenGuid})");
        }
    }

    private void Indent(int level, Action render)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(level * _indentSpace);
        render();
        GUILayout.EndHorizontal();
    }

    public void Dispose() => _subscription?.Dispose();
}
