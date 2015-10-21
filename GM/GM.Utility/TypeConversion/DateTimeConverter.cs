using System;
using System.Globalization;

namespace GM.Utility.TypeConversion
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DateTimeConverter : BaseTypeConverter
    {
        public override bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(DateTime) || base.CanConvertFrom(sourceType);
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if (source == null)
            {
                return DateTimeHelper.MinValue;
            }

            Type sourceType = source.GetType();
            if (sourceType == typeof(string))
            {
                string value = source as string;

                DateTime tryvalue;

                //判断是否是正确的时间格式
                if (string.IsNullOrWhiteSpace(value) || !DateTime.TryParse(value,out tryvalue))
                {
                    return DateTimeHelper.MinValue;
                }
            }

            return Convert.ToDateTime(source);
        }
    }
}