using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace GM.Utility
{
    /// <summary>
    /// 输入验证类
    /// </summary>
    public class InputVerifierHelper
    {
        /// <summary>
        /// 安全过滤
        /// 1.防止跨站脚本攻击安全过滤
        /// 2.对URL输入访问，过滤掉目录遍历功能
        /// </summary>
        /// <param name="content">要过滤的内容</param>
        /// <returns>返回过滤后字符串</returns>
        public static string SecurityFilter(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            string result;
            try
            {
                content = SecurityScript(content);
                content = SecurityUrl(content);
                result = content;
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 转义字符
        /// 防止跨站脚本攻击安全，将字符进行HtmlEncode编码
        /// </summary>
        /// <param name="content">要过滤的内容</param>
        /// <returns>转义后的字符脚本</returns>
        public static string SecurityScript(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            content = content.Replace("'", "''");
            content = content.Replace("%", "[%]");
            content = HttpContext.Current.Server.HtmlEncode(content);
            return content;
        }

        /// <summary>
        /// 安全过滤
        /// SQL注入错误安全检查
        /// </summary>
        /// <param name="content">要检查的内容</param>
        /// <returns>检查后的SQL</returns>
        private static string SecuritySqlFilter(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            content = content.Replace("'", "''");
            content = content.Replace("%", "[%]");
            return content;
        }

        /// <summary>
        /// 安全过滤
        /// SQL注入错误安全过滤
        /// </summary>
        /// <param name="content">要过滤的内容</param>
        /// <returns>过滤后的SQL</returns>
        public static string SecuritySql(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            string[] aryReg = new string[]
			{
				"'",
				"\"",
				"\\",
				"/",
				":",
				"*",
				"%"
			};
            for (int i = 0; i < aryReg.Length; i++)
            {
                content = content.Replace(aryReg[i], string.Empty);
            }
            string pattern = "and|or|exec|insert|select|delete|update|count|chr|mid|master|truncate|char|declare|drop|xp_cmdshell|net user|net localgroup administrators";
            content = Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>
        /// 安全过滤
        /// 对URL输入访问，过滤掉目录遍历功能
        /// </summary>
        /// <param name="content">要过滤的内容</param>
        /// <returns>过滤后的URL</returns>
        private static string SecurityUrl(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            content = content.Replace("../", "");
            return content;
        }

        /// <summary>
        /// 存在危险的字符串，包括("&lt;", "&gt;", "--", "'")
        /// </summary>
        private static readonly string[] FilterString = new string[] { "<", ">", "--", "'" };

        /// <summary>
        /// 校验，当content包含存在危险的字符时，抛出异常
        /// </summary>
        /// <param name="content">待检查内容</param>
        /// <exception cref="ArgumentException">当包含存在危险的字符时，抛出参数异常</exception>
        public static string VerifyContent(string content)
        {
            foreach (string filter in FilterString)
            {
                if (content.Contains(filter))
                    content.Replace(filter, string.Empty);
                    //throw new ArgumentException("Invalid argument : " + filter);
            }
            return content;
        }

        /// <summary>
        /// 校验，当content包含存在危险的字符时，抛出异常，
        /// 当except不为空时，except不作为危险字符
        /// </summary>
        /// <param name="content">待检查内容</param>
        /// <param name="exceptList">需去除的字符</param>
        /// <exception cref="ArgumentException">当包含存在危险的字符时，抛出参数异常</exception>
        public static string VerifyContent(string content, List<string> exceptList)
        {
            if (exceptList == null || exceptList.Count <= 0)
                VerifyContent(content);

            foreach (string filter in FilterString)
            {
                if (exceptList.Contains(filter)) continue;

                if (content.Contains(filter))
                    content.Replace(filter, string.Empty);

                    //throw new ArgumentException("Invalid argument : " + filter);
            }
            return content;
        }

        /// <summary>
        /// 过滤非法字符
        /// </summary>
        public static string ReplaceInvalidChars(string value)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
                result = Regex.Replace(value, "[\\s\\p{P}\n\r=<>$>￥^]", "");

            if (!string.IsNullOrWhiteSpace(result))
                result = result.Replace("+", "")
                .Replace(" ", "").Replace("-", "")
                .Replace("(", "").Replace(")", "")
                .Replace("[", "").Replace("]", "")
                .Replace("{", "").Replace("}", "")
                .Replace(":", "").Replace("/", "")
                .Replace(@"\", "").Trim();

            return result;
        }
    }
}
