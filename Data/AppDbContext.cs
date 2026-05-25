using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s29820.Models.Entities;

namespace PJATK_APBD_Cw4_s29820.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Pc> PCs => Set<Pc>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<PcComponent> PCComponents => Set<PcComponent>();
    public DbSet<ComponentManufacturer> ComponentManufacturers => Set<ComponentManufacturer>();
    public DbSet<ComponentType> ComponentTypes => Set<ComponentType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurePc(modelBuilder);
        ConfigureComponent(modelBuilder);
        ConfigurePcComponent(modelBuilder);
        ConfigureComponentManufacturer(modelBuilder);
        ConfigureComponentType(modelBuilder);
        Seed(modelBuilder);
    }

    private static void ConfigurePc(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pc>(entity =>
        {
            entity.ToTable("PCs");
            entity.HasKey(pc => pc.Id);

            entity.Property(pc => pc.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(pc => pc.Weight)
                .IsRequired()
                .HasColumnType("float(5)");

            entity.Property(pc => pc.Warranty)
                .IsRequired();

            entity.Property(pc => pc.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(pc => pc.Stock)
                .IsRequired();
        });
    }

    private static void ConfigureComponent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.ToTable("Components");
            entity.HasKey(component => component.Code);

            entity.Property(component => component.Code)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnType("char(10)");

            entity.Property(component => component.Name)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(component => component.Description)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            entity.Property(component => component.ComponentManufacturersId)
                .IsRequired();

            entity.Property(component => component.ComponentTypesId)
                .IsRequired();

            entity.HasOne(component => component.ComponentManufacturer)
                .WithMany(manufacturer => manufacturer.Components)
                .HasForeignKey(component => component.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(component => component.ComponentType)
                .WithMany(type => type.Components)
                .HasForeignKey(component => component.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurePcComponent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.ToTable("PCComponents");
            entity.HasKey(pcComponent => new { pcComponent.PcId, pcComponent.ComponentCode });

            entity.Property(pcComponent => pcComponent.PcId)
                .HasColumnName("PCId")
                .IsRequired();

            entity.Property(pcComponent => pcComponent.ComponentCode)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnType("char(10)");

            entity.Property(pcComponent => pcComponent.Amount)
                .IsRequired();

            entity.HasOne(pcComponent => pcComponent.Pc)
                .WithMany(pc => pc.PcComponents)
                .HasForeignKey(pcComponent => pcComponent.PcId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pcComponent => pcComponent.Component)
                .WithMany(component => component.PcComponents)
                .HasForeignKey(pcComponent => pcComponent.ComponentCode)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureComponentManufacturer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.ToTable("ComponentManufacturers");
            entity.HasKey(manufacturer => manufacturer.Id);

            entity.Property(manufacturer => manufacturer.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(manufacturer => manufacturer.FullName)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(manufacturer => manufacturer.FoundationDate)
                .IsRequired()
                .HasColumnType("date");
        });
    }

    private static void ConfigureComponentType(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.ToTable("ComponentTypes");
            entity.HasKey(type => type.Id);

            entity.Property(type => type.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(type => type.Name)
                .IsRequired()
                .HasMaxLength(150);
        });
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateOnly(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "NV",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateOnly(1993, 4, 5)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "COR",
                FullName = "Corsair Gaming Inc.",
                FoundationDate = new DateOnly(1994, 1, 1)
            }
        );

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType
            {
                Id = 1,
                Abbreviation = "CPU",
                Name = "Processor"
            },
            new ComponentType
            {
                Id = 2,
                Abbreviation = "GPU",
                Name = "Graphics Card"
            },
            new ComponentType
            {
                Id = 3,
                Abbreviation = "RAM",
                Name = "Memory"
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU0000001",
                Name = "Ryzen 7 7800X3D",
                Description = "8-core gaming processor",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new Component
            {
                Code = "GPU0000001",
                Name = "RTX 4080 Super",
                Description = "High-end gaming graphics card",
                ComponentManufacturersId = 2,
                ComponentTypesId = 2
            },
            new Component
            {
                Code = "RAM0000001",
                Name = "Corsair Vengeance DDR5 16GB",
                Description = "DDR5 RAM module 16GB",
                ComponentManufacturersId = 3,
                ComponentTypesId = 3
            }
        );

        modelBuilder.Entity<Pc>().HasData(
            new Pc
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5f,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new Pc
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2f,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new Pc
            {
                Id = 3,
                Name = "Budget Student PC",
                Weight = 6.7f,
                Warranty = 12,
                CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0),
                Stock = 8
            }
        );

        modelBuilder.Entity<PcComponent>().HasData(
            new { PcId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new { PcId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new { PcId = 1, ComponentCode = "RAM0000001", Amount = 2 },
            new { PcId = 2, ComponentCode = "CPU0000001", Amount = 1 },
            new { PcId = 2, ComponentCode = "RAM0000001", Amount = 2 },
            new { PcId = 3, ComponentCode = "CPU0000001", Amount = 1 },
            new { PcId = 3, ComponentCode = "RAM0000001", Amount = 1 }
        );
    }
}
