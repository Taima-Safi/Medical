using M_EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_EF.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SpecialtyModel> Specialities { get;set; }
        public DbSet<ServiceModel> Services { get;set; }
        public DbSet<DoctorModel> Doctors { get;set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ServiceModel>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<DoctorModel>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
