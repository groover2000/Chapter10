

using Microsoft.EntityFrameworkCore;

public class Northwind : DbContext
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    // DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Northwind.db");
        string connection = $"Filename={path}";

        optionsBuilder.UseSqlite(connection);
    }
}