using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hims_Security_API.Model;
using Hims_Security_API.Repository;
using Microsoft.AspNetCore.Authorization;
using Hims_Security_API.SecurityDB;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Hims_Security_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       [HttpPost]
       [Route("[Action]")]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            try
            {
                User Auth = new User();
                var result=await  Auth.Authenticate(request);
                if (result == null)
                {
                    return Unauthorized();
                }
                else
                { 
                    return Ok(result);
                }
            

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        private string GetUserName()
        {

            string userName = "";
            var userNameClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Username");
            if (userNameClaim != null)
            {
                userName = userNameClaim.Value;
                //  return Ok($"Logged-in User Name: {userName}");
            }
            return userName;
            //return Ok($"Logged-in User Name: {userName}");
        }
        [EnableCors("AllowAllHeaders")]
       [HttpPost]
       [Route("[Action]")]
        [Authorize]
        public IActionResult Register(AddUserVo register)
        {
        
                User Auth = new User();
                var result = Auth.AddUser(register);
                if(result!=null)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(JsonConvert.SerializeObject("Re try"));
                }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("[Action]")]
        public List<PathlistVo> GetPathList(string   Role)
        {

            User Auth = new User();
            var result = Auth.GetPathList(Role.ToString());
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        [Authorize]
        [Route("[Action]")]
        public int AddOrganizations(List<AddOrg_facilityVo> obj)
        {
            User Auth = new User();
            var result = Auth.AddOrganizations(obj);
            if (result != null)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        [Authorize]
        [Route("[Action]")]
        public async Task<int> UpdateOrganization(UpdateOrganizationVo obj)
        {
            try
            {
                User Auth = new User();
                var result = await Auth.UpdateOrganization(obj);
                return result;
              


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Authorize]
        [Route("[Action]")]

        public async Task<int> UpdateFacility(UpdateFacilityVo obj)
        {
            try
            {
                User Auth = new User();
                var result = await Auth.UpdateFacility(obj);
                return result;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Authorize]
        [Route("[Action]")]
        
        public async Task<int> UpdateUser(GetUserDetailsForUpdateVo User)
        {
            try
            {
                string username = GetUserName();
                User Auth = new User();
                var result = await Auth.UpdateUser(User, username);
                return result;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Authorize]
        [Route("[Action]")]
        public async Task<int> UpdateUserFacilities(string Facilities, int UserId, int DefaultFid)
        {
            try
            {
                string UpdatedBy = GetUserName();
                User Auth = new User();
                var result = await Auth.UpdateUserFacilities(Facilities, UserId, DefaultFid, UpdatedBy) ;
                return result;



            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        [HttpPost]
        [Authorize]
        [Route("[Action]")]
        public async Task<int> UpdateFacilitiesForUser(List<UpdateFacilitiesForUserVo> obj)
        {
            try
            {
                string UpdatedBy = GetUserName();
                User Auth = new User();
                var result = await Auth.UpdateFacilitiesForUser(obj);
                return result;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}

