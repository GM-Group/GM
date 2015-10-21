using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using GM.Utility;

namespace GM.DAL
{
    public class LoginDao
    {

        #region Instance
        private LoginDao() { }

        private static LoginDao instance = null;

        private static object lockHelper = new object();

        public static LoginDao Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new LoginDao();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        public bool Register(string loginName, string password)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into [sq_GMGroup].[sq_GMGroup].[User] ([ID],[LoginName],[Password],[LastPassword],[SecurityEmail],[SecurityQuestion],[SecurityAnswer],[LoginStatus],[LastLoginTime],[CreateDate]) ");
                //sb.Append("values('@ID','@LoginName','@Password','@LastPassword','@SecurityEmail','@SecurityQuestion','@SecurityAnswer',@LoginStatus,@LastLoginTime,@CreateDate)");
                sb.Append("values('xxxx','aa','bb','cc','cc','cc','cc',0,getdate(),getdate())");

                //SqlParameter[] param = new SqlParameter[]
                //                    {                                            
                //                        new SqlParameter("@ID",SqlDbType.VarChar,32,CommonHelper.GenerateUserID()),
                //                        new SqlParameter("@LoginName",SqlDbType.VarChar,32,loginName),
                //                        new SqlParameter("@Password",SqlDbType.VarChar,50,password),
                //                        new SqlParameter("@LastPassword",SqlDbType.VarChar,50,""),
                //                        new SqlParameter("@SecurityEmail",SqlDbType.VarChar,50,""),
                //                        new SqlParameter("@SecurityQuestion",SqlDbType.VarChar,500,""),
                //                        new SqlParameter("@SecurityAnswer",SqlDbType.VarChar,500,""),
                //                        new SqlParameter("@LoginStatus",SqlDbType.Bit,0),
                //                        new SqlParameter("@LastLoginTime",DateTime.Now),
                //                        new SqlParameter("@CreateDate",DateTime.Now),
                //                    };

                return SqlHelper.ExecuteNonQuery(CommonDao.sqlConnectionString, CommandType.Text, sb.ToString()) > 0 ? true : false;
                //return SqlHelper.ExecuteNonQuery(CommonDao.sqlConnectionString, CommandType.Text, sb.ToString(), param) > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
