using Microsoft.EntityFrameworkCore;
using Models;

namespace Repo
{
    public class TrainingModule_DBContext: DbContext
    {
        private readonly string connectionString;
        public virtual DbSet<Script> Scripts { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<Topic> Answers { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<BaseMath> BasicMaths { get; set; } = null!;
        public virtual DbSet<Compiler> Compilers { get; set; } = null!;
        public virtual DbSet<MethodProgramm> MethodProgramms { get; set; } = null!;
        public virtual DbSet<SelectKey> SelectKeys { get; set; } = null!;

        public TrainingModule_DBContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().UseTphMappingStrategy();
        }
    }
}