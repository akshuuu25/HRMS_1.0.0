using AspNetCoreHero.ToastNotification.Abstractions;
using HRMS.Models;
using IdentityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
    [Authorize(Roles = "Admin , HR")]

    public class OnBoardingController : Controller
    {
        private readonly AppDbContext _dbcontext;
        private readonly INotyfService _notyfService;

        public OnBoardingController(AppDbContext dbcontext, INotyfService notyfService)
        {
            _dbcontext = dbcontext;
            _notyfService = notyfService;

        }
        [HttpGet]
        public IActionResult Dashboard(string SearchString)
        {
            var result = from data in _dbcontext.tbl_onboardingrequest

                         join bunit in _dbcontext.tbl_businessunit on
                         data.BUnitId equals bunit.BUnitId

                         join dept in _dbcontext.tbl_department on
                         data.DeptID equals dept.DeptID

                         join sdept in _dbcontext.tbl_subdepartment on
                         data.SDeptID equals sdept.SDeptID

                         join rm in _dbcontext.tbl_reportingmanager on
                         data.ReportID equals rm.ReportID

                         select new OnBoardingRequest
                         {
                             EmpId = data.EmpId,
                             Firstname = data.Firstname,
                             Lastname = data.Lastname,
                             Email = data.Email,
                             Mobile = data.Mobile,
                             Doj = data.Doj,   
                             Designation = data.Designation,    
                             BUnitName = bunit.BUnitName,
                             DeptName = dept.DeptName,
                             SubDeptName = sdept.SubDeptName,
                             ReportManagerName = rm.ReportManagerName,

                         };
            var details = from x in result select x;
            if (!string.IsNullOrEmpty(SearchString))
            {
                details = details.Where(m => m.Firstname.Contains(SearchString));
                if (details.Count() == 0)
                {
                    _notyfService.Error("No data available");
                }
                else
                {
                    _notyfService.Success("Done");
                }
            }

            return View(details.AsNoTracking().ToList());


            //_notyfService.Information("Welcome To Dashboard!!", 5);
            //return View();
        }

        public IActionResult OnBoardingRequest()
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

            var reportingManager = _dbcontext.tbl_reportingmanager.ToList();
            ViewBag.reportingmanager = new SelectList(reportingManager, "ReportID", "ReportManagerName");
            return View();
        }


        [HttpGet]
        public JsonResult GetSubDepartments(int DeptID)
        {

            var subDepartments = _dbcontext.tbl_subdepartment.Where(x => x.DeptID == DeptID).ToList();
            return Json(new SelectList(subDepartments, "SDeptID", "SubDeptName"));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnBoardingRequest(OnBoardingRequest onboardingRequest)
        {
            if (_dbcontext.tbl_onboardingrequest.Count((a) => a.Email == onboardingRequest.Email && a.Mobile == onboardingRequest.Mobile) == 0)
            {
                _dbcontext.tbl_onboardingrequest.Add(onboardingRequest);
                _notyfService.Success("You have successfully saved the data.");
            }
            else
            {
                _notyfService.Error("This data already exist!!");
            }
            _dbcontext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var businessUnit = _dbcontext.tbl_businessunit.ToList();
            ViewBag.businessUnit = new SelectList(businessUnit, "BUnitId", "BUnitName");

            var department = _dbcontext.tbl_department.ToList();
            ViewBag.department = new SelectList(department, "DeptID", "DeptName");

            var subdept = _dbcontext.tbl_subdepartment.ToList();
            ViewBag.subdept = new SelectList(subdept, "SDeptID", "SubDeptName");

            var rm = _dbcontext.tbl_reportingmanager.ToList();
            ViewBag.rm = new SelectList(rm, "ReportID", "ReportManagerName");


            var details = _dbcontext.tbl_onboardingrequest.Find(id);
            return View(details);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OnBoardingRequest onBoardRequest,int id)
        {
           bool dataExist = false;

            OnBoardingRequest request = _dbcontext.tbl_onboardingrequest.Find(id);
            if (request != null)
            {
                dataExist = true;
            }
            else
            {
                request = new OnBoardingRequest();
            }
            if (ModelState.IsValid)
            {


                request.Firstname = onBoardRequest.Firstname;
                request.Lastname = onBoardRequest.Lastname;
                request.Email = onBoardRequest.Email;
                request.Mobile = onBoardRequest.Mobile; 
                request.Doj = onBoardRequest.Doj;
                request.Designation = onBoardRequest.Designation;
                request.BUnitId = onBoardRequest.BUnitId;
                request.DeptID = onBoardRequest.DeptID;
                request.SDeptID = onBoardRequest.SDeptID;
                request.ReportID = onBoardRequest.ReportID;



                if (dataExist)
                {
                    _dbcontext.Update(request);
                    _notyfService.Success("You have successfully update the data.");
                }
                else
                {
                    _dbcontext.Add(request);

                }
                _dbcontext.SaveChanges();


            }

            return RedirectToAction("Dashboard");

        }

        public IActionResult OnBoardDashboard(string SearchString)
        { 

            var result = from data in _dbcontext.tbl_checkPoint

                         join bunit in _dbcontext.tbl_businessunit on
                         data.BUnitId equals bunit.BUnitId

                         join dept in _dbcontext.tbl_department on
                         data.DeptID equals dept.DeptID

                         join assignee in _dbcontext.tbl_assignee on
                         data.AssigneeId equals assignee.AssigneeId


                         select new OnBoardingCheckPoint
                         {
                             CheckPointId = data.CheckPointId,
                             CheckPointName = data.CheckPointName,
                             BUnitName = bunit.BUnitName,
                             DeptName = dept.DeptName,
                             AssigneeName = assignee.AssigneeName,
                             Description = data.Description,
                          
                         };

            var details = from x in result select x;
            if (!string.IsNullOrEmpty(SearchString))
            {
                details = details.Where(m => m.CheckPointName.Contains(SearchString));
                if (details.Count() == 0)
                {
                    _notyfService.Error("No data available");
                }
                else
                {
                    _notyfService.Success("Done");
                }
            }

            return View(details.AsNoTracking().ToList());
        }


        public IActionResult AddOrEdit(int id)
        {
            var businessUnit = _dbcontext.tbl_businessunit.ToList();
            ViewBag.businessUnit = new SelectList(businessUnit, "BUnitId", "BUnitName");

            var department = _dbcontext.tbl_department.ToList();
            ViewBag.department = new SelectList(department, "DeptID", "DeptName");

            var assign = _dbcontext.tbl_assignee.ToList();
            ViewBag.assignee = new SelectList(assign, "AssigneeId", "AssigneeName"); 

            var details = _dbcontext.tbl_checkPoint.Find(id);
            return View(details);




        }

        [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(OnBoardingCheckPoint onBoardCheckpoint, int id)

        {


            bool dataExist = false;

            OnBoardingCheckPoint checkPoint = _dbcontext.tbl_checkPoint.Find(id);
            if (checkPoint != null)
            {
                dataExist = true;
            }
            else
            {
                checkPoint = new OnBoardingCheckPoint();
            }
            if (ModelState.IsValid)
            {


                checkPoint.CheckPointName = onBoardCheckpoint.CheckPointName;
                checkPoint.BUnitId = onBoardCheckpoint.BUnitId;
                checkPoint.DeptID = onBoardCheckpoint.DeptID;
                checkPoint.AssigneeId = onBoardCheckpoint.AssigneeId;
                checkPoint.Description = onBoardCheckpoint.Description; 



                if (dataExist)
                {
                    _dbcontext.Update(checkPoint);
                    _notyfService.Success("You have successfully update the data.");
                }
                else
                {
                    _dbcontext.Add(checkPoint);
                    
                }
                _dbcontext.SaveChanges();
               

            }
            
            return RedirectToAction("OnBoardDashboard");

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var details = _dbcontext.tbl_checkPoint.FirstOrDefault(m => m.CheckPointId == id);

            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var details = _dbcontext.tbl_checkPoint.Find(id);
            _dbcontext.tbl_checkPoint.Remove(details);
            _notyfService.Success("Record deleted successfully.");
            _dbcontext.SaveChanges();
           

            return RedirectToAction("OnBoardDashboard");
        }



    }
}
