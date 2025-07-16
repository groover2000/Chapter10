using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
// using Microsoft.EntityFrameworkCore.Sqlite;

namespace WorkingWithEFCore.Models;

public class Northwind : DbContext
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Northwind.db");
        string connection = $"Filename={path}";


        optionsBuilder.LogTo(Console.WriteLine, [RelationalEventId.CommandExecuting]).EnableSensitiveDataLogging();


        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Connection: {connection}");
        Console.ForegroundColor = previousColor;

        optionsBuilder.UseSqlite(connection);
        optionsBuilder.UseLazyLoadingProxies();

        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
        .Property(category => category.CategoryName)
        .IsRequired()
        .HasMaxLength(15);


        if (Database.ProviderName?.Contains("Sqlite") ?? false)
        {
            modelBuilder.Entity<Product>()
            .Property(product => product.Cost)
            .HasConversion<double>();

            // modelBuilder.Entity<Product>()
            // .HasQueryFilter(p => !p.Discontinued);

            
        }

    }
}