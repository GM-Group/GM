using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string LoginName { get; set; }

        public string Password { get; set; }
        public string LastPassword { get; set; }
        public string SecurityEmail { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public bool LoginStatus { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class UserInfo
    {
        public string LoginID { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public int Gender { get; set; }
        public string IdentityCard { get; set; }
        public string Area { get; set; }
        public int CreditScore { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime CreateDate { get; set;}
        public DateTime UpdateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }

    }
}
