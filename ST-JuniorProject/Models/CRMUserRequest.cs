using ST_JuniorProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject.Models
{
    public class CRMUserRequest
    {
        public String Name { set; get; }
        public String PhoneNumber { set; get; }
        public int CuratorId { set; get; }
    }
}