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
    public class AppusersController : ControllerBase
    {
        private readonly IAppuser appuserInterface;

        public AppusersController(IAppuser appuserInterface)
        {
            this.appuserInterface = appuserInterface;
        }

        [HttpPost]
        [Route("Appuser")]
        public async Task<IActionResult> AddAppuser(Appuser AppUser)
        {
            try
            {
                var Appuser = await appuserInterface.AddAppuser(AppUser);
                if (Appuser != null) 
                {
                    return Ok(Appuser);
                }
                else
                {
                    return StatusCode(500, $"Failed to Add AppUser");
                }
            }
            catch 
            {
                return StatusCode(500, $"Error occured While Adding AppUser");
            }
         
        }

        [HttpGet]
        [Route("Appuser/Usertype/{userType}")]
        public async Task<IActionResult> GetAllAppuserByUserType([FromRoute] string userType)
        {
            try
            {
                if (userType == null)
                {
                    return StatusCode(500, $"userType cannot be null");
                }
                var Appusers = await appuserInterface.GetAllAppuserByUserType(userType);
                if(Appusers != null)
                {
                    return Ok(Appusers);
                }
                else
                {
                    return NotFound($"Appuser Not Found");
                }
            }
            catch 
            {
                return StatusCode(500, $"Error Occured while Getting Appuser by Usertype");
            }
        }

        [HttpPatch]
        [Route("Appuser/Id/{AppUserId}")]
        public async Task<IActionResult> UpdateAppuser([FromRoute] int AppUserId, [FromBody] Appuser AppUser)
        {
            try
            {
                if (AppUser == null)
                {
                    return StatusCode(500, $"AppUser cannot be null");
                }
                var ExistingAppusers = await appuserInterface.UpdateAppuser(AppUserId, AppUser);
                if(ExistingAppusers != null)
                {
                    return Ok(ExistingAppusers);
                }
                else
                {
                    return NotFound($"Appuser Not Found");
                }
            }
            catch 
            {
                return StatusCode(500, $"Error Occured while Updating AppUser");
            }
        }


        [HttpDelete]
        [Route("Appuser/Id/{AppUserId}/Delete")]
        public async Task<IActionResult> DeleteAppuser(int AppUserId)
        {
            try
            {
                var AppUser = await appuserInterface.DeleteAppuser(AppUserId);
                if(AppUser != null)
                {
                    return Ok(AppUser);
                }
                else
                {
                    return NotFound($"Appuser Not Found");
                }
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Deleting AppUser");
            }
        }

    }
}
