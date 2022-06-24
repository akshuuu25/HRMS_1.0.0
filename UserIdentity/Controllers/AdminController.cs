using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS.Models;
using IdentityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;





namespace HRMS.Controllers
{
    [Authorize(Roles = "Admin , HR")]
    
    public class AdminController : Controller
       
    {

        
        private readonly AppDbContext _dbcontext;
        private readonly INotyfService _notyfService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(AppDbContext dbcontext, INotyfService notyfService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager)
        {
            _dbcontext = dbcontext;
            _notyfService = notyfService;
            this.roleManager = roleManager;
            this._userManager = _userManager;
        }


   
        //[Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
                var details = _dbcontext.tbl_createRole.ToList();
                return View(details);
            
        }
          





        



        public IActionResult Adminbunit()
        {
             return View();
            
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adminbunit(BusinessUnit businessUnit)
        {
            if (_dbcontext.tbl_businessunit.Count((a) => a.BUnitName == businessUnit.BUnitName) == 0)
            {

                _dbcontext.tbl_businessunit.Add(businessUnit);

                _notyfService.Success("You have successfully saved the data.");
            }
            else
            {
                _notyfService.Error("This data already exist!!!");
            }
            _dbcontext.SaveChanges();

            return RedirectToAction("BunitDashboard", "BunitDash");

        }



        public IActionResult AdminDept()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminDept(Department department)
        {
            if (_dbcontext.tbl_department.Count((a) => a.DeptName == department.DeptName) == 0)
            {
                _dbcontext.tbl_department.Add(department);
                _notyfService.Success("You have successfully saved the data.");
            }
            else
            {
                _notyfService.Error("This data already exist!!!");
            }
            _dbcontext.SaveChanges();
            return RedirectToAction("DeptDashboard", "DeptDash");

        }

        public IActionResult OnBoardingCheckPoint()
        {
            var businessUnit = _dbcontext.tbl_businessunit.ToList().Where(i => i.Status == true);



            foreach (var item in businessUnit)
            {

                ViewBag.businessUnit = new SelectList(businessUnit, "BUnitId", "BUnitName");

            }



            var department = _dbcontext.tbl_department.ToList().Where(i => i.Status == true);
            foreach (var item in department)
            {
                ViewBag.department = new SelectList(department, "DeptID", "DeptName");
            }

            var assignee = _dbcontext.tbl_assignee.ToList();
            ViewBag.assignee = new SelectList(assignee, "AssigneeId", "AssigneeName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnBoardingCheckPoint(OnBoardingCheckPoint onBoardCheckpoint)
        {
            if (_dbcontext.tbl_checkPoint.Count((a) => a.CheckPointName == onBoardCheckpoint.CheckPointName) == 0)
            {
                _dbcontext.tbl_checkPoint.Add(onBoardCheckpoint);
                _notyfService.Success("You have successfully saved the data.");
            }
            else
            {
                _notyfService.Error("This data already exist!!!");
            }
            _dbcontext.SaveChanges();
            return RedirectToAction("OnBoardDashboard", "OnBoarding");

        }



        public IActionResult OffBoardingCheckPoint()
        {
            var businessUnit = _dbcontext.tbl_businessunit.ToList().Where(i => i.Status == true);



            foreach (var item in businessUnit)
            {

                ViewBag.businessUnit = new SelectList(businessUnit, "BUnitId", "BUnitName");

            }

            var department = _dbcontext.tbl_department.ToList().Where(i => i.Status == true);
            foreach (var item in department)
            {
                ViewBag.department = new SelectList(department, "DeptID", "DeptName");
            }

            var assignee = _dbcontext.tbl_assignee.ToList();
            ViewBag.assignee = new SelectList(assignee, "AssigneeId", "AssigneeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OffBoardingCheckPoint(OffBoardingCheckPoint offBoardCheckpoint)
        {
            if (_dbcontext.tbl_offboardcheckPoint.Count((a) => a.offCheckPointName == offBoardCheckpoint.offCheckPointName) == 0)
            {

                _dbcontext.tbl_offboardcheckPoint.Add(offBoardCheckpoint);
                _notyfService.Success("You have successfully saved the data.");
            }
            else
            {
                _notyfService.Error("This data already exist!!!");
            }
            _dbcontext.SaveChanges();
            return RedirectToAction("OffBoardDashboard", "OffBoarding");

        }

        [Authorize]
        public IActionResult AddOrEdit(int id)
        {
            var users = _userManager.Users.ToList();
            ViewBag.users = new SelectList(users);

            var roles = roleManager.Roles.ToList();
            ViewBag.roles = new SelectList(roles);

            var details = _dbcontext.tbl_createRole.Find(id);

            if (details == null)
            {
                return NotFound();
            }

            return View(details);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult AddOrEdit(CreateRole createRole, int id)
        {
            bool IsAuthorExist = false;

            CreateRole cr = _dbcontext.tbl_createRole.Find(id);
            if (cr != null)
            {
                IsAuthorExist = true;
            }
            else
            {
                cr = new CreateRole();
            }
            if (ModelState.IsValid)
            {


                cr.Username = createRole.Username;
                cr.Rolename = createRole.Rolename;



                if (IsAuthorExist)
                {
                    _dbcontext.Update(cr);
                    _notyfService.Success("You have successfully update the data.");
                }
                else
                {
                    _dbcontext.Add(cr);
                }
                _dbcontext.SaveChanges();

            }
            return RedirectToAction("Admin");

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var details = _dbcontext.tbl_createRole.FirstOrDefault(m => m.Id == id);

            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var details = _dbcontext.tbl_createRole.Find(id);
            _dbcontext.tbl_createRole.Remove(details);
            _notyfService.Success("Record deleted successfully.");
            _dbcontext.SaveChanges();


            return RedirectToAction("Admin");
        }



    }





















    }

