using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ST_JuniorProject.Models;
using ST_JuniorProject.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;

namespace ST_JuniorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private IUserInfoUpdateService InfoUpdateService;

        public UserInfoController(IUserInfoUpdateService infoUpdateService)
        {
            InfoUpdateService = infoUpdateService;
        }

        [HttpPost]
        public async Task<JsonResult> Post(CRMUserRequest crm)
        {
            CRMUserInfo userInfo = new CRMUserInfo();
            string url = "http://localhost:62708/api/RequestCRMInfo";
            var content = new StringContent(JsonConvert.SerializeObject(crm), Encoding.UTF8, "application/json");
            userInfo = await url.PostAsync(content).ReceiveJson<CRMUserInfo>();

            if (userInfo.Login == -1) return new JsonResult("Error");

            InfoUpdateService.UpdateUserContact(userInfo, crm.PhoneNumber);
            return new JsonResult("Job done successfully!");
        }
    }
}