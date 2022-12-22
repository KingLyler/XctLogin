using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XctLogin.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace XctLogin.Data_Access_Layer
{
    public class LoginContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    } 
}
    