using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace ServerLibrary.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext>options): DbContext(options)
    {
        public DbSet<Person> t_Person { get; set; }
        public DbSet<Company> t_Company { get; set; }
        public DbSet<Address> t_Address { get; set; }
        public DbSet<Province> t_Province { get; set; }
        public DbSet<District> t_District{ get; set; }
        public DbSet<Village> t_Village { get; set; }
        public DbSet<Chiefdom> t_Chiefdom { get; set; }
        public DbSet<ApplicationUser> t_Applicationuser { get; set; }
    }
}
