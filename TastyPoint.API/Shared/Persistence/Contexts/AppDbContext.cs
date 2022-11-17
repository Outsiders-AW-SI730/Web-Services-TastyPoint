using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Shared.Extensions;

namespace TastyPoint.API.Shared.Persistence.Contexts;

public class AppDbContext: DbContext
{
    public DbSet<Pack> Packs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<BusinessPlan> BusinessPlans { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Product Entity Mapping Configuration
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Product>().Property(p => p.Type).IsRequired().HasMaxLength(50);

        //Pack Entity Mapping Configuration
        builder.Entity<Pack>().ToTable("Packs");
        builder.Entity<Pack>().HasKey(p => p.Id);
        builder.Entity<Pack>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Pack>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        
        //Pack BusinessPlans Mapping Configuration
        builder.Entity<BusinessPlan>().ToTable("BusinessPlans");
        builder.Entity<BusinessPlan>().HasKey(p => p.Id);
        builder.Entity<BusinessPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<BusinessPlan>().Property(p => p.CurrentPlan).IsRequired().HasMaxLength(100);
        builder.Entity<BusinessPlan>().Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Entity<BusinessPlan>().Property(p => p.PlanPrice).IsRequired();

        //Relationships
        builder.Entity<Pack>()
            .HasMany(p => p.Products)
            .WithOne(p => p.Pack)
            .HasForeignKey(p => p.PackId);

        builder.UseSnakeCaseNamingConvention();
    }
    
}