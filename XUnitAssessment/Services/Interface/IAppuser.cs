using XUnitAssessment.Models;

namespace XUnitAssessment.Services.Interface
{
    public interface IAppuser
    {
        public Task<Appuser> AddAppuser(Appuser AppUser);

        public Task<IEnumerable<Appuser>> GetAllAppuserByUserType(string userType);

        public Task<Appuser> UpdateAppuser(int AppUserId, Appuser AppUser);

        public Task<Appuser> DeleteAppuser(int AppUserId);

        
    }
}
