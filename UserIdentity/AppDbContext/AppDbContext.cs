
using HRMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IdentityFramework.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _options;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options = options;
        }


        public DbSet<Country> tbl_countries { get; set; }

        public DbSet<State> tbl_states { get; set; }

        public DbSet<City> tbl_cities { get; set; }
        public DbSet<PostalCode> tbl_postalcodes { get; set; }

        public DbSet<BusinessUnit> tbl_businessunit { get; set; }
        public DbSet<Department> tbl_department { get; set; }
        public DbSet<SubDepartment> tbl_subdepartment { get; set; }
        public DbSet<ReportingManager> tbl_reportingmanager { get; set; }
        public DbSet<Assignee> tbl_assignee { get; set; }

        public DbSet<OnBoardingRequest> tbl_onboardingrequest { get; set; }

        public DbSet<OnBoardingCheckPoint> tbl_checkPoint { get; set; }

        public DbSet<OffBoardingCheckPoint> tbl_offboardcheckPoint { get; set; }

        public DbSet<CreateRole> tbl_createRole { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }






        public DbSet<HRMS.Models.OffBoardingCheckPoint> OffBoardingCheckPoint { get; set; }






      //public DbSet<HRMS.ViewModels.OnBoardingCheckPointViewModel> OnBoardingCheckPointViewModel { get; set; }

    }
}
