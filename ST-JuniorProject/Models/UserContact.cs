using ST_JuniorProject.Models.Base;
using System;

namespace ST_JuniorProject.Models
{
    public class UserContact : UserModel
    {
        public UserData Client { set; get; }
        public int ClientId { set; get; }
        public String PhoneNumber { set; get; }
    }
}