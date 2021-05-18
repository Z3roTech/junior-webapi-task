using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ST_JuniorProject.Models;
using ST_JuniorProject.Services.Implementations;
using ST_JuniorProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ST_JuniorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private HttpClient HttpClient = new HttpClient();
        private IUserInfoUpdateService InfoUpdateService;

        public UserInfoController(IUserInfoUpdateService infoUpdateService)
        {
            InfoUpdateService = infoUpdateService;
        }

        [HttpPost]
        public async Task<JsonResult> Post(CRMUserRequest crm)
        {
            CRMUserInfo userInfo = new CRMUserInfo();
            using (var content = new StringContent(JsonConvert.SerializeObject(crm), Encoding.UTF8, "application/json"))
            {
                var responseMessage = await HttpClient.PostAsync("http://localhost:62708/api/RequestCRMInfo", content);
                if (responseMessage != null)
                {
                    var jsonString = await responseMessage.Content.ReadAsStringAsync();
                    userInfo = JsonConvert.DeserializeObject<CRMUserInfo>(jsonString);
                }
            }
            InfoUpdateService.UpdateUserContact(userInfo, crm.PhoneNumber);
            return new JsonResult("Job done successful!");
        }
    }
}