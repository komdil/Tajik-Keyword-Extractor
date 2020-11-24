using Microsoft.EntityFrameworkCore;

namespace Model.DataSet.SqlServer
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<WordDataSet> WordsDataSet { get; set; }

        public DbSet<BandakDataSet> BandakDataSets { get; set; }

        public DbSet<JonishinDataSet> JonishinDataSets { get; set; }

        public DbSet<PeshoyandDataSet> PeshoyandDataSets { get; set; }

        public DbSet<BookDataSet> BookDataSets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=KEA;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<BandakDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<JonishinDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<PeshoyandDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<BookDataSet>().HasKey(a => a.Guid);
            base.OnModelCreating(modelBuilder);
        }
    }
}
