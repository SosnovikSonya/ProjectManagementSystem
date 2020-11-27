using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkItemState> WorkItemStates { get; set; }
        public DbSet<WorkItemType> WorkItemTypes { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkItemRelation> WorkItemRelations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<WorkItemState>().ToTable("WorkItemState");
            modelBuilder.Entity<WorkItemType>().ToTable("WorkItemType");
            modelBuilder.Entity<WorkItem>().ToTable("WorkItem");
            modelBuilder.Entity<WorkItemRelation>().ToTable("WorkItemRelation");
        }    

        public void AddOrUpdate(ITable source)
        {
            var tableType = source.GetType();

            var target = Find(tableType, source.Id);

            if (target == null)
            {
                target = source;
                Attach(target);
            }

            foreach (var prop in tableType.GetProperties())
            {
                var propValue = prop.GetValue(source);
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    prop.SetValue(target, prop.GetValue(source));
                }
            }
        }
    }
}
