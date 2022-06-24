using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS.Models;
using IdentityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
    [Authorize(Roles = "Admin , HR")]

    public class DeptDashController : Controller
    {
        private readonly AppDbContext _dbcontext;
        private readonly INotyfService _notyfService;

        public DeptDashController(AppDbContext dbcontext, INotyfService notyfService)
        {
            _dbcontext = dbcontext;
            _notyfService = notyfService;
        }
        public async Task<IActionResult> DeptDashboard(string SearchString)
        {
            ViewData["GetAllDepartment"] = SearchString;
            var details = from x in _dbcontext.tbl_department select x;
            if (!string.IsNullOrEmpty(SearchString))
            {
                details = details.Where(m => m.DeptName.Contains(SearchString));
                if (details.Count() == 0)
                {
                    _notyfService.Error("No data available");
                }
                else
                {
                    _notyfService.Success("Done");
                }
               
            }

            return View(await details.AsNoTracking().ToListAsync());
        }

        public IActionResult AddOrEdit(int id)
        {




            var details = _dbcontext.tbl_department.Find(id);

            if (details == null)
            {
                return NotFound();
            }

            return View(details);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(Department dept, int id)
        {
            bool IsDeptExist = false;

            Department depts = _dbcontext.tbl_department.Find(id);
            if (depts != null)
            {
                IsDeptExist = true;
            }
            else
            {
                depts = new Department();
            }
            if (ModelState.IsValid)
            {


                depts.DeptName = dept.DeptName;
                depts.Status = dept.Status;



                if (IsDeptExist)
                {
                    _dbcontext.Update(depts);
                    _notyfService.Success("You have successfully update the data.");
                }
                else
                {
                    _dbcontext.Add(depts);
                }
                _dbcontext.SaveChanges();

            }
            return RedirectToAction("DeptDashboard");

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            var details = _dbcontext.tbl_department.FirstOrDefault(m => m.DeptID == id);

            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var details = _dbcontext.tbl_department.Find(id);


            var ondetails = _dbcontext.tbl_onboardingrequest.Find(id);
            var oncheckdetails = _dbcontext.tbl_checkPoint.Find(id);
            var offdetails = _dbcontext.tbl_offboardcheckPoint.Find(id);


            bool onExist = (from x in _dbcontext.tbl_onboardingrequest
                            where x.EmpId == id
                            select x).Any();

            bool oncheckExist = (from x in _dbcontext.tbl_checkPoint
                                 where x.CheckPointId == id
                                 select x).Any();

            bool offExist = (from x in _dbcontext.tbl_offboardcheckPoint
                             where x.offCheckPointId == id
                             select x).Any();



            if (onExist || oncheckExist || offExist)
            {
                _notyfService.Information("Can't remove. Exist in another table.", 3);
                return RedirectToAction("BunitDashboard");
            }
            else
            {
                //_dbcontext.tbl_onboardingrequest.Remove(ondetails);
                //_dbcontext.tbl_checkPoint.Remove(oncheckdetails);
                //_dbcontext.tbl_offboardcheckPoint.Remove(offdetails);

                _dbcontext.tbl_department.Remove(details);
                _notyfService.Success("Record deleted successfully.");
                _dbcontext.SaveChanges();
            }

            //Add new record 


            return RedirectToAction("DeptDashboard");

        }





    }
}
