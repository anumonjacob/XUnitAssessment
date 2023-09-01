using XUnitAssessment.Models;

namespace XUnitAssessment.Services.Interface
{
    public interface IUserType
    {
        public Task<Usertype> AddUserType(Usertype userType);

        public Task<IEnumerable<Usertype>> GetAllUserType();

        public Task<Usertype> UpdateUserType(int UserTypeId, Usertype userType );
       
    }
}
