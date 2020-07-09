using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS.DataAccess.Data
{
    public class RTSDBContext : IdentityDbContext<Employee>
    {
        public RTSDBContext(DbContextOptions<RTSDBContext> options)
          : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategorie> itemCategories { get; set; }
        public DbSet<Trnasaction> Trnasactions { get; set; }
        public DbSet<ItemRequest> itemRequests { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
            //.SelectMany(e => e.GetForeignKeys()))
            //    foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            modelBuilder.Entity<RequestStatus>().HasData(
               new RequestStatus() { Id = 1, Status = "approved" },
               new RequestStatus() { Id = 2, Status = "pending" },
               new RequestStatus() { Id = 3, Status = "rejected" });

            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole() { Name = "Admin" },
               new IdentityRole() { Name = "Employee" }
               );
        }
    }
}
