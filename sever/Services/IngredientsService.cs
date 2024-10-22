

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

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.getIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception($"Invalid Id {ingredientId}");
    }
    return ingredient;
  }

  internal string deleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    Recipe recipe = _recipesService.getRecipeById(ingredient.RecipeId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("You can't delete ingredients from another users Recipe, buddy");
    }

    _repository.DeleteIngredient(ingredientId);

    return "Ingredient Deleted";
  }
}


