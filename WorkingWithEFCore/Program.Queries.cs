using Microsoft.EntityFrameworkCore;

using WorkingWithEFCore.Models;
partial class Program
{
    static void QueringCategories()
    {
        using (Northwind db = new())
        {
            SectionTitle("Categories and how many products they have:");

            IQueryable<Category>? categories = db.Categories?.Include(categories => categories.Products);

            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found.");
                return;
            }

            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} products");
            }
        }
    }
}
