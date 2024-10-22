



namespace allspice_dotnet.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient AddIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO 
    ingredients(name, quantity, recipeId)
    values(@Name, @Quantity, @recipeId);
    
   SELECT * FROM
    ingredients
    WHERE id = LAST_INSERT_ID();";

    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId)
  {
    string sql = "DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { ingredientId });

    switch (rowsAffected)
    {
      case 0:
        throw new Exception("No ingredients were deleted");
      case 1:
        break;
      default:
        throw new Exception($"{rowsAffected} were deleted thats not good");
    }
  }

  internal Ingredient getIngredientById(int ingredientId)
  {
    string sql = @"Select * FROM ingredients
      WHERE id = @ingredientId;";
    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }

  internal List<Ingredient> getRecipeIngredients(int recipeId)
  {
    string sql = @"
    Select * FROM ingredients 
    WHERE ingredients.recipeId = @recipeId;";

    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }
}
