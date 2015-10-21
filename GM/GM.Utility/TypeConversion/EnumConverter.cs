using System;
using System.Globalization;

namespace GM.Utility.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnumConverter : BaseTypeConverter
    {
        // Methods
        public EnumConverter(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override bool CanConvertFrom(Type sourceType)
        {
            return ((sourceType == typeof(Enum[])) || (sourceType == typeof(string)));
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if (source != null)
            {
                Array enumValues = Enum.GetValues(this.EnumType);
                Type sourceType = source.GetType();
                if (((sourceType == typeof(short)) || (sourceType == typeof(int))) || (sourceType == typeof(long)))
                {
                    return Enum.ToObject(this.EnumType, source);
                }
                if (source is string)
                {
                    try
                    {
                        string str = (string)source;
                        if (str.IndexOf(',') != -1)
                        {
                            long num = 0L;
                            foreach (string str2 in str.Split(new char[] { ',' }))
                            {
                                num |= Convert.ToInt64((Enum)Enum.Parse(this.EnumType, str2, true), culture);
                            }
                            return Enum.ToObject(this.EnumType, num);
                        }
                        return Enum.Parse(this.EnumType, str, true);
                    }
                    catch (Exception exception)
                    {
                        throw new FormatException(string.Format("ConvertInvalidPrimitive={0} is not a valid value for {1}.", (string)source, this.EnumType.Name), exception);
                    }
                }
                if (source is Enum[])
                {
                    long num2 = 0L;
                    foreach (Enum enum2 in (Enum[])source)
                    {
                        num2 |= Convert.ToInt64(enum2, culture);
                    }
                    return Enum.ToObject(this.EnumType, num2);
                }
            }
            return Enum.ToObject(this.EnumType, 0);
        }

        // Properties
        public Type EnumType { get; private set; }
    }
}