using AspNetCoreHero.ToastNotification.Abstractions;
using IdentityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using HRMS.Models;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Controllers
{
    [Authorize(Roles = "Admin , HR")]

    public class OffBoardingController : Controller
    {
        private readonly AppDbContext _dbcontext;
        private readonly INotyfService _notyfService;

        public OffBoardingController(AppDbContext dbcontext, INotyfService notyfService)
        {
            _dbcontext = dbcontext;
            _notyfService = notyfService;

        }
        public IActionResult OffBoardingRequest()
        {
            
            return View();
        }

        public IActionResult OffBoardDashboard(string SearchString)
        {

            var result = from data in _dbcontext.tbl_offboardcheckPoint

                         join bunit in _dbcontext.tbl_businessunit on
                         data.BUnitId equals bunit.BUnitId

                         join dept in _dbcontext.tbl_department on
                         data.DeptID equals dept.DeptID

                         join assignee in _dbcontext.tbl_assignee on
                         data.AssigneeId equals assignee.AssigneeId


                         select new OffBoardingCheckPoint
                         {
                             offCheckPointId = data.offCheckPointId,
                             offCheckPointName = data.offCheckPointName,
                             BUnitName = bunit.BUnitName,
                             DeptName = dept.DeptName,
                             AssigneeName = assignee.AssigneeName,
                             Description = data.Description,

                         };
            var details = from x in result select x;
            if (!string.IsNullOrEmpty(SearchString))
            {
                details = details.Where(m => m.offCheckPointName.Contains(SearchString));
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

            var details = _dbcontext.tbl_offboardcheckPoint.Find(id);
            return View(details);




        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var details = _dbcontext.tbl_offboardcheckPoint.FirstOrDefault(m => m.offCheckPointId == id);

            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var details = _dbcontext.tbl_offboardcheckPoint.Find(id);
            _dbcontext.tbl_offboardcheckPoint.Remove(details);
            _notyfService.Success("Record deleted successfully.");
            _dbcontext.SaveChanges();


            return RedirectToAction("OffBoardDashboard");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(OffBoardingCheckPoint offBoardCheckpoint, int id)

        {


            bool dataExist = false;

            OffBoardingCheckPoint offCheckPoint = _dbcontext.tbl_offboardcheckPoint.Find(id);
            if (offCheckPoint != null)
            {
                dataExist = true;
            }
            else
            {
                offCheckPoint = new OffBoardingCheckPoint();
            }
            if (ModelState.IsValid)
            {


                offCheckPoint.offCheckPointName = offBoardCheckpoint.offCheckPointName;
                offCheckPoint.BUnitId = offBoardCheckpoint.BUnitId;
                offCheckPoint.DeptID = offBoardCheckpoint.DeptID;
                offCheckPoint.AssigneeId = offBoardCheckpoint.AssigneeId;
                offCheckPoint.Description = offBoardCheckpoint.Description;



                if (dataExist)
                {
                    _dbcontext.Update(offCheckPoint);
                    _notyfService.Success("You have successfully update the data.");
                }
                else
                {
                    _dbcontext.Add(offCheckPoint);

                }
                _dbcontext.SaveChanges();


            }

            return RedirectToAction("OffBoardDashboard");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Copy(int[] id)
        {

            string strtoint = string.Empty;
               if (id != null)
                {
                    strtoint = id.Select(a => a.ToString()).Aggregate((i, j) => i + "," + j);
                
                
                }
                SqlConnection con = new SqlConnection(@"Server = 192.168.5.241; Initial Catalog = HumanResourseManagement_Trainee; user id = sa; password = BDev2019$; MultipleActiveResultSets = True");
                SqlCommand cmd = new SqlCommand("Sp_CopyToOffBoarding", con);
                cmd.CommandType = CommandType.StoredProcedure;
           
                cmd.Parameters.AddWithValue("strtoint", strtoint);
         
                con.Open();
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {

                }
                _notyfService.Success("Copy data successfully");
                 con.Close();
            
           

            return RedirectToAction("OffBoardDashboard");
           
        }
    }
}
