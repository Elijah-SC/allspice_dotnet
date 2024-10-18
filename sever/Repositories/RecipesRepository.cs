



using System.Diagnostics;
using System.Security.Cryptography;

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

  internal void deleteRecipe(int recipeId)
  {
    string sql = @"DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { recipeId });

    switch (rowsAffected)
    {
      case 0:
        throw new Exception("No Recipes were Deleted, FAILED");
      case 1:
        break;
      default:
        throw new Exception($"{rowsAffected} were deleted, THATS NOT GOOD");
    }
  }
  internal Recipe getRecipeById(int recipeId)
  {
    string sql = @"
  SELECT
   recipes.*,
   accounts.*
   FROM recipes
   JOIN accounts on recipes.creatorId = accounts.id
   Where recipes.id = @recipeId;";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreatorToRecipe, new { recipeId }).FirstOrDefault();
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

  internal void updateRecipe(Recipe recipe)
  {
    string sql = @"
    Update recipes 
    SET 
    title = @Title,
    subtitle = @Subtitle,
    instructions = @Instructions,
    img = @Img,
    category = @category
    WHERE id = @Id
    LIMIT 1;";

    int rowsAffected = _db.Execute(sql, recipe);

    switch (rowsAffected)
    {
      case 0:
        throw new Exception("No Recipes were Updated, FAILED");
      case 1:
        break;
      default:
        throw new Exception($"{rowsAffected} were deleted, THATS NOT GOOD");
    }
  }

  private Recipe JoinCreatorToRecipe(Recipe recipe, Profile profile)
  {
    recipe.Creator = profile;
    return recipe;
  }
}

