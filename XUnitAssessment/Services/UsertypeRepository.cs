using XUnitAssessment.Models;
using XUnitAssessment.Data;
using XUnitAssessment.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XUnitAssessment.Services
{
    public class UsertypeRepository : IUserType
    {
        private readonly XunitDbContext DbContext;

        public UsertypeRepository(XunitDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        // Add UserType
        public async Task<Usertype> AddUserType(Usertype userType)
        {
            await DbContext.XunitUsertype.AddAsync(userType);
            await DbContext.SaveChangesAsync();
            return userType;
        }

        //Get All UserType
        public async Task<IEnumerable<Usertype>> GetAllUserType()
        {
            var AllUsers = await DbContext.XunitUsertype.ToListAsync();
            if (AllUsers.Count != 0)
            {
                return AllUsers;
            }
            else
            {
                return null;
            }
           
        }

        //Edit UserType
        public async Task<Usertype> UpdateUserType(int UserTypeId, Usertype userType)
        {
            var ExistingUsertypes = await DbContext.XunitUsertype.SingleOrDefaultAsync(option => option.UserTypeId == UserTypeId);
            if(ExistingUsertypes != null)
            {
                if (userType.UserTypeId != null)
                {
                    ExistingUsertypes.UserTypeId = userType.UserTypeId;
                }
                ExistingUsertypes.UserType = userType.UserType ?? ExistingUsertypes.UserType;
                ExistingUsertypes.Description = userType.Description ?? ExistingUsertypes.Description;
                ExistingUsertypes.IsActive = userType.IsActive ?? ExistingUsertypes.IsActive;

                await DbContext.SaveChangesAsync();
                return ExistingUsertypes;
            }
            else
            {
                return null;
            }
        }


    }
}
