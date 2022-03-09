using System;
using System.Diagnostics;
using ScreenManager.Enums;

namespace ScreenManager
{
    [DebuggerDisplay("{Id}")]
    public struct ScreenData
    {
        public ScreenData(ScreenId screenId, object context, bool shouldHidePrevious, bool showLoadingIndicator = true)
        {
            Id = screenId;
            Context = context;
            ShouldHidePrevious = shouldHidePrevious;
            ScreenGuid = Guid.NewGuid();
            StackPosition = 0;
            ShowLoadingIndicator = showLoadingIndicator;
        }

        public ScreenId Id;
        public object Context;
        public bool ShouldHidePrevious;
        public Guid ScreenGuid;
        public int StackPosition;
        public bool ShowLoadingIndicator;
    }
}
