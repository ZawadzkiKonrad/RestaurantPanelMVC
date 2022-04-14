using RestaurantPanelMVC.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPanelMVC.Infrastructure
{
    public class Context : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUsers { get; set; }
       




        public Context(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ////wiele do wielu:
            //builder.Entity<PostTag>()
            //.HasKey(pt => new { pt.PostId, pt.TagId });

        }
    }
}
