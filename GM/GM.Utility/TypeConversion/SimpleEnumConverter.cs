using System;
using System.Globalization;

namespace GM.Utility.TypeConversion
{
    public class SimpleEnumConverter : BaseTypeConverter
    {
        public SimpleEnumConverter(Type enumType)
        {
            this.EnumType = enumType;
        }

        public Type EnumType { get; private set; }
     
        public override bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(int);
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            object enumObj = null;

            // 如果是source是字符串类型，则特定是Key
            if (source is string)
            {
                string str = (string)source;
                if (str.IndexOf(',') != -1)
                {
                    long num = 0L;
                    foreach (string str2 in str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        num |= Convert.ToInt64((Enum)Enum.Parse(this.EnumType, str2, true), culture);
                    }
                    enumObj = Enum.ToObject(this.EnumType, num);
                }
                else
                {
                    enumObj = EnumHelper.GetEnumByKey(this.EnumType, source as string);
                }
            }
            else if (source.GetType() == typeof(int) 
                || source.GetType() == typeof(byte) 
                || source.GetType() == typeof(Int64))
            {                
                enumObj = EnumHelper.GetEnumByValue(this.EnumType, Convert.ToInt32(source));
                if (enumObj == null)
                {
                    enumObj = Enum.ToObject(this.EnumType, source);
                }
            }
            
            return enumObj;
        }

        public override bool CanConvertTo(Type destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(int);
        }

        public override object ConvertTo(object value, Type destinationType, CultureInfo culture)
        {
            object enumObj = null;

            // 如果是source是字符串类型，则特定是Key
            if (value is string)
            {
                enumObj = EnumHelper.GetEnumByKey(destinationType, value as string);
            }
            else if (value.GetType() == typeof(int)
                || value.GetType() == typeof(byte)
                || value.GetType() == typeof(Int64))
            {  
                enumObj = EnumHelper.GetEnumByValue(destinationType, Convert.ToInt32(value));
            }

            return enumObj;
        }
    }
}