using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Model.DataSet.SqlServer
{
    public class SqlServerContext : DbContext, IWordContext
    {
        public SqlServerContext()
        {
            Database.EnsureCreated();
        }

        public List<IWordsDataSet> Bandaks { get => GetEntities<BookDataSet>().Cast<IWordsDataSet>().ToList(); }

        public List<IWordsDataSet> Jonishins { get => GetEntities<JonishinDataSet>().Cast<IWordsDataSet>().ToList(); }

        public List<IWordsDataSet> Peshoyands { get => GetEntities<PeshoyandDataSet>().Cast<IWordsDataSet>().ToList(); }

        public List<BookDataSet> BookDataSets { get => GetEntities<BookDataSet>().ToList(); }

        public List<IWordsDataSet> Words { get => GetEntities<WordDataSet>().Cast<IWordsDataSet>().ToList(); }

        public List<IWordsDataSet> StopWords { get => GetEntities<StopWord>().Cast<IWordsDataSet>().ToList(); }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return Set<T>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-C7K0FSC;Database=KEA;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<BandakDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<JonishinDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<PeshoyandDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<BookDataSet>().HasKey(a => a.Guid);
            modelBuilder.Entity<StopWord>().HasKey(a => a.Guid);
            base.OnModelCreating(modelBuilder);
        }
    }
}
