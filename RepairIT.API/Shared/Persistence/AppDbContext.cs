using Microsoft.EntityFrameworkCore;
using RepairIT.API.Repairing.Domain.Model;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Shared.Extensions;

namespace RepairIT.API.Shared.Persistence;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //Clients
        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<Client>().HasKey(p => p.Id);
        builder.Entity<Client>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Client>().Property(p => p.Address).IsRequired();
        builder.Entity<Client>().Property(p => p.District).IsRequired();
        builder.Entity<Client>().Property(p => p.Email).IsRequired();
        builder.Entity<Client>().Property(p => p.Name).IsRequired().HasMaxLength(20);
        builder.Entity<Client>().Property(p => p.LastName).IsRequired();
        builder.Entity<Client>().Property(p => p.DateBirth).IsRequired().HasMaxLength(10);
        builder.Entity<Client>().Property(p => p.CellPhoneNumber).IsRequired();
        //Devices
        builder.UseSnakeCaseNamingConvention();
    }
    
}