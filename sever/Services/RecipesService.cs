


using System.Security.Cryptography;

namespace allspice_dotnet.Services;

public class RecipesService
{
  private readonly RecipesRepository _repository;

  public RecipesService(RecipesRepository repository)
  {
    _repository = repository;
  }

  internal Recipe createRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.createRecipe(recipeData);
    return recipe;
  }

  internal string deleteRecipe(int recipeId, string userId)
  {
    Recipe recipeToDelete = getRecipeById(recipeId);
    if (recipeToDelete.CreatorId != userId)
    {
      throw new Exception("That ain't your Recipe to Delete, You Slimy Postman USER, or I wrote my front end poorly in that case I apologize");
    }
    _repository.deleteRecipe(recipeId);
    return "Recipe Deleted";
  }

  internal Recipe getRecipeById(int recipeId)
  {
    Recipe recipe = _repository.getRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("No recipe with that ID");
    }
    return recipe;
  }


  internal List<Recipe> getRecipes()
  {
    List<Recipe> recipes = _repository.getRecipes();
    return recipes;
  }

  internal Recipe updateRecipe(Recipe recipeData, string UserId, int recipeId)
  {
    Recipe Recipe = getRecipeById(recipeId);
    if (Recipe.CreatorId != UserId)
    {
      throw new Exception("Thats not your Recipe to Update, Lil Bro");
    }

    Recipe.Title = recipeData.Title ?? Recipe.Title;
    Recipe.SubTitle = recipeData.SubTitle ?? Recipe.SubTitle;
    Recipe.Instructions = recipeData.Instructions ?? Recipe.Instructions;
    Recipe.Img = recipeData.Img ?? Recipe.Img;
    Recipe.Category = recipeData.Category ?? Recipe.Category;

    _repository.updateRecipe(Recipe);
    return Recipe;
  }
}
