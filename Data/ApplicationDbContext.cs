using System;
using System.Collections.Generic;
using System.Text;
using EerCare.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EerCare.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<LineItem> LineItem { get; set; }

    }
}
