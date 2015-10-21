using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GM.DAL
{
    public class CommonDao
    {
        public static readonly string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;

    }
}
