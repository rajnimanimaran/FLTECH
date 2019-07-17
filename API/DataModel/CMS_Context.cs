using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DataModel
{
    public class CMS_Context : DbContext
    {
        public CMS_Context()
            : base("name=CMS_Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // throw new UnintentionalCodeFirstException();
        }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Action> Action { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<OrganizationLevel> OrganizationLevel { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleMenuMapping> RoleMenuMapping { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<UserMenuMapping> UserMenuMapping { get; set; } 
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }
        // public DbSet<EmployeeDocuments> EmployeeDocuments { get; set; }
        public DbSet<EmployeeAcademy> EmployeeAcademy { get; set; }
        public DbSet<EmployeeExperience> EmployeeExperience { get; set; }
      
    }
}




