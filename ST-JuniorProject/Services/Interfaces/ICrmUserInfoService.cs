using ST_JuniorProject.Models;

namespace ST_JuniorProject.Services.Interfaces
{
    public interface ICrmUserInfoService
    {
        public CRMUserInfo GetCRMUserInfo(CRMUserRequest userRequest);
    }
}