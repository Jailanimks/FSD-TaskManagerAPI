namespace TaskManager.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(): base("name=SqlConnection")
        {
        }
        public DbSet<TaskData> Tasks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskData>().HasKey(p => p.TaskId);
            modelBuilder.Entity<TaskData>().Property(c => c.TaskId)
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);

        }
    }
}
