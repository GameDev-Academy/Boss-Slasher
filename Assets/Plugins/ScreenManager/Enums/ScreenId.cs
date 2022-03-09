using System;
using ScreenManager.Events;
using ScreenManager.Interfaces;
using SimpleBus.Extensions;

namespace ScreenManager.Enums
{
    [Serializable]
    public struct ScreenId : IComparable, IComparable<ScreenId>
    {
        private static IScreenSettingsProvider _screenSettingsProvider;
        public static readonly ScreenId None = new ScreenId(0);
        public int Id;

        public ScreenId(int id)
        {
            Id = id;
        }
        
        public override string ToString()
        {
            if (_screenSettingsProvider == null)
            {
                var getScreenSettingsProviderEvent = new GetScreenSettingsProviderEvent();
                getScreenSettingsProviderEvent.Publish(EventStreams.UserInterface);
                _screenSettingsProvider = getScreenSettingsProviderEvent.ScreenSettingsProvider;
            }
            
            return _screenSettingsProvider?.Get(this).Name ?? base.ToString();
        }

        public int CompareTo(ScreenId other)
        {
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj is ScreenId screenType)
            {
                return Id == screenType.Id;
            }

            return false;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(ScreenId a, ScreenId b) => a.Id == b.Id;

        public static bool operator !=(ScreenId a, ScreenId b) => !(a == b);

        public static implicit operator int(ScreenId type)
        {
            return type.Id;
        }
        
        public static implicit operator ScreenId(int id)
        {
            return new ScreenId(id);
        }
        
        public int CompareTo(object other) => Id.CompareTo(((ScreenId) other).Id);
    }
}