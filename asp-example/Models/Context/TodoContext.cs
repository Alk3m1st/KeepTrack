using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace asp_example.Models.Context
{
    // http://msdn.microsoft.com/en-us/data/jj193542
    public class TodoContext : DbContext
    {
        public TodoContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Todo> Todos { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Todo>().HasKey<int>(e => e.Id);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}