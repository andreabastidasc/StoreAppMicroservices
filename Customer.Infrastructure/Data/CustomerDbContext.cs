using Microsoft.EntityFrameworkCore;
using Customer.Domain.Entities;
using System.Collections.Generic;

namespace Customer.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
    }
}
