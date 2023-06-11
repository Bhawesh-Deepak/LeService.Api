using LeService.Api.DataLayer.DBModel;
using LeService.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer
{
    public class LeContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=89.163.218.70\\MSSQLSERVER2017; Database= IGL_Development; User Id=igl;Password = Manoj@12345");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
        public virtual DbSet<CategoryModel>  CategoryModel{ get; set; }
        public virtual DbSet<ServiceDetail> ServiceModel { get; set; }
        public virtual DbSet<AppointmentModel> AppointmentModels { get; set; }

        public virtual DbSet<VendorBasicInformationModel> VendorBasicInformationModels { get; set; }
        public virtual DbSet<VendorServiceDetails> VendorServiceDetails { get; set; }
        public virtual DbSet<ContactUsModel> ContactUsModels { get; set; }
        public virtual DbSet<ReasonMaster> ReasonMasters { get; set; }
        public virtual DbSet<TransportAppintmentModel> TransportAppintmentModels { get; set; }
        public virtual DbSet<LoginModel> LoginModels { get; set; }
        public virtual DbSet<DriverInformation> DriverInformation { get; set; }
        public virtual DbSet<VehicleMaster> VehicleMasters { get; set; }
    }
}
