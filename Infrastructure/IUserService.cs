
using Data;
using Data.Models;
using Data.Repository;

namespace Infrastructure
{
    public interface IUserService : IRepository<ApplicationUser>
    {

        Task<Response> ChangeAction(int id);
        Task<Response> AssignPackage(int userId, int packageId);
        Task<Response> Assignpackage(int TID);

        Task<Response> TwoFactorEnabled(int id);
    }
}
