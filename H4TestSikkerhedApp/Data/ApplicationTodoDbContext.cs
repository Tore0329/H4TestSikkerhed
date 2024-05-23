using H4TestSikkerhedApp.Model;
using Microsoft.EntityFrameworkCore;

namespace H4TestSikkerhedApp.Data
{
    public partial class ApplicationTodoDbContext : DbContext
    {
        public ApplicationTodoDbContext()
        {
        }

        public ApplicationTodoDbContext(DbContextOptions<ApplicationTodoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cpr> Cprs { get; set; }

        public virtual DbSet<TodoList> TodoLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Todo;Trusted_Connection=True;MultipleActiveResultSets=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cpr>(entity =>
            {
                entity.ToTable("Cpr");

                entity.Property(e => e.CprNr).HasMaxLength(500);
                entity.Property(e => e.User).HasMaxLength(500);
            });

            modelBuilder.Entity<TodoList>(entity =>
            {
                entity.ToTable("Todolist");

                entity.Property(e => e.Item).HasMaxLength(500);

                entity.HasOne(d => d.User).WithMany(p => p.TodoLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Todolist_Cpr");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
