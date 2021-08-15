using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities.Common;
using Wmsds.Entities.HEIS;
using Wmsds.Entities.WC;

namespace Wmsds.Dal
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("name = EntityContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityContext, Migrations.Configuration>("EntityContext"));
        }

        #region Common Classes
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Tehsil> Tehsils { get; set; }

        public DbSet<FinancialYear> FinancialYears { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<WaterCourse> WaterCourses { get; set; }
        #endregion

        #region Watercourse

        public DbSet<WcProject> WcProjects { get; set; }
        public DbSet<WcIdentification> WcIdentifications { get; set; }
        public DbSet<WcIdentificationDetail> WcIdentificationDetails { get; set; }

        #endregion

        #region HEIS
        public DbSet<HeisIdentification> HeisIdentifications { get; set; }
        public DbSet<HeisIdentificationDetail> HeisIdentificationDetails { get; set; }
        public DbSet<HeisVendor> HeisVendors { get; set; }

        #endregion

        #region Users
        public DbSet<AppUser> AppUsers { get; set; }
        #endregion
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
