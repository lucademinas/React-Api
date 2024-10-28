using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(CreateSysAdminDataSeed());

            base.OnModelCreating(modelBuilder);
        }

        private User[] CreateSysAdminDataSeed()
        {
            return new User[]
            {
                new User
                {
                    Id = -1,
                    Name = "Sysadmin",
                    Email = "sys@admin.com",
                    Password = "123",
                    StartDate = new DateTime(2024/9/21),
                    UserRol = "Sysadmin"

                }
            };
        }
    }
}
