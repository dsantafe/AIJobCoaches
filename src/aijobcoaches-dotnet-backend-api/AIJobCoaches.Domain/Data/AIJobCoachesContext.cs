namespace AIJobCoaches.Domain.Data
{
    using AIJobCoaches.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AIJobCoachesContext : DbContext
    {
        public AIJobCoachesContext() { }

        public AIJobCoachesContext(DbContextOptions<AIJobCoachesContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        public DbSet<QuizResponse> QuizResponses { get; set; }
    }
}
