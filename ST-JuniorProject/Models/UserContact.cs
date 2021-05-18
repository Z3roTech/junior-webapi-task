using ST_JuniorProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject.Models
{
    public class UserContact : UserModel
    {
        public UserData Client { set; get; }
        public int ClientId { set; get; }
        public String PhoneNumber { set; get; }
    }
}