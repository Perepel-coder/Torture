using Microsoft.EntityFrameworkCore;
using Models;

namespace Repo
{
    public class Cryptoalgorithm_DBContext : DbContext
    {
        private readonly string connectionString;
        public virtual DbSet<Cryptoalgorithm> Cryptoalgorithms { get; set; } = null!;
        public virtual DbSet<NumOfKey> NumOfKeys { get; set; } = null!;
        public virtual DbSet<Mode> Modes { get; set; } = null!;

        public Cryptoalgorithm_DBContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source="+connectionString);
        }
    }
}
