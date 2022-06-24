using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS.Models;
using IdentityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
    [Authorize(Roles = "Admin , HR")]

    public class BunitDashController : Controller
    {
        private readonly AppDbContext _dbcontext;
        private readonly INotyfService _notyfService;

        
        public BunitDashController(AppDbContext dbcontext, INotyfService notyfService)
        {
            _dbcontext = dbcontext;
            _notyfService = notyfService;
        }


        [HttpGet]
        public async Task<IActionResult> BunitDashboard(string SearchString)
        {
            
            ViewData["GetAllBunitList"] = SearchString;
            var details = from x in _dbcontext.tbl_businessunit select x;
          
          
           
            if (!string.IsNullOrEmpty(SearchString))
            {
                details = details.Where(m => m.BUnitName.Contains(SearchString));
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




            var details = _dbcontext.tbl_businessunit.Find(id);

            if (details == null)
            {
                return NotFound();
            }

            return View(details);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(BusinessUnit bunit, int id)
        {
            bool IsAuthorExist = false;

            BusinessUnit bunits = _dbcontext.tbl_businessunit.Find(id);
            if (bunits != null)
            {
                IsAuthorExist = true;
            }
            else
            {
                bunits = new BusinessUnit();
            }
            if (ModelState.IsValid)
            {


                bunits.BUnitName = bunit.BUnitName;
                bunits.Status = bunit.Status;



                if (IsAuthorExist)
                {
                    _dbcontext.Update(bunits);
                    _notyfService.Success("You have successfully update the data.");
                }
                else
                {
                    _dbcontext.Add(bunits);
                }
                _dbcontext.SaveChanges();

            }
            return RedirectToAction("BunitDashboard");

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            var details = _dbcontext.tbl_businessunit.FirstOrDefault(m => m.BUnitId == id);

            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var details = _dbcontext.tbl_businessunit.Find(id);


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
                _notyfService.Information("Can't remove. Exist in another table.",3);
                return RedirectToAction("BunitDashboard");
            }
            else {
            //_dbcontext.tbl_onboardingrequest.Remove(ondetails);
            //_dbcontext.tbl_checkPoint.Remove(oncheckdetails);
            //_dbcontext.tbl_offboardcheckPoint.Remove(offdetails);

            _dbcontext.tbl_businessunit.Remove(details);
            _notyfService.Success("Record deleted successfully.");
            _dbcontext.SaveChanges();
        }

         //Add new record 
            
           
            return RedirectToAction("BunitDashboard");
        }
    }
}
