

namespace allspice_dotnet.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe createRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO
    recipes(title, subtitle, instructions, img, category, creatorId)
    values(@Title, @SubTitle, @Instructions, @Img, @Category, @creatorId);
    
    Select
    recipes.*,
    accounts.*
    From recipes
    JOIN accounts ON recipes.creatorId = accounts.id
    WHERE recipes.id = LAST_INSERT_ID();";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreatorToRecipe, recipeData).FirstOrDefault();
    return recipe;
  }

  internal List<Recipe> getRecipes()
  {
    string sql = @"
    Select 
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON recipes.creatorId = accounts.id;";
    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreatorToRecipe).ToList();
    return recipes;
  }

  private Recipe JoinCreatorToRecipe(Recipe recipe, Profile profile)
  {
    recipe.Creator = profile;
    return recipe;
  }
}

