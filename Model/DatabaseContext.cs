using Microsoft.EntityFrameworkCore;
namespace Api.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        modelBuilder.HasSequence<int>("employee_id", schema: "dbo")
            .StartsAt(1)
            .IncrementsBy(1);
            //builder.ForNpgsqlUseIdentityColumns();

        modelBuilder.Entity<Employee>()
            .Property(o => o.employee_id);
            // .HasDefaultValueSql("NEXT VALUE FOR dbo.Order_seq");
         modelBuilder.HasSequence<int>("id", schema: "dbo")
            .StartsAt(1)
            .IncrementsBy(1);
            //builder.ForNpgsqlUseIdentityColumns();

        modelBuilder.Entity<Login>()
            .Property(o => o.id);
            // .HasDefaultValueSql("NEXT VALUE FOR dbo.Order_seq");
              modelBuilder.HasSequence<int>("id", schema: "dbo")
            .StartsAt(1)
            .IncrementsBy(1);
            //builder.ForNpgsqlUseIdentityColumns();

        modelBuilder.Entity<Attendance>()
            .Property(o => o.id);
            // .HasDefaultValueSql("NEXT VALUE FOR dbo.Order_seq");
        }
    }
}


