using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Utility
{
    public static class UserHelper
    {
        private object lockObj = new object();
        private DateTime tempDate = DateTime.MinValue;
        private long tempId;
        private const int MaxCaskNumber = 2000;

        public string GenerateUserID(string ip = "127.0.0.1")
        {
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
            lock (this.lockObj)
            {
                if (now.Date != this.tempDate.Date)
                {
                    this.tempId = ticks;
                    this.tempDate = now;
                }
                if (ticks <= this.tempId)
                {
                    this.tempId++;
                }
                else
                {
                    this.tempId = ticks;
                    this.tempDate = now;
                }
                ticks = this.tempId;
            }
            var ts = DateTime.Now - new DateTime(2006, 7, 4);
            var cask = Convert.ToString((int)Math.Abs(ts.Days % MaxCaskNumber), 16).PadLeft(4, '0').ToUpper();
            var id = string.Format(userIdFormat, null, appId, now, ticks, "US", cask);
            return id;
        }

         
    }
}
