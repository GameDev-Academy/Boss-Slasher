using System;
using ScreenManager.Conditions;
using UnityEngine;

namespace ScreenManager
{
    public class QueueScreenData : IDisposable
    {
        public ScreenData ScreenData;
        public IScreenCondition ExpireConditions;
        public IScreenCondition ShowConditions;
        // Screen with the highest priority shows first
        public int Priority;
        public bool IsBlockingQueue;

        public QueueScreenData(ScreenData screenScreenData,
            bool isBlockingQueue = false,
            IScreenCondition expireConditions = null,
            IScreenCondition showConditions = null)
        {
            ScreenData = screenScreenData;
            ExpireConditions = expireConditions;
            ShowConditions = showConditions;
            IsBlockingQueue = isBlockingQueue;
        }

        public bool IsExpired()
        {
            if (ExpireConditions == null)
            {
                return false;
            }

            try
            {
                return ExpireConditions.IsSatisfied();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                return true;
            }
        }

        public bool IsReadyToShow()
        {
            if (ShowConditions == null)
            {
                return true;
            }

            try
            {
                return ShowConditions.IsSatisfied();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                return false;
            }
        }

        public void Dispose()
        {
            ExpireConditions?.Dispose();
            ShowConditions?.Dispose();
        }
    }
}
