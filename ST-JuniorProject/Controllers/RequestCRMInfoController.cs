using Microsoft.AspNetCore.Mvc;
using ST_JuniorProject.Models;
using ST_JuniorProject.Services.Interfaces;

namespace ST_JuniorProject.Controllers
{
    /// <summary>
    /// Контроллер эмитации удалённого ресурса, хранящего параметры пользователей CRM-системы
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RequestCRMInfoController : ControllerBase
    {
        private ICrmUserInfoService CrmUserInfoService;

        public RequestCRMInfoController(ICrmUserInfoService crmUserInfoService)
        {
            CrmUserInfoService = crmUserInfoService;
        }

        /// <summary>
        /// Обработчик POST запроса api/RequestCRMInfo для выдачи информации о пользователе CRM-системы
        /// </summary>
        /// <param name="userRequest">JSON-файл с указанным CuratorId</param>
        /// <returns>Параметры пользователя в БД</returns>
        [HttpPost]
        public CRMUserInfo Post(CRMUserRequest userRequest)
        {
            CRMUserInfo client = CrmUserInfoService.GetCRMUserInfo(userRequest);
            if (client != null)
                return client;
            return new CRMUserInfo() { Login = -1 };
        }
    }
}