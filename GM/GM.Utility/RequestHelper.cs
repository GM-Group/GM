using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Web.Mvc;

namespace GM.Utility
{
    /// <summary>
    /// ҳ��Requestͨ�ú���
    /// </summary>
    /// <history>
    ///  <date>2012-07-04</date>
    ///  <programmer>���Ƚ�</programmer>
    /// <document></document>
    /// </history>
    public class RequestHelper
    {
        #region POST
        public static void PostData(string url, Dictionary<string, string> dicData)
        {
            HttpContext context = HttpContext.Current;

            StringBuilder builder = new StringBuilder(256);

            builder.AppendFormat("<html><body><form id='submitdata' method='post' action='{0}'>", url);
            foreach (var item in dicData)
            {
                builder.AppendFormat("<input type='hidden' name='{0}' value={1} id='{0}'><br/>", item.Key, InputVerifierHelper.VerifyContent(item.Value));
            }

            builder.Append("</form><script language='javascript'> document.getElementById('submitdata').submit();</script></body></html>");
            context.Response.Write(builder.ToString());
            context.Response.End();
        }

        /// <summary>
        /// Get & Post���µ�ҳ�� 
        /// </summary>
        public static void PostData(string url)
        {
            HttpContext context = HttpContext.Current;

            StringBuilder builder = new StringBuilder(256);
            builder.Append(string.Format("<html><body><form id=\"submitdata\" name=\"submitdata\" action=\"{0}\" method=\"POST\">", url));
            for (int i = 0; i < context.Request.Form.Count; i++)
            {
                // xss©���޸�
                builder.AppendFormat("<input type='hidden' name='{0}' value={1} id='{0}'><br/>", context.Request.Form.GetKey(i), InputVerifierHelper.VerifyContent(context.Request.Form[i]));
            }
            builder.Append(@"<script language=""javascript"">  document.getElementById('submitdata').submit(); </script></body></form>");
            context.Response.Write(builder.ToString());
            context.Response.End();
        }
        #endregion

        #region ��ȡ�û�Form�ύ���ֶ�ֵ
        public static string GetString(string inputName, MethodType method = MethodType.All)
        {
            return GetPostOrRequestValue(inputName, method);
        }

        public static int GetInt(string inputName, MethodType method = MethodType.All)
        {
            string tempValue = GetPostOrRequestValue(inputName, method);
            int val;
            int.TryParse(tempValue, out val);
            return val;
        }

        /// <summary>
        /// ��ȡpost��get�ύֵ
        /// </summary>
        /// <typeparam name="T">Ҫת��������</typeparam>
        /// <param name="inputName">�ֶ���</param>
        /// <param name="method">post��get</param>
        /// <returns></returns>
        public static T Get<T>(string inputName, MethodType method = MethodType.All)
        {
            string tempValue = GetPostOrRequestValue(inputName, method);
            return ConvertHelper.ToType<T>(tempValue);
        }

        /// <summary>
        /// ��ȡpost��get�ύֵ����ת��
        /// </summary>
        /// <typeparam name="T">Ҫת��������</typeparam>
        /// <param name="inputName">�ֶ���</param>
        /// <param name="Encodeing">����</param>
        /// <param name="method">post��get</param>
        /// <returns></returns>
        public static T GetEncodeing<T>(string inputName,string Encodeing, MethodType method = MethodType.All)
        {
            string tempValue = GetPostOrRequestValueEncoding(inputName,Encodeing, method);
            return ConvertHelper.ToType<T>(tempValue);
        }

        /// <summary>
        /// ��ȡMVC Route Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputName"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static T Get<T>(string inputName, Controller controller)
        {
            string tempValue = GetRouteValues(controller, inputName);
            return ConvertHelper.ToType<T>(tempValue);
        }

        public static string GetString(string inputName, Controller controller)
        {
            return GetRouteValues(controller, inputName);
        }

        public static int GetInt(string inputName, Controller controller)
        {
            string tempValue = GetRouteValues(controller, inputName);
            int val;
            int.TryParse(tempValue, out val);
            return val;
        }

        /// <summary>
        /// ��ȡpost��get��ֵ
        /// </summary>
        /// <param name="inputName">����</param>
        /// <param name="method">�ύ����</param>
        /// <returns></returns>
        private static string GetPostOrRequestValue(string inputName, MethodType method)
        {
            HttpContext context = HttpContext.Current;
            string tempValue = string.Empty;
            #region ��ȡ�ύ�ֶ����� TempValue
            switch (method)
            {
                case MethodType.All:
                    tempValue = ProcessAll(context, inputName);
                    break;
                case MethodType.Post:
                    tempValue = ProcessPost(context, inputName);
                    break;
                case MethodType.Get:
                    tempValue = ProcessGet(context, inputName);
                    break;
                default: tempValue = ProcessAll(context, inputName); break;
            }
            #endregion
            return InputVerifierHelper.VerifyContent(tempValue);
        }

        private static string GetPostOrRequestValueEncoding(string inputName,string Encoding, MethodType method)
        {
            HttpContext context = HttpContext.Current;
            string tempValue = string.Empty;
            #region ��ȡ�ύ�ֶ����� TempValue
            switch (method)
            {
                case MethodType.Post:
                    tempValue = ProcessPostEncoding(context, inputName, Encoding);
                    break;
                case MethodType.Get:
                    tempValue = ProcessGetEncoding(context, inputName, Encoding);
                    break;
                default: tempValue = ProcessGetEncoding(context, inputName, Encoding); break;
            }
            #endregion
            return InputVerifierHelper.VerifyContent(tempValue);
        }

        private static string ProcessAll(HttpContext context, string inputName)
        {
            string tempValue = string.Empty;

            if (context.Request.QueryString[inputName] != null)
            {
                tempValue = ProcessGet(context, inputName);
            }

            if (String.IsNullOrEmpty(tempValue))
            {
                if (context.Request.Form[inputName] != null)
                {
                    tempValue = ProcessPost(context, inputName);
                }
            }
            return tempValue;
        }

        private static string ProcessGet(HttpContext context, string inputName)
        {
            string tempValue = string.Empty;

            if (context.Request.QueryString[inputName] != null)
            {
                tempValue = context.Request.QueryString[inputName].ToString();
            }
            return tempValue;
        }

        private static string ProcessGetEncoding(HttpContext context, string inputName,string encoding)
        {
            string tempValue = string.Empty;

            if (context.Request.QueryString[inputName] != null)
            {
                tempValue = HttpUtility.ParseQueryString(context.Request.Url.Query, Encoding.GetEncoding(encoding))[inputName].ToString();
            }
            return tempValue;
        }

        private static string ProcessPostEncoding(HttpContext context, string inputName, string encoding)
        {
            string tempValue = string.Empty;

            if (context.Request.Form[inputName] != null)
            {
                tempValue =Encoding.GetEncoding(encoding).GetString(
                    Encoding.Convert(Encoding.GetEncoding("GB2312"), Encoding.GetEncoding(encoding),Encoding.GetEncoding("GB2312").GetBytes(context.Request.Form[inputName])));
            }
            return tempValue;
        }

        private static string ProcessPost(HttpContext context, string inputName)
        {
            string tempValue = string.Empty;

            if (context.Request.Form[inputName] != null)
            {
                tempValue = context.Request.Form[inputName].ToString();
            }
            return tempValue;
        }

        private static string GetRouteValues(Controller controller, string key)
        {
            Guard.ArgumentNotNull(controller, "controller");

            string tempValue = string.Empty;
            var context = controller.Request.RequestContext;

            if (context.RouteData.Values[key] != null)
                tempValue = context.RouteData.Values[key].ToString();

            tempValue = InputVerifierHelper.VerifyContent(tempValue);
            return tempValue;
        }
        #endregion
    }

    /// <summary>
    /// ��ȡ�������ݷ�ʽ
    /// </summary>
    public enum MethodType
    {
        All = 0,
        /// <summary>
        /// Post��ʽ
        /// </summary>
        Post = 1,
        /// <summary>
        /// Get��ʽ
        /// </summary>
        Get = 2
    }
}
