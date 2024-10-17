


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

  internal Recipe updateRecipe(Recipe recipeData, string id)
  {
    throw new NotImplementedException();
  }
}
