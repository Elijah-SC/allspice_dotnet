


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

  // internal Recipe getRecipeById(int recipeId)
  // {

  // }

  internal List<Recipe> getRecipes()
  {
    List<Recipe> recipes = _repository.getRecipes();
    return recipes;
  }
}
