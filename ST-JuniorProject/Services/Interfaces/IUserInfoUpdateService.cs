using ST_JuniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject.Services.Interfaces
{
    public interface IUserInfoUpdateService
    {
        public void UpdateUserContact(CRMUserInfo userInfo, string phoneNumber);
    }
}