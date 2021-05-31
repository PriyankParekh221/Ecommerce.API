using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<ProductViewModel> Product { get; set; }
        public DbSet<OrderTransectionViewModel> Order { get; set; }
        public DbSet<OrderDetailViewModel> OrderDetail { get; set; }
    }
}
