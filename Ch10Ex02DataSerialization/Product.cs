using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Column(name: "ProductName")]
    [StringLength(40)]
    public string Name { get; set; } = null!;

    [Column(name: "UnitPrice", TypeName = "money")]
    public decimal? Cost { get; set; }

    [Column(name: "UnitsInStock")]
    public int Count { get; set; }
    
    public bool Discontinued { get; set; }
}