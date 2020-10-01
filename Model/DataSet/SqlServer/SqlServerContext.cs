using Microsoft.EntityFrameworkCore;

namespace Model.DataSet.SqlServer
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<WordDataSet> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=KEA;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordDataSet>().HasKey(a => a.Guid);
            base.OnModelCreating(modelBuilder);
        }
    }
}
