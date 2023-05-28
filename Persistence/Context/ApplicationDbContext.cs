using Application.Interfaces;
using Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        { 
            base.OnModelCreating(builder); 
        }

        async Task<int> IApplicationDbContext.SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Customer> Customers { get; set; }
        
    }
}
