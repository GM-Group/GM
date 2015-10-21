using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Utility
{
    /// <summary>
    /// A static helper class that includes various parameter checking routines.
    /// </summary>
    public static partial class Guard
    {
        /// <summary>
        /// 如果提供的 <paramref name="argumentValue"/> 是 <see langword="null"/>，
        /// 则抛出 <see cref="ArgumentNullException"/>。
        /// </summary>
        /// <param name="argumentValue">参数值。</param>
        /// <param name="argumentName">参数名。</param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果提供的 <paramref name="argumentValue"/> 是 <see langword="null"/>.
        /// </exception>
        public static void ArgumentNotNull(object validationValue, string argumentName)
        {
            ArgumentNotNull(validationValue, argumentName, "参数{0}的值不能为空.");
        }

        /// <summary>
        /// 如果提供的 <paramref name="argumentValue"/> 是 <see langword="null"/>，
        /// 则抛出 <see cref="ArgumentNullException"/>。
        /// </summary>
        /// <param name="argumentValue">参数值。</param>
        /// <param name="argumentName">参数名。</param>
        /// <param name="message">提示信息。</param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果提供的 <paramref name="argumentValue"/> 是 <see langword="null"/>.
        /// </exception>
        public static void ArgumentNotNull(object validationValue, string argumentName, string message)
        {
            if (validationValue == null)
            {
                throw new ArgumentNullException(
                    argumentName,
                    string.Format(message, argumentName)
                );
            }
        }

        public static void ArgumentNotNullOrEmpty(string validationValue, string argumentName)
        {
            ArgumentNotNullOrEmpty(validationValue, argumentName, "参数{0}的值不能为Null或为空.");
        }

        public static void ArgumentNotNullOrEmpty(string validationValue, string argumentName, string message)
        {
            if (string.IsNullOrEmpty(validationValue))
            {
                throw new ArgumentNullException(
                    argumentName,
                    string.Format(message, argumentName)
                );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentName"></param>
        /// <param name="col"></param>
        public static void ArgumentHasLength(ICollection col, string argumentName)
        {
            if (col == null || col.Count <= 0)
            {
                throw new ArgumentException("集合中没有元素.", argumentName);
            }
        }

        #region IsTrue

        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。
        /// </summary>
        /// <param name="condition">要验证的条件为 true。</param>
        /// <param name="argumentName">参数的名称。</param>
        /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
        public static void IsTrue(bool condition, string argumentName)
        {
            IsTrue(condition, argumentName, "判断条件不能为True", null);
        }

        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息。
        /// </summary>
        /// <param name="condition">要验证的条件为 true。</param>
        /// <param name="argumentName">参数的名称。</param>
        /// <param name="message">断言失败时显示的消息。</param>
        /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
        public static void IsTrue(bool condition, string argumentName, string message)
        {
            IsTrue(condition, argumentName, message, null);
        }

        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <param name="condition">要验证的条件为 true。</param>
        /// <param name="argumentName">参数的名称。</param>
        /// <param name="message">断言失败时显示的消息。</param>
        /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
        /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
        public static void IsTrue(bool condition, string argumentName, string message, params object[] messageArgs)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(message, messageArgs), argumentName);
            }
        }

        #endregion

        #region IsFalse

        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。
        /// </summary>
        /// <param name="condition">要验证的条件为 true。</param>
        /// <param name="argumentName">参数的名称。</param>
        /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
        public static void IsFalse(bool condition, string argumentName)
        {
            IsFalse(condition, argumentName, "判断条件不能为False.", null);
        }

        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息。
        /// </summary>
        /// <param name="condition">要验证的条件为 true。</param>
        /// <param name="argumentName">参数的名称。</param>
        /// <param name="message">断言失败时显示的消息。</param>
        /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
        public static void IsFalse(bool condition, string argumentName, string message)
        {
            IsFalse(condition, argumentName, message, null);
        }

        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <param name="condition">要验证的条件为 true。</param>
        /// <param name="argumentName">参数的名称。</param>
        /// <param name="message">断言失败时显示的消息。</param>
        /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
        /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
        public static void IsFalse(bool condition, string argumentName, string message, params object[] messageArgs)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(message, messageArgs), argumentName);
            }
        }

        #endregion

        public static bool IsNullable(Type type)
        {
            ArgumentNotNull(type, "type");
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
