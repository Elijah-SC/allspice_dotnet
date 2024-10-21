

namespace allspice_dotnet.Services;

public class IngredientsService
{

  public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
  {
    _repository = repository;
    _recipesService = recipesService;
  }
  private readonly IngredientsRepository _repository;
  private readonly RecipesService _recipesService;


  internal Ingredient AddIngredient(Ingredient ingredientData, string userId)
  {
    Recipe recipeToAddIngredients = _recipesService.getRecipeById(ingredientData.RecipeId);
    if (recipeToAddIngredients.CreatorId != userId)
    {
      throw new Exception("You can't Add Ingredients to Someone else's Recipe, Lil Chef");
    }
    Ingredient ingredient = _repository.AddIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> getRecipeIngredients(int recipeId)
  {
    List<Ingredient> ingredients = _repository.getRecipeIngredients(recipeId);
    return ingredients;
  }
}
