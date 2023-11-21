using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hims_Security_API.Model;

namespace Hims_Security_API.Repository
{
   public interface IUser
    {
        public Task<JwtAuthResponse> Authenticate(AuthenticationRequest request);
        public List<PathlistVo> GetPathList(string Role);
        public Boolean AddFacility(AddFacilityVo facility);
        public string AddUser(AddUserVo user);
        public int AddOrganizations(List<AddOrg_facilityVo> obj);
        public Task<int> UpdateOrganization(UpdateOrganizationVo obj);
        public Task<int> UpdateFacility(UpdateFacilityVo obj);
        public Task<int> UpdateUser(GetUserDetailsForUpdateVo User, string Username);
        public Task<int> UpdateUserFacilities(string Facilities, int UserId, int DefaultFid, string UpdatedBy);

        public Task<int> UpdateFacilitiesForUser(List<UpdateFacilitiesForUserVo> obj);


    }
}
