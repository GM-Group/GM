using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM.DAL;

namespace GM.BLL
{
    public class LoginBusiness
    {

        public bool Register(string loginName, string password)
        {
            bool result = false;
            result = LoginDao.Instance.Register(loginName, password);
            return result;
        }
    }
}
