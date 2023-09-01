using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XUnitAssessment.Data;
using XUnitAssessment.Models;
using XUnitAssessment.Services.Interface;

namespace XUnitAssessment.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsertypesController : ControllerBase
    {
        private readonly IUserType UsertypeInterface;

        public UsertypesController(IUserType UsertypeInterface)
        {
            this.UsertypeInterface = UsertypeInterface;
        }

        [HttpPost]
        [Route("usertype")]
        public async Task<IActionResult> AddUserType([FromBody] Usertype usertype)
        {
            try
            {
                var userType = await UsertypeInterface.AddUserType(usertype);
                if (userType != null)
                {
                    return Ok(userType);
                }
                else
                {
                    return StatusCode(500, $"Failed to Add Usertypes");
                }
            }
            catch 
            {
                return StatusCode(500, $"Error Occured while Adding Usertypes");
            }
        }


        [HttpGet]
        [Route("usertype")]
        public async Task<IActionResult> GetAllUserType()
        {
            try
            {
                var AllUsers = await UsertypeInterface.GetAllUserType();
                if(AllUsers != null)
                {
                    return Ok(AllUsers);
                }
                else
                {
                    return NotFound($"Usertype Not Found");
                }
            }
            catch 
            {
                return StatusCode(500, $"Error Occured while getting Usertypes");
            }
        }

        [HttpPatch]
        [Route("Usertype/Id/{UserTypeId}")]
        public async Task<IActionResult> UpdateUserType([FromRoute] int UserTypeId, [FromBody] Usertype userType)
        {
            try
            {
                var Usertypes = await UsertypeInterface.UpdateUserType(UserTypeId, userType);
                if (Usertypes != null)
                {
                    return Ok(Usertypes);
                }
                else
                {
                    return NotFound($"Usertype Not Found");
                }
            }
            catch
            {
                return StatusCode(500, $"Error Occured while updating Usertypes");
            }
            
        }

    }
}
