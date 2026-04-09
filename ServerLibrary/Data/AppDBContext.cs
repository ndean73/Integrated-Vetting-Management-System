using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLibrary.Data
{
    public class AppDBContext: DbContext
      {
        public AppDBContext() { }
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Company> Companies{ get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts{ get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<Chiefdom> Chiefdoms { get; set; }
        public DbSet<ApplicationUser> Applicationusers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
