using Microsoft.EntityFrameworkCore;
using StateMachine.Model.Entities;

namespace StateMachine.Data;

public class StateMachineDbContext : DbContext
{
    public StateMachineDbContext(DbContextOptions<StateMachineDbContext> options) : base(options)
    {
    }
    
    public StateMachineDbContext(string connectionString) : base(new DbContextOptionsBuilder<StateMachineDbContext>()
        .UseSqlServer(connectionString)
        .Options)
    {
    }
    
    //Add DbSet properties for your entities here
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<OnboardingSagaData> OnboardingSagaData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OnboardingSagaData>().HasKey(s => s.CorrelationId);
    }
}