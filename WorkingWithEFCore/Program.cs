using WorkingWithEFCore.Models;

// Northwind db = new();
// Console.WriteLine($"Provider {db.Database.ProviderName}");

// QueringCategories();
// FilteredIncludes();
// QueryingProducts();
// QueryingWithLike();
// GetRandomProduct();

var resultAdd = AddProduct(6, "Bobs Burgers3", 500);

if (resultAdd.affected == 1)
{
    Console.WriteLine($"Add product successful with ID: {resultAdd.productId}");
}

ListProducts([resultAdd.productId]);