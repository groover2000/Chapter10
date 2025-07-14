using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkingWithEFCore.AutoGenModels;



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
                Console.WriteLine($"| {p.ProductId:000} | {p.ProductName,-35} | {p.UnitPrice,8:$#, ##0.00} | {p.UnitsInStock,5} | {p.Discontinued}");
                Console.ForegroundColor = previousColor;

            }
        }
    }
    static (int affected, int productId) AddProduct(
        int categoryId, string productName, double? price)
    {
        using (Northwind db = new())
        {
            if (db.Products is null) return (0, 0);

            Product p = new()
            {
                CategoryId = categoryId,
                ProductName = productName,
                UnitPrice = price,
                UnitsInStock = 72
            };
        }
        return (1, 2);
    }

}