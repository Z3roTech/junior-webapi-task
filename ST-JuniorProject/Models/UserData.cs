using ST_JuniorProject.Models.Base;
using System;
using System.Collections.Generic;

namespace ST_JuniorProject.Models
{
    public class UserData : UserModel
    {
        public String Name { set; get; }
        public List<UserContact> Contacts { set; get; }
        public int Login { set; get; }
        public int LineNumber { set; get; }
    }
}