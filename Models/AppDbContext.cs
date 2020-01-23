using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{

    public class AppDbContext :DbContext 
    {
        public DbSet<UserResumePage> UserResumePages { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) :base(dbContextOptions)
        {
            
        }
    }
}
