using Microsoft.AspNetCore.Mvc;
using ST_JuniorProject.Models;
using ST_JuniorProject.Repositories.Interfaces;
using System.Linq;

namespace ST_JuniorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestCRMInfoController : ControllerBase
    {
        private readonly IUserRepository<CRMUserInfo> userRepository;

        public RequestCRMInfoController(IUserRepository<CRMUserInfo> userRep)
        {
            userRepository = userRep;
        }

        // POST api/<RequestCRMInfoController>
        [HttpPost]
        public CRMUserInfo Post(CRMUserRequest userRequest)
        {
            CRMUserInfo client = userRepository.GetAll().Where(c => c.CuratorId == userRequest.CuratorId).FirstOrDefault();
            if (client != null)
                return client;
            return new CRMUserInfo() { Login = -1 };
        }
    }
}