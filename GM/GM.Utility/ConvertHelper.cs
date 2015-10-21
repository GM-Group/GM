using GM.Utility.TypeConversion;
using System;
using System.Globalization;

namespace GM.Utility
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class ConvertHelper
    {
        /// <summary>
        /// 将字符串表示转换成等效的对象。
        /// </summary>
        /// <typeparam name="T">要把 <paramref name="value"/> 表示的 <see cref="System.String"/> 转换成的类型。</typeparam>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <returns>
        /// <paramref name="conversionType"/> 类型的对象，其值由 value 表示。
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为空引用。</exception>
        /// <exception cref="FormatException"><paramref name="value"/> 的格式不符合 <paramref name="T"/>的 style。</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> 超出允许范围。</exception>
        /// <remarks>
        /// 对于 <paramref name="T"/> 为 <see cref="System.DateTime"/> 的转换需做日期校验，日期值不能比 <see cref="DateTimeHelper.MinValue"/> 还要小
        /// <seealso cref="CheckTypeValue"/>
        /// </remarks>
        public static T ToType<T>(object value)
        {
            return (T)ToType(value, typeof(T));
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将字符串表示转换成等效的对象。
        /// </summary>
        /// <typeparam name="T">要把 <paramref name="value"/> 表示的 <see cref="System.String"/> 转换成的类型。</typeparam>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <param name="defaultValue">转换不成功后替代的默认值</param>
        /// <returns>
        /// <paramref name="conversionType"/> 类型的对象，其值由 value 表示。
        /// 如果转换不成功，则返回 <pararef name="defaultValue">。
        /// </returns>
        public static T ToType<T>(object value, T defaultValue)
        {
            return (T)ToType(value, typeof(T), defaultValue);
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将字符串表示转换成等效的对象。
        /// </summary>
        /// <param name="value">包含要转换的</param>
        /// <param name="conversionType">要转换成的 <see cref="System.Type"/>。</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns><paramref name="conversionType"/> 类型的对象，其值由 value 表示。</returns>
        public static object ToType(object value, Type targetType, object defaultValue)
        {
            object targetValue = defaultValue;
            try
            {
                targetValue = ToType(value, targetType);
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }

        /// <summary>
        /// 将字符串表示转换成等效的对象。
        /// </summary>
        /// <param name="source">包含要转换的 <see cref="String"/>。</param>
        /// <param name="conversionType">要转换成的 <see cref="System.Type"/>。</param>
        /// <returns><paramref name="conversionType"/> 类型的对象，其值由 value 表示。</returns>
        public static object ToType(object source, Type targetType)
        {
            ITypeConverter typeConverter = TypeConverterRegistry.GetConverter(targetType);
            if (typeConverter == null)
            {
                return Convert.ChangeType(source, targetType, CultureInfo.CurrentCulture);
            }

            return typeConverter.ConvertFrom(source);
        }
    }
}
