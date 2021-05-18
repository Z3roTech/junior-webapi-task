using Microsoft.EntityFrameworkCore;
using ST_JuniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject
{
    public class AppDBContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<CRMUserInfo> CRMUserInfos { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}