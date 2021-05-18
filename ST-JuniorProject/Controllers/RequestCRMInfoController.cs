using Microsoft.AspNetCore.Mvc;
using ST_JuniorProject.Models;
using ST_JuniorProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
        public string Post(CRMUserRequest userRequest)
        {
            CRMUserInfo client = userRepository.GetAll().Where(c => c.CuratorId == userRequest.CuratorId).FirstOrDefault();
            var jsonString = JsonConvert.SerializeObject(client);
            return jsonString;
        }
    }
}