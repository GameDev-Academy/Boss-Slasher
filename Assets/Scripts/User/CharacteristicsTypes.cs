using System;
using System.Collections.Generic;
using System.Linq;

namespace User
{
    public static class CharacteristicsTypes
    {
        public static IEnumerable<CharacteristicType> GetAll()
        {
            return Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();
        }
    }
}