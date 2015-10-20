using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Utility
{
    public class SqlHelper
    {
        private static object lockObj = new object();
        private static const int MaxCaskNumber = 2000;

        public static string GenerateUserID(string ip = "127.0.0.1")
        {
            long tempId;
            DateTime tempDate = DateTime.MinValue;
            const string userIdFormat = "{4}{2,6:yyMMdd}{3,5:00000}{1,3:000}-{5}";
            var appId = -1;


            if (!string.IsNullOrEmpty(ip))
            {
                var segments = ip.Split('.');
                if (segments.Length == 4)
                {
                    if (!int.TryParse(segments[3], out appId))
                    {
                        appId = -1;
                    }
                }
            }

            if (appId < 0)
            {
                appId = AppDomain.CurrentDomain.Id;
            }
            if (appId < 0)
            {
                appId = 0;
            }
            var now = DateTime.Now;
            var ticks = (long)DateTime.Now.TimeOfDay.TotalSeconds;
            lock (lockObj)
            {
                if (now.Date != tempDate.Date)
                {
                    tempId = ticks;
                    tempDate = now;
                }
                if (ticks <= tempId)
                {
                    tempId++;
                }
                else
                {
                    tempId = ticks;
                    tempDate = now;
                }
                ticks = tempId;
            }
            var ts = DateTime.Now - new DateTime(2006, 7, 4);
            var cask = Convert.ToString((int)Math.Abs(ts.Days % MaxCaskNumber), 16).PadLeft(4, '0').ToUpper();
            var id = string.Format(userIdFormat, null, appId, now, ticks, "US", cask);
            return id;
        }

         
    }
}
