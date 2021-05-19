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
    /// <summary>
    /// Контроллер обработки уведомлений о изменение пользовательских данных
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private IUserInfoService InfoUpdateService;

        public UserInfoController(IUserInfoService infoUpdateService)
        {
            InfoUpdateService = infoUpdateService;
        }

        /// <summary>
        /// Тестовая страница для подтверждения работы приложения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult("Server online");
        }

        /// <summary>
        /// Обработчик POST запроса api/UserInfo для обновлении контактных данных пользователя в БД
        /// </summary>
        /// <param name="crm">JSON-файл содержащий информацию о изменении контактных данных пользователя</param>
        /// <returns></returns>
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