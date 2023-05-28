using Microsoft.EntityFrameworkCore;
using Models;

namespace Repo
{
    public class User_DBContext : DbContext
    {
        private readonly string connectionString;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Tutor> Tutors { get; set; } = null!;
        public virtual DbSet<Researcher> Researchers { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<ScriptUser> ScriptUsers { get; set; } = null!;
        public virtual DbSet<TestUser> TestUsers { get; set; } = null!;
        public virtual DbSet<QuestionTestUser> QuestionTestUsers { get; set; } = null!;
        public virtual DbSet<TaskUser> TaskUsers { get; set; } = null!;

        public User_DBContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + connectionString);
        }
    }
}
