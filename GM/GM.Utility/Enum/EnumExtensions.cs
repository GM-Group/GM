using System;

namespace GM.Utility
{
    public static class EnumExtensions
    {
        public static string Key(this Enum enumObj)
        {
            int enumValue = Convert.ToInt32(enumObj);
            EnumItem enumItem = EnumHelper.GetEnumItemByValue(enumObj.GetType(), enumValue);
            return enumItem != null ? enumItem.Key : "0";
        }

        public static string Description(this Enum enumObj)
        {
            int enumValue = Convert.ToInt32(enumObj);
            EnumItem enumItem = EnumHelper.GetEnumItemByValue(enumObj.GetType(), enumValue);
            return enumItem != null ? enumItem.Description : string.Empty;
        }
    }
}