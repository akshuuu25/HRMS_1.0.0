using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS.Email;
using HRMS.Models;
using HRMS.Repository;
using HRMS.ViewModel;
using IdentityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UserIdentity.Controllers;


namespace HRMS.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbcontext;
        private readonly IAccountRepository _accountRepository;
        private readonly INotyfService _notyfService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;



        // Country country = new Country();

        public AccountController(AppDbContext dbcontext, IAccountRepository accountRepositoty , INotyfService notyfService , RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> _userManager , ILogger<AccountController> logger)
        {
            _dbcontext = dbcontext;
            _accountRepository = accountRepositoty;
            _notyfService = notyfService;
            this.roleManager = roleManager;
            this._userManager = _userManager;
            _logger = logger;

        }

        public IActionResult AdminAccess()
        {
            return View();
        }

        public IActionResult Register()

        {

            
                var countries = _dbcontext.tbl_countries.ToList();
                ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
                return View();

        }
     
       
           
       

      [HttpGet]
        public JsonResult GetStates(int CountryId)
        {
 
            var states = _dbcontext.tbl_states.Where(x => x.CountryId == CountryId).ToList();
            return Json(new SelectList(states, "StateId", "StateName"));
        
        }

        [HttpGet]
        public JsonResult GetCities(int StateId)
        {
            var cities = _dbcontext.tbl_cities.Where(x=>x.StateId == StateId).ToList();
            return Json(new SelectList(cities, "CityId", "CityName"));
        }


        [HttpGet]
        public JsonResult GetPostalcodes(int CityId)
        {
            var pcodes = _dbcontext.tbl_postalcodes.Where(x => x.CityId == CityId).ToList();
            return Json(new SelectList(pcodes, "PostalId", "PostalNo"));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {

                var result = await _accountRepository.CreateUserAsync(register);
                if (!result.Succeeded)
                {

                    _notyfService.Error("Enter valid data");
                     return RedirectToAction("Index", "Home");
                }
                ModelState.Clear();
                _notyfService.Success("You have successfully registered your self.");
                return RedirectToAction("Index", "Home"); 
            }
            return RedirectToAction("Index", "Home");


        }

         
        public IActionResult LogIn()
        {


            return View();
               
            
        }


        [HttpPost]
      
        public async Task<IActionResult> LogIn(Login login)
        {
            if (ModelState.IsValid)
            {

                var identityResult = await _accountRepository.PasswordSignInAsync(login);
                

                    if (identityResult.Succeeded)
                    { 
                    _notyfService.Information("Welcome!!", 5);
                   
                    var currentUser = _userManager.FindByNameAsync(login.Email).Result;
                    if (await _userManager.IsInRoleAsync(currentUser, "Admin") ||
                        await _userManager.IsInRoleAsync(currentUser, "HR")) //<= Checking Role and redirecting accordingly.
                    {
                        return RedirectToAction("Admin", "Admin");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Username", login.Email);
                        return RedirectToAction("Index", "Home");
                    }

                  



                   }
              
                _notyfService.Error("Username and password is incorrect");
                //ModelState.AddModelError(string.Empty, "Username and password is incorrect");
            }
            
            return RedirectToAction("Index", "Home");
        }

        


        public async Task<IActionResult> logout()
        {
         
            await _accountRepository.SignOut();

          
           
           
            _notyfService.Information("Logging Out", 3);
            return RedirectToAction("LogIn", "Account");
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            var users = _userManager.Users.ToList();
            ViewBag.users = new SelectList(users);

            var roles = roleManager.Roles.ToList();
            ViewBag.roles = new SelectList(roles);

            return View();



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> CreateRole(CreateRole vm)
        {
            
            var RoleExist = await roleManager.RoleExistsAsync(vm.Rolename);
            if (RoleExist)
            {
                var currentUser = _userManager.FindByNameAsync(vm.Username).Result;

                

                await _userManager.AddToRoleAsync(currentUser, vm.Rolename);

                _dbcontext.tbl_createRole.Add(vm);
              
                _dbcontext.SaveChanges();
                if (await _userManager.IsInRoleAsync(currentUser, "Admin") ||
                      await _userManager.IsInRoleAsync(currentUser, "HR"))
                {
                    _notyfService.Success("Data added successfully.");
                    return RedirectToAction("Admin", "Admin");
                }



                    return RedirectToAction("Index", "Home");
            }
            else
            {
               
                return View();

            }



        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null)
                {
                    // Generate the reset password token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    // Log the password reset link
                  
                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, passwordResetLink);
                    if (emailResponse)
                    {
                        // Send the user to Forgot Password Confirmation view
                        return View("ForgotPasswordConfirmation");
                    }
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                
            }

            return View(model);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            
            return View();
        }


    }
}
