using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using XUnitAssessment.Data;
using XUnitAssessment.Models;
using XUnitAssessment.Services.Interface;

namespace XUnitAssessment.Services
{
    public class AppuserRepository : IAppuser
    {
        private readonly XunitDbContext DbContext;

        public AppuserRepository( XunitDbContext DbContext )
        {
            this.DbContext = DbContext;
        }

        // Add AppUser
        public async Task<Appuser> AddAppuser(Appuser AppUser)
        {
            var ExistingAppUser = await DbContext.XunitAppuser.FirstOrDefaultAsync( a => a.UserName == AppUser.UserName );
            if(ExistingAppUser != null)
            {
                return null;
            }
            await DbContext.XunitAppuser.AddAsync(AppUser);
            await DbContext.SaveChangesAsync();
            return AppUser;
        }

        //Get All AppUser
        public async Task<IEnumerable<Appuser>> GetAllAppuserByUserType(string userType)
        {
            var UserType = await DbContext.XunitUsertype.FirstOrDefaultAsync(user => user.UserType == userType);
            Console.WriteLine(UserType);
            if (UserType != null )
            {
                var Appusers = await DbContext.XunitAppuser.Where(a => a.UserTypeId == UserType.UserTypeId).ToListAsync();

                return Appusers;
            }

            return null;


        }

        //Edit AppUser
        public async Task<Appuser> UpdateAppuser(int AppUserId, Appuser AppUser)
        {
            var ExistingAppusers = await DbContext.XunitAppuser.SingleOrDefaultAsync(option => option.AppUserId == AppUserId);
            if (ExistingAppusers != null)
            {
                if (AppUser.UserTypeId != null)
                {
                    ExistingAppusers.UserTypeId = AppUser.UserTypeId;
                }
                ExistingAppusers.UserName = AppUser.UserName ?? ExistingAppusers.UserName;
                ExistingAppusers.Password = AppUser.Password ?? ExistingAppusers.Password;
                ExistingAppusers.IsActive = AppUser.IsActive ?? ExistingAppusers.IsActive;

                await DbContext.SaveChangesAsync();
                return ExistingAppusers;
            }
            else
            {
                return null;
            }
        }

        //Delete AppUser
        public async Task<Appuser> DeleteAppuser(int AppUserId)
        {
        
            var AppUser = await DbContext.XunitAppuser.FindAsync(AppUserId);
            if (AppUser != null)
            {

                DbContext.XunitAppuser.Remove(AppUser);
                DbContext.SaveChanges();
                return (AppUser);

            }
            else
            {
                return null;
            }
        }
    }
}
