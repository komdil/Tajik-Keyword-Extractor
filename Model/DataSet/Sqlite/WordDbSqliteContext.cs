using Microsoft.EntityFrameworkCore;

namespace Model.KEADataSet.Sqlite
{
    public class WordDbSqliteContext : DbContext
    {
        public DbSet<Word> word { get; set; }
        public DbSet<Dictionary> dictionary { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DataSet/KEA.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasKey(a => a._id);
            modelBuilder.Entity<Dictionary>().HasKey(a => a._id);
            //modelBuilder.Entity<Word>().HasOne(a => a.Dictionary).WithMany(d => d.Words).HasForeignKey(f => f.dictionary_id);
            //modelBuilder.Entity<Dictionary>().HasMany(a => a.Words).WithOne(d => d.Dictionary).HasForeignKey(f => f.dictionary_id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
