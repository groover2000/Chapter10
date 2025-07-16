using WorkingWithEFCore.Models;

// Northwind db = new();
// Console.WriteLine($"Provider {db.Database.ProviderName}");

// QueringCategories();
// FilteredIncludes();
// QueryingProducts();
// QueryingWithLike();
// GetRandomProduct();

// var resultAdd = AddProduct(6, "Bobs Burgers3", 500);

// if (resultAdd.affected == 1)
// {
//     Console.WriteLine($"Add product successful with ID: {resultAdd.productId}");
// }

// ListProducts([resultAdd.productId]);

// var resultUpdate = IncreaseProductPrice("Bob", 20M);

// if (resultUpdate.affected == 1)
// {
//     Console.WriteLine($"Increase price success for ID: {resultUpdate.ProductId}");
// }

// ListProducts([resultUpdate.ProductId]);
// Console.WriteLine("About to delete all products whose name starts with Bob:");
// Console.Write("Press enter to continue or any other key to exit:");

if (Console.ReadKey(intercept: true).Key == ConsoleKey.Enter)
{
    int deleted = DeleteProducts("Bob");
    Console.WriteLine($"{deleted} products were deleted");
}
else
{
    Console.WriteLine("Delete was canceled");
}

// var resultUpdateBetter = IncreaseProductPriceBetter("Bob", 20);

// if (resultUpdateBetter.affected > 0)
// {
//     Console.WriteLine("Increase Product Price product price successful");
// }

// ListProducts(resultUpdateBetter.productIds);

// Console.WriteLine("About to delete all products whose name starts with Bob");
// Console.Write("Press Enter to continue or any other key to exit: ");
// if (Console.ReadKey(intercept: true).Key == ConsoleKey.Enter)
// {
//     int deleted = DeleteProductsBetter("Bob");
//     Console.WriteLine($"{deleted} products were deleted");
// }
// else
// {
//     Console.WriteLine("Delete was canceled");
// }

