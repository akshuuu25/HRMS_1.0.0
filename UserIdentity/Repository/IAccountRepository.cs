using HRMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HRMS.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(Register register);
        Task<SignInResult> PasswordSignInAsync(Login login);

        
        Task SignOut();
    }
}