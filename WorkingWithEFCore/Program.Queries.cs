using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WorkingWithEFCore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
partial class Program
{
    static void QueringCategories()
    {
        using (Northwind db = new())
        {
            SectionTitle("Categories and how many products they have:");

            IQueryable<Category>? categories;
            // db.Categories;
            // .Include(categories => categories.Products);

            db.ChangeTracker.LazyLoadingEnabled = false;
            Console.Write("Enable eager loading? (Y/N)");
            bool eagerLoading = Console.ReadKey(intercept: true).Key == ConsoleKey.Y;
            bool explicitLoading = false;
            Console.WriteLine();

            if (eagerLoading)
            {
                categories = db.Categories?.Include(c => c.Products);
            }
            else
            {
                categories = db.Categories;
                Console.Write("Enable explicit loading? (Y/N)");
                explicitLoading = Console.ReadKey(intercept: true).Key == ConsoleKey.Y;
                Console.WriteLine();
            }

            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found.");
                return;
            }

            foreach (Category c in categories)
            {
                if (explicitLoading)
                {
                    Console.Write($"Explicitly load products for {c.CategoryName}? (Y/N)");
                    ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                    Console.WriteLine();

                    if (key.Key == ConsoleKey.Y)
                    {
                        CollectionEntry<Category, Product> products =
                        db.Entry(c).Collection(c2 => c2.Products);

                        if (!products.IsLoaded) products.Load();
                    }
                }
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} products");
            }
        }
    }

    static void FilteredIncludes()
    {
        using (Northwind db = new())
        {
            SectionTitle("Products with a minimum number of units in stock");

            string? input;
            int stock;
            do
            {
                Console.Write("Enter a minimum for units in stock: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out stock));

            IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.Stock >= stock));

            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found");
                return;
            }
            // Info($"ToQueryString: {categories.ToQueryString()}");
            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock");

                foreach (Product p in c.Products)
                {
                    Console.WriteLine($"\t{p.ProductName} has {p.Stock} units in stock");
                }
                Console.WriteLine(new string('-', 50));
            }
        }
    }

    static void QueryingProducts()
    {
        using (Northwind db = new())
        {
            SectionTitle("Products that cost more than a price, highest at top");

            string? input;
            decimal price;

            do
            {
                Console.Write("Enter a product price: ");
                input = Console.ReadLine();
            } while (!decimal.TryParse(input, out price));

            IQueryable<Product>? products = db.Products?.TagWith("Products filtered by price and sorted").Where(product => product.Cost > price).OrderByDescending(product => product.Cost);

            if ((products is null) || (!products.Any()))
            {
                Fail("No products found.");
                return;
            }
            Info($"ToQueryString: {products.ToQueryString()}");
            foreach (Product p in products)
            {
                Console.WriteLine($"{p.ProductId}: {p.ProductName} costs {p.Cost:$#,##0.00} and has {p.Stock} in Stock");
            }
        }
    }
    static void QueryingWithLike()
    {
        using (Northwind db = new())
        {
            SectionTitle("Pattern matchong with LIKE");
            Console.Write("Enter a part of a product name: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Fail("You did not enter part of a product name.");
                return;
            }

            IQueryable<Product>? products = db.Products?.Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

            if ((products is null) || (!products.Any()))
            {
                Fail("No products found");
                return;
            }

            foreach (Product p in products)
            {
                Console.WriteLine($"{p.ProductName} has {p.Stock}. Discounted? {p.Discontinued}");
            }

        }
    }

    static void GetRandomProduct()
    {
        using (Northwind db = new())
        {
            SectionTitle("Get a random product. ");

            int? rowCount = db.Products?.Count();

            if (rowCount == null)
            {
                Fail("Products table is empty");
                return;
            }
            Product? p = db.Products?.FirstOrDefault(p => p.ProductId == (int)(EF.Functions.Random() * rowCount));

            if (p == null)
            {
                Fail("Product not found");
                return;
            }
            Console.WriteLine($"Random product: {p.ProductId} {p.ProductName}");
        }
    }
}
