using System.ComponentModel.DataAnnotations;

namespace allspice_dotnet.Models;

public class Ingredient
{
  public int Id { get; set; }
  [MinLength(1), MaxLength(100)] public string Name { get; set; }
  [MinLength(1), MaxLength(100)] public string quantity { get; set; }
  public int RecipeId { get; set; }
}