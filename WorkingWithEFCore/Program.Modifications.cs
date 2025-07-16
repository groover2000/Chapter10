using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using WorkingWithEFCore.Models;



partial class Program
{
    static void ListProducts(int[]? productIdsToHighlight = null)
    {
        using (Northwind db = new())
        {
            if ((db.Products is null) || (!db.Products.Any()))
            {
                Fail("There are no products");
                return;
            }


            Console.WriteLine($"| {"Id",-3} | {"Product Name",-35} | {"Cost",8} | {"Stock",5} | {"Disc"} |");

            foreach (Product p in db.Products)
            {
                ConsoleColor previousColor = Console.ForegroundColor;

                if ((productIdsToHighlight is not null) && productIdsToHighlight.Contains(p.ProductId))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"| {p.ProductId:000} | {p.ProductName,-35} | {p.Cost,8:$#, ##0.00} | {p.Stock,5} | {p.Discontinued}");
                Console.ForegroundColor = previousColor;

            }
        }
    }


    static (int affected, int productId) AddProduct(
        int categoryId, string productName, decimal? price)
    {
        using (Northwind db = new())
        {
            if (db.Products is null) return (0, 0);

            Product p = new()
            {
                CategoryId = categoryId,
                ProductName = productName,
                Cost = price,
                Stock = 72
            };

            EntityEntry<Product> entity = db.Products.Add(p);
            Console.WriteLine($"State: {entity.State}, ProductId: {p.ProductId}");

            int affected = db.SaveChanges();
            Console.WriteLine($"State {entity.State} ProductId: {p.ProductId}");

            return (affected, p.ProductId);
        }
    }

    static (int affected, int ProductId) IncreaseProductPrice(
        string productNameStartsWith, decimal amount)
    {
        using (Northwind db = new())
        {

            if (db.Products is null) return (0, 0);
            Product updateProduct = db.Products.First(p => p.ProductName.StartsWith(productNameStartsWith));

            updateProduct.Cost += amount;

            int affected = db.SaveChanges();

            return (affected, updateProduct.ProductId);
        }
    }

    static int DeleteProducts(string productNameStartsWith)
    {
        using (Northwind db = new())
        {
            using (IDbContextTransaction t = db.Database.BeginTransaction())
            {
                Console.WriteLine($"Transaction isolation level: {t.GetDbTransaction().IsolationLevel}");

                IQueryable<Product>? products = db.Products?.Where(p => p.ProductName.StartsWith(productNameStartsWith));

                if (products is null || !products.Any())
                {
                    Console.WriteLine("No products found to delete: ");
                    return 0;
                }
                else
                {
                    if (db.Products is null) return 0;
                    db.Products.RemoveRange(products);
                }
                int affected = db.SaveChanges();
                t.Commit();
                return affected;
            }
        }
    }

    static (int affected, int[]? productIds) IncreaseProductPriceBetter(
        string productNameStartsWith, decimal amount
    )
    {
        using (Northwind db = new())
        {
            if (db.Products is null) return (0, null);

            IQueryable<Product>? products = db.Products.Where(p => p.ProductName.StartsWith(productNameStartsWith));

            int affected = products.ExecuteUpdate(s => s.SetProperty(
                p => p.Cost,
                p => p.Cost + amount));

            int[] productIds = products.Select(p => p.ProductId).ToArray();
            return (affected, productIds);
        }
    }

    static int DeleteProductsBetter(string productNameStartsWith)
    {
        using (Northwind db = new())
        {
           
            int affected = 0;

            IQueryable<Product>? products = db.Products?.Where(p => p.ProductName.StartsWith(productNameStartsWith));

            if (products is null || !products.Any())
            {
                Console.WriteLine("No products found to delete");
                return 0;
            }
            else
            {
                affected = products.ExecuteDelete();
            }
            return affected;
        }
    }

}