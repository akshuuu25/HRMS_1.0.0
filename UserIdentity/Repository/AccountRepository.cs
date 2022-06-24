using HRMS.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HRMS.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;

        public AccountRepository(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public async Task<IdentityResult> CreateUserAsync(Register register)
        {
            var user = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.Email,
                FullName = register.FullName,
                Gender = register.Gender,
                CountryId = register.CountryId,
                StateId = register.StateId,
                CityId = register.CityId,
                PostalId = register.PostalId,
                Address = register.Address



            };

            var result = await _userManager.CreateAsync(user, register.Password);
           
            return result;

        }

       

        public async Task<SignInResult> PasswordSignInAsync(Login login)
        {
            return await _signinManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
        }

        public async Task SignOut()
        {
            await _signinManager.SignOutAsync();
        }

    }
}
