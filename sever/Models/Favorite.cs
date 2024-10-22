namespace allspice_dotnet.Models;

public class Favorite
{
  public int Id { get; set; }
  public int RecipeId { get; set; }
  public string AccountId { get; set; }
}


public class FavoriteProfile : Profile
{
  public int RecipeId { get; set; }
  public string AccountId { get; set; }
}

public class FavoriteRecipe : Recipe
{
  public int FavoriteId { get; set; }
  public int RecipeId { get; set; }
  public string AccountId { get; set; }
}