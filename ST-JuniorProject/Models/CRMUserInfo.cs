using ST_JuniorProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject.Models
{
    public class CRMUserInfo : UserModel
    {
        public int CuratorId { get; set; }
        public int Login { get; set; }
        public int LineNumber { get; set; }
    }
}