

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

  internal List<Ingredient> getRecipeIngredients(int recipeId)
  {
    string sql = @"
    Select * FROM ingredients 
    WHERE ingredients.recipeId = @recipeId;";

    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }
}
