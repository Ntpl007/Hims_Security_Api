using Hims_Security_API.Model;
using Hims_Security_API.Security;
using Hims_Security_API.SecurityDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hims_Security_API.Repository
{
    public class User : IUser
    {
      
        public async Task<JwtAuthResponse> Authenticate(AuthenticationRequest request)
        {
            try
            {
                JwtAuthResponse response = new JwtAuthResponse();
                Decryption obj = new Decryption();
               // TblUser data = new TblUser();
                string pwd = null;
                using (var context = new bhishak_app_dbContext())
                {

                  var  data =await  context.TblUsers.Where(x => x.UserName == request.username).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        pwd= obj.DecryptAesManaged(data);

                        if (pwd == request.Password)
                        {
                           var facility = context.TblAdmFacilities.Where(x => x.FacilityId == data.FacilityId).FirstOrDefault();
                            var organization = context.TblOrganizations.Where(x => x.OrganizationId == data.OrganizationId).FirstOrDefault();
                           
                           // var roledata = context.TblUserroles.Where(x => x.UserId == data.UserId).FirstOrDefault();
                            var roleid = (from x in context.TblUserroles
                                             where x.UserId == data.UserId
                                             select x.Roleid

                                            ).First(); 

                            var role = context.TblRoles.Where(x => x.RoleId == roleid).FirstOrDefault();
                            //authentication

                            IdentityOptions _options = new IdentityOptions();

                            var tokenExpiryTimestamp = DateTime.Now.AddMinutes(Constants.JWT_TOKEN_VALIDITY_IN_MIN);
                            var JwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                            var tokenkey = Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY);
                            var securitytokendiscriptor = new SecurityTokenDescriptor
                            {
                                 // Issuer= "https://localhost:44335/",
                                Issuer = "https://10.10.20.25:81/",



                                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("Username",data.UserName),
                    new Claim(ClaimTypes.PrimaryGroupSid,"user group 01"),
                    new Claim(_options.ClaimsIdentity.RoleClaimType,role.RoleName),
                    
                }),
                              
                                Expires = tokenExpiryTimestamp,
                               
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                               
                                

                            };
                            var securityToken = JwtSecurityTokenHandler.CreateToken(securitytokendiscriptor);
                            var token = JwtSecurityTokenHandler.WriteToken(securityToken);

                            var respnse= new JwtAuthResponse
                            {
                                token = token,
                                User_Id=data.UserId,
                                User_Name = data.UserName,
                                Expires_In = (int)tokenExpiryTimestamp.Subtract(DateTime.Now).TotalSeconds,
                                Facility_Name = facility.FacilityName,
                                Organization_Name = organization.OrganizationName,
                                Organization_Id=organization.OrganizationId,
                                Facility_Id=facility.FacilityId
                                
                            };
                            return respnse;

                        }
                        else
                        {
                            return null;
                        }
                      

                    }
                    else
                    {
                        return null;
                      
                    }

                }
              
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public string AddUser(AddUserVo user)
        //{
        //    try
        //    {

        //        Encryption enc = new Encryption();
        //        EncryptedDataVo objEnc = new EncryptedDataVo();
        //        objEnc = enc.EncryptAesManaged(user.Password);
              
        //        using (var context = new bhishak_app_dbContext())
        //        {
        //            if(user.Speciality!=0 && user.Speciality!=null)
        //            {
        //                var speciality = (from x in context.TblAdmSpecialities
        //                                  where x.SpecialityId == user.Speciality
        //                                  select x.SpecialityDesc
        //                                  ).First();

        //                   TblUser obj = new TblUser();
        //                    obj.FirstName = user.First_Name;
        //                    obj.LastName = user.Last_Name;
        //                    obj.ListName = "Dr." + user.First_Name+" "+user.Last_Name+" ("+ speciality + ")";
        //                    obj.RoleId = user.UserRole;

        //                if(user.User_Name!="")
        //                {
        //                    obj.UserName = user.User_Name;
        //                }

                           
        //                    obj.MobileNumber = user.Mobile_Number;
        //                    obj.FacilityId = user.facility_id;
        //                    obj.OrganizationId = user.Organization_id;
        //                     obj.Status = true;
        //                if (user.Password != "")
        //                {
        //                    obj.Password = user.Password;
        //                }
                        
        //                    obj.EncryptedPassword = objEnc.EncryptedPassword;
        //                    obj.EncryptedKey = objEnc.Key;
        //                    obj.EncryptedIv = objEnc.IV;
        //                    if (user.isReferDoctor == true)
        //                    {
        //                        obj.IsProvider = false;
        //                    }
        //                    else obj.IsProvider = true;
        //                    if (user.Speciality != 0)
        //                    {
        //                        obj.SpecialityId = user.Speciality;
        //                    }

        //                    obj.CreatedDate = DateTime.Now;
        //                    obj.CreatedBy = user.CreatedBy;
        //                    context.TblUsers.Add(obj);
        //                    context.SaveChanges();
        //                    var userdata = context.TblUsers.Where(x => (x.FirstName == user.First_Name) && (x.MobileNumber == user.Mobile_Number)).FirstOrDefault();
        //                    TblUserrole objuserrole = new TblUserrole();
        //                    objuserrole.UserId = userdata.UserId;
        //                    objuserrole.Roleid = user.UserRole;
        //                    objuserrole.CreatedDate = DateTime.Now;
        //                    objuserrole.CreatedBy = user.CreatedBy;
        //                    context.TblUserroles.Add(objuserrole);
        //                    context.SaveChanges();

        //                    return JsonConvert.ToString(obj.UserName + " Saved Successfuly");
                        
        //            }else
        //            {
        //                var checkIsExist = context.TblUsers.Where(x => (x.UserName == user.User_Name)).FirstOrDefault();
        //                if (checkIsExist == null)
        //                {
        //                    TblUser obj = new TblUser();
        //                    obj.FirstName = user.First_Name;
        //                    obj.LastName = user.Last_Name;
        //                    obj.UserName = user.User_Name;
        //                    obj.MobileNumber = user.Mobile_Number;
        //                    obj.FacilityId = user.facility_id;
        //                    obj.OrganizationId = user.Organization_id;
        //                    obj.Status = true;
        //                    obj.Password = user.Password;
        //                    obj.EncryptedPassword = objEnc.EncryptedPassword;
        //                    obj.EncryptedKey = objEnc.Key;
        //                    obj.EncryptedIv = objEnc.IV;
                           
        //                    obj.CreatedDate = DateTime.Now;
        //                    obj.CreatedBy = user.CreatedBy;
        //                    context.TblUsers.Add(obj);
        //                    context.SaveChanges();
        //                    var userdata = context.TblUsers.Where(x => (x.UserName == user.User_Name) && (x.MobileNumber == user.Mobile_Number)).FirstOrDefault();
        //                    TblUserrole objuserrole = new TblUserrole();
        //                    objuserrole.UserId = userdata.UserId;
        //                    objuserrole.Roleid = user.UserRole;
        //                    objuserrole.CreatedDate = DateTime.Now;
        //                    objuserrole.CreatedBy = user.CreatedBy;
        //                    context.TblUserroles.Add(objuserrole);
        //                    context.SaveChanges();

        //                    return JsonConvert.ToString(obj.UserName + " Saved Successfuly");
        //                }

        //            }


        //            return JsonConvert.ToString("User Name is Already Exists");


        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //}


        public string AddUser(AddUserVo user)
        {
            try
            {

                Encryption enc = new Encryption();
                EncryptedDataVo objEnc = new EncryptedDataVo();
                objEnc = enc.EncryptAesManaged(user.Password);

                using (var context = new bhishak_app_dbContext())
                {
                    if (user.Speciality != 0 && user.Speciality != null)
                    {
                        var speciality = (from x in context.TblAdmSpecialities
                                          where x.SpecialityId == user.Speciality
                                          select x.SpecialityDesc
                                          ).First();

                        TblUser obj = new TblUser();
                        obj.FirstName = user.First_Name;
                        obj.LastName = user.Last_Name;
                        obj.ListName = "Dr." + user.First_Name + " " + user.Last_Name + " (" + speciality + ")";
                        obj.RoleId = user.UserRole;

                        if (user.User_Name != "")
                        {
                            obj.UserName = user.User_Name;
                        }


                        obj.MobileNumber = user.Mobile_Number;
                        obj.FacilityId = user.facility_id;
                        obj.OrganizationId = user.Organization_id;
                        obj.Status = true;
                        if (user.Password != "")
                        {
                            obj.Password = user.Password;
                        }

                        obj.EncryptedPassword = objEnc.EncryptedPassword;
                        obj.EncryptedKey = objEnc.Key;
                        obj.EncryptedIv = objEnc.IV;
                        if (user.isReferDoctor == true)
                        {
                            obj.IsProvider = false;
                        }
                        else obj.IsProvider = true;
                        if (user.Speciality != 0)
                        {
                            obj.SpecialityId = user.Speciality;
                        }

                        obj.CreatedDate = DateTime.Now;
                        obj.CreatedBy = user.CreatedBy;
                        context.TblUsers.Add(obj);
                        context.SaveChanges();
                        var userdata = context.TblUsers.Where(x => (x.FirstName == user.First_Name) && (x.MobileNumber == user.Mobile_Number)).FirstOrDefault();
                        TblUserrole objuserrole = new TblUserrole();
                        objuserrole.UserId = userdata.UserId;
                        objuserrole.Roleid = user.UserRole;
                        objuserrole.CreatedDate = DateTime.Now;
                        objuserrole.CreatedBy = user.CreatedBy;
                        context.TblUserroles.Add(objuserrole);
                        context.SaveChanges();

                        return JsonConvert.ToString(obj.UserName + " Saved Successfuly");

                    }
                    else
                    {
                        var checkIsExist = context.TblUsers.Where(x => (x.UserName == user.User_Name)).FirstOrDefault();
                        if (checkIsExist == null)
                        {
                            TblUser obj = new TblUser();
                            obj.FirstName = user.First_Name;
                            obj.LastName = user.Last_Name;
                            obj.UserName = user.User_Name;
                            obj.MobileNumber = user.Mobile_Number;
                            obj.FacilityId = user.facility_id;
                            //obj.FirstName = fclist;
                            obj.OrganizationId = user.Organization_id;
                            obj.Status = true;
                            obj.Password = user.Password;
                            obj.EncryptedPassword = objEnc.EncryptedPassword;
                            obj.EncryptedKey = objEnc.Key;
                            obj.EncryptedIv = objEnc.IV;
                            obj.FacilityList = user.FacilityList;
                            obj.CreatedDate = DateTime.Now;
                            obj.CreatedBy = user.CreatedBy;
                            context.TblUsers.Add(obj);
                            context.SaveChanges();
                            var userdata = context.TblUsers.Where(x => (x.UserName == user.User_Name) && (x.MobileNumber == user.Mobile_Number)).FirstOrDefault();
                            TblUserrole objuserrole = new TblUserrole();
                            objuserrole.UserId = userdata.UserId;
                            objuserrole.Roleid = user.UserRole;
                            objuserrole.CreatedDate = DateTime.Now;
                            objuserrole.CreatedBy = user.CreatedBy;
                            context.TblUserroles.Add(objuserrole);
                            context.SaveChanges();

                            return JsonConvert.ToString(userdata.UserId);
                        }

                    }


                    return JsonConvert.ToString("User Name is Already Exists");


                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public List<PathlistVo> GetPathList(string Role)
        {
           using(var context=new bhishak_app_dbContext())
            {
                List<PathlistVo> objList = new List<PathlistVo>();
                PathlistVo obj = new PathlistVo();
                var RoleInfo = context.TblRoles.Where(x => x.RoleName == Role).FirstOrDefault();
                var list = context.TblComponentMappings.Where(x => x.Roleid == RoleInfo.RoleId).ToList();
              for(int i=0;i<list.Count;i++)
                {
                    obj.Path = list[i].MappingUrl;
                    obj.Role = Role;
                    obj.RoleId = list[i].Roleid;
                    objList.Add(obj);
                }
            


                return objList;
                    

            }
        }
        public Boolean AddFacility(AddFacilityVo facility)
        {
            using(var context =new bhishak_app_dbContext())
            {
                TblAdmFacility obj = new TblAdmFacility();
                obj.FacilityName = facility.FacilityName;
                obj.Createdby = facility.CreatedBy;
                obj.CreatedDate = DateTime.Now;
            }
            return true;
        }
        public int AddOrganizations(List<AddOrg_facilityVo> obj)
         {
            int response = 0;
            try
            {
               
                int orgId = 0;
                int facilityid = 0;
                bhishak_app_dbContext context = new bhishak_app_dbContext();
               
                var orgList = context.TblOrganizations.Where(x => x.OrganizationName.ToLower() == obj[0].Organization.ToLower()).FirstOrDefault();

                if (orgList == null)
                {

                    TblOrganization objOrg = new TblOrganization();
                    objOrg.OrganizationName = obj[0].Organization;
                    objOrg.Createdby = obj[0].CreatedBy;
                    objOrg.CreatedDate = DateTime.Now;
                    objOrg.Address = obj[0].Address;
                    objOrg.Organizationimage = obj[0].organizationimage;
                    context.TblOrganizations.Add(objOrg);
                    context.SaveChanges();
                    orgId = objOrg.OrganizationId;
                }
                else orgId = orgList.OrganizationId;

                    for (int i = 0; i < obj.Count; i++)
                    {

                    if (obj[i].Facility != "" && obj[i].Facility != null)
                    {
                        var facilitydata = context.TblAdmFacilities.Where(x => x.FacilityName.ToLower().Replace(" ", "") == obj[i].Facility.ToLower().Replace(" ", "")).FirstOrDefault();
                        if (facilitydata == null)
                        {
                            TblAdmFacility tbladmfacility = new TblAdmFacility();
                            tbladmfacility.Createdby = obj[i].CreatedBy;
                            tbladmfacility.FacilityName = obj[i].Facility;
                            tbladmfacility.CreatedDate = DateTime.Now;
                            context.TblAdmFacilities.Add(tbladmfacility);
                            context.SaveChanges();
                            facilityid = tbladmfacility.FacilityId;
                        }
                        else facilityid = facilitydata.FacilityId;

                        var data = (from x in context.TblFacilityMappings
                                    where x.OrganizationId == orgId && x.FacilityId == facilityid
                                    select x
                                  ).FirstOrDefault();
                        if (data == null)
                        {
                            TblFacilityMapping tblfacilitymapping = new TblFacilityMapping();
                            tblfacilitymapping.OrganizationId = orgId;
                            tblfacilitymapping.FacilityId = facilityid;
                            tblfacilitymapping.Createdby = obj[i].CreatedBy;
                            tblfacilitymapping.CreatedDate = DateTime.Now;
                            tblfacilitymapping.Address = obj[i].FacilityAddress;
                            context.TblFacilityMappings.Add(tblfacilitymapping);
                            response = response + context.SaveChanges();
                        }
                    }
                    else return -1;
                 
                          

             }
                return response;     
            }
            catch(Exception e)
            {
                throw e;
            }
           
           
            
        }
        public async Task<int> UpdateOrganization(UpdateOrganizationVo obj)
        {
            using(var _context=new bhishak_app_dbContext())
            {
                var tblOrg = _context.TblOrganizations.Where(x => x.OrganizationId == obj.OrganizationId).FirstOrDefault();
                tblOrg.OrganizationName = obj.Organization;
                tblOrg.Address = obj.Address;
                tblOrg.Organizationimage = obj.organizationimage;
                return await _context.SaveChangesAsync();
            }
          
        }
        public async Task<int> UpdateFacility(UpdateFacilityVo obj)
        {
            int Result = 0;
            using (var _context = new bhishak_app_dbContext())
            {
                var tblFmap = await (from x in _context.TblFacilityMappings
                                      where x.FacilityMappingId == obj.FacilityMappingId
                                      select x).FirstOrDefaultAsync();
                if(tblFmap != null)
                {
                    tblFmap.Address = obj.Address;
                   Result= _context.SaveChanges();
                    var tblFacility = await _context.TblAdmFacilities.Where(x => x.FacilityId == tblFmap.FacilityId).FirstOrDefaultAsync();
                    if (tblFacility != null)
                    {
                        tblFacility.FacilityName = obj.Facility;
                        Result +=   _context.SaveChanges();
                    }
                }
                return Result;


            }
        }
        public async Task<int> UpdateUser(GetUserDetailsForUpdateVo User,string Username)
        {
            try
            {

                int resp = 0;
                using(var _context=new bhishak_app_dbContext())
                {

                    var tbluser = await _context.TblUsers.Where(x => x.UserId == User.UserId).FirstOrDefaultAsync();
                    if (tbluser != null)
                    {
                        tbluser.FirstName = User.FirstName;
                        tbluser.LastName = User.LastName;
                        tbluser.FacilityId = User.FacilityId;
                        tbluser.SpecialityId = User.SpecialityId == 0 ? null : User.SpecialityId;
                        tbluser.RoleId = User.RoleId;
                        tbluser.ModifiedBy = Username;
                        tbluser.CreatedDate = DateTime.Now;
                        if (User.isProvider == 1)
                        {
                            tbluser.IsProvider = true;
                        }
                        else if (User.isProvider == 0)
                        {
                            tbluser.IsProvider = false;
                        }else tbluser.IsProvider = null;
                        tbluser.MobileNumber = Convert.ToInt64(User.MobileNumber);

                        resp += await _context.SaveChangesAsync();
                        var userroles = _context.TblUserroles.Where(x => x.UserId == User.UserId).FirstOrDefault();
                        userroles.Roleid = User.RoleId;
                        userroles.ModifiedBy = Username;
                        userroles.ModifiedDate = DateTime.Now;
                        resp += await _context.SaveChangesAsync();
                    }
                }

               
                return resp;
            }
            catch (Exception e) { throw e; }

        }

        public async Task<int> UpdateUserFacilities(string Facilities, int UserId, int DefaultFid, string UpdatedBy)
        {

            using (var _context = new bhishak_app_dbContext())
            {
                var tbluser = _context.TblUsers.Where(x => x.UserId == UserId).FirstOrDefault();
                tbluser.FacilityList = Facilities;
                tbluser.FacilityId = DefaultFid;
                return await _context.SaveChangesAsync();

            }
        }


        public async Task<int> UpdateFacilitiesForUser(List<UpdateFacilitiesForUserVo> obj)
        {
            string FacilityString = "";
            using (var context = new bhishak_app_dbContext())
            {
                var user = context.TblUsers.Where(x => x.UserId == obj[0].UserId).FirstOrDefault();

                if (obj[0].FacilityListId != 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        if (i <= (obj.Count - 2))
                        {
                            FacilityString = FacilityString + obj[i].FacilityListId + ",";
                        }
                        else
                        {
                            FacilityString = FacilityString + obj[i].FacilityListId.ToString();

                        }

                    }
                    user.FacilityList = FacilityString;
                }



                user.FacilityId = obj[0].DefaultFacilityId;
                return await context.SaveChangesAsync();

            }
        }
    }
}
