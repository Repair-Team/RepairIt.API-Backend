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
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Device> Devices { get; set; }
    
    public DbSet<Report> Reports { get; set; }
    public DbSet<Technician> Technicians { get; set; }

    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
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
        
        
        //Data Technicians
        builder.Entity<Technician>()
            .HasData(
                new Technician
                {
                    Id = 1, Name = "Pedro", LastName = "Cayllahua", DateBirth = "25/01/2003",
                    CellPhoneNumber = "+51 924483206", Email = "jcayllahua@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("jose123"), Address = "Lima,Peru",
                    District = "Santiago de Surco"
                },
                new Technician
                {
                    Id = 2, Name = "Pablo", LastName = "Flores", DateBirth = "25/08/2000",
                    CellPhoneNumber = "+51 924123206", Email = "luis.flores@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("luis123"), Address = "Lima,Peru",
                    District = "Villa el Salvador"
                },
                new Technician
                {
                    Id = 3, Name = "Pinto", LastName = "Anco", DateBirth = "20/05/2000",
                    CellPhoneNumber = "+51 958123206", Email = "jorge.anco@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("jorge123"), Address = "Lima,Peru",
                    District = "Villa Maria"
                },
                new Technician
                {
                    Id = 4, Name = "Arturo",LastName = "Escobedo", DateBirth = "20/05/1980",
                    CellPhoneNumber = "+51 958123546", Email = "angel.bdo@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("angel124"), Address = "Lima,Peru",
                    District = "Victoria"
                }
               
            );
        //Relationships Technicians
        builder.Entity<Technician>()
            .HasMany(p => p.Reports)
            .WithOne(p => p.Technician)
            .HasForeignKey(p => p.TechnicianId);
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
        
        //Relationships Client
        builder.Entity<Client>()
            .HasMany(p => p.Devices)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientId);
        //Data
        builder.Entity<Client>()
            .HasData(
                new Client
                {
                    Id = 1, Name = "Jose", LastName = "Cayllahua", DateBirth = "25/01/2003",
                    CellPhoneNumber = "+51 924483206", Email = "jcayllahua@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("jose123"), Address = "Lima,Peru",
                    District = "Santiago de Surco"
                },
                new Client
                {
                    Id = 2, Name = "Luis", LastName = "Flores", DateBirth = "25/08/2000",
                    CellPhoneNumber = "+51 924123206", Email = "luis.flores@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("luis123"), Address = "Lima,Peru",
                    District = "Villa el Salvador"
                },
                new Client
                {
                    Id = 3, Name = "Jorge", LastName = "Anco", DateBirth = "20/05/2000",
                    CellPhoneNumber = "+51 958123206", Email = "jorge.anco@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("jorge123"), Address = "Lima,Peru",
                    District = "Villa Maria"
                },
                new Client
                {
                    Id = 4, Name = "Angel",LastName = "Escobedo", DateBirth = "20/05/1980",
                    CellPhoneNumber = "+51 958123546", Email = "angel.bdo@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("angel124"), Address = "Lima,Peru",
                    District = "Victoria"
                }
               
            );
        //Devices
        builder.Entity<Device>().ToTable("Devices");
        builder.Entity<Device>().HasKey(p => p.Id);
        builder.Entity<Device>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(p => p.name).IsRequired().HasMaxLength(30);
        builder.Entity<Device>().Property(p => p.description).IsRequired().HasMaxLength(90);
        builder.Entity<Device>().Property(p => p.imagePath).IsRequired();
        builder.Entity<Device>().Property(p => p.inventoryStatus).IsRequired().HasMaxLength(20);
        builder.Entity<Device>().Property(p => p.ClientId).IsRequired();
        
        
        //Devices Data

        builder.Entity<Device>()
            .HasData(
        
            new Device{Id = 1, name = "MyPersonalDevice", description = "For study", imagePath = "https://allmobiles.com.pe/wp-content/uploads/2021/12/Diseno-sin-titulo-18.png", inventoryStatus = "Me", ClientId = 1},
            new Device{Id = 2, name = "MyPersonalDevice", description = "For study", imagePath = "https://i.pinimg.com/originals/16/6c/d0/166cd0df2080407096c7fa4a25ebe842.png", inventoryStatus = "Me", ClientId = 2},
            new Device{Id = 3, name = "MyPersonalDevice", description = "For study", imagePath = "https://http2.mlstatic.com/D_NQ_NP_842282-MLA49947859519_052022-O.jpg", inventoryStatus = "Store", ClientId = 3},
            new Device{Id = 4, name = "MyPersonalDevice", description = "For study", imagePath = "https://img01.huaweifile.com/sg/ms/pe/pms/uomcdn/PE_HW_B2C/pms/202209/gbom/6941487237319/428_428_08FCA2D57166F8D219D7D665F2C69B20mp.png", inventoryStatus = "Repairing", ClientId = 4}
        );
        
        
        
        
        //Reports

        builder.Entity<Report>().ToTable("Reports");
        builder.Entity<Report>().HasKey(p => p.Id);
        builder.Entity<Report>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().Property(p => p.Description).IsRequired().HasMaxLength(150);
        builder.Entity<Report>().Property(p => p.DeviceId).IsRequired();
        builder.Entity<Report>().Property(p => p.TechnicianId).IsRequired();
        builder.UseSnakeCaseNamingConvention();
        
        //Reports Data

        builder.Entity<Report>()
            .HasData(
                new Report
                {
                    Id = 1, Description = "Este dispositivo presenta fallas con la funcion de touch.", DeviceId = 1,TechnicianId = 1
                },
                new Report
                {
                    Id = 2, Description = "Este dispositivo presenta fallos por ingreso de agua en los componentes.",DeviceId = 2,
                    TechnicianId = 2
                },
                new Report
                {
                    Id = 3,
                    Description =
                        "Este Disposito presenta lentitud, por motivos desconocidos. Se recomienda un mantenimiento para descartar problemas.",DeviceId = 3,
                    TechnicianId = 3
                },
                new Report
                {
                    Id = 4,
                    Description =
                        "Este dispositivo tiene problemas con la bocina derecha. Se requiere un cambio de bocina.",DeviceId = 4,
                    TechnicianId = 4
                }
            );
    }
}