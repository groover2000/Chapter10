
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "NTEXT")]

    public string? Description { get; set; }
}