using ST_JuniorProject.Models;

namespace ST_JuniorProject.Services.Interfaces
{
    public interface IUserInfoService
    {
        public void UpdateUserContact(CRMUserInfo userInfo, string phoneNumber);
    }
}