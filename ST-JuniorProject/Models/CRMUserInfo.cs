using ST_JuniorProject.Models.Base;

namespace ST_JuniorProject.Models
{
    public class CRMUserInfo : UserModel
    {
        public int CuratorId { get; set; }
        public int Login { get; set; }
        public int LineNumber { get; set; }
    }
}