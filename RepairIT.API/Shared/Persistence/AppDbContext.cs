using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Shared.Extensions;

namespace RepairIT.API.Shared.Persistence;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Device> Devices { get; set; }
    
    public DbSet<Report> Reports { get; set; }
    public DbSet<Technician> Technicians { get; set; }

    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //Users
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        builder.Entity<User>().Property(p => p.Email).IsRequired();
        builder.Entity<User>().Property(p => p.Password).IsRequired();
        builder.Entity<User>().Property(p => p.IsTechnician).IsRequired().HasDefaultValue(false);
        builder.Entity<User>().Property(p => p.IsPremium).IsRequired().HasDefaultValue(false);
        
        
        
        
        //Technicians
        builder.Entity<Technician>().ToTable("Technicians");
        builder.Entity<Technician>().HasKey(p => p.Id);
        builder.Entity<Technician>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Technician>().Property(p => p.Address).IsRequired();
        builder.Entity<Technician>().Property(p => p.District).IsRequired();
        builder.Entity<Technician>().Property(p => p.Email).IsRequired();
        builder.Entity<Technician>().Property(p => p.Name).IsRequired().HasMaxLength(20);
        builder.Entity<Technician>().Property(p => p.LastName).IsRequired();
        builder.Entity<Technician>().Property(p => p.DateBirth).IsRequired().HasMaxLength(10);
        builder.Entity<Technician>().Property(p => p.CellPhoneNumber).IsRequired();
        
        
        
        //Devices
        builder.Entity<Device>().ToTable("Devices");
        builder.Entity<Device>().HasKey(p => p.Id);
        builder.Entity<Device>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(p => p.name).IsRequired().HasMaxLength(30);
        builder.Entity<Device>().Property(p => p.description).IsRequired().HasMaxLength(90);
        builder.Entity<Device>().Property(p => p.imagePath).IsRequired();
        builder.Entity<Device>().Property(p => p.inventoryStatus).IsRequired().HasMaxLength(20);
        builder.Entity<Device>().Property(p => p.UserId).IsRequired();
        
        

       
        
        
        
        
        //Reports

        builder.Entity<Report>().ToTable("Reports");
        builder.Entity<Report>().HasKey(p => p.Id);
        builder.Entity<Report>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().Property(p => p.Description).IsRequired().HasMaxLength(150);
        builder.Entity<Report>().Property(p => p.DeviceId).IsRequired();
        builder.Entity<Report>().Property(p => p.TechnicianId).IsRequired();
        builder.UseSnakeCaseNamingConvention();
        
      
        
      
       
    }
}