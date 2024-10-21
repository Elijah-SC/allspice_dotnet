using System.Net.Http;

namespace allspice_dotnet.Controllers;

[ApiController, Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
    _ingredientsService = ingredientsService;
  }
  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0Provider;
  private readonly IngredientsService _ingredientsService;


  [Authorize, HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.createRecipe(recipeData);
      return Ok(recipe);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [HttpGet]
  public ActionResult<List<Recipe>> getRecipes()
  {
    try
    {
      List<Recipe> recipes = _recipesService.getRecipes();
      return Ok(recipes);

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> getRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.getRecipeById(recipeId);
      return recipe;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpPut("{recipeId}"), Authorize]
  public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipeData, int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.updateRecipe(recipeData, userInfo.Id, recipeId);
      return recipe;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{recipeId}"), Authorize]
  public async Task<ActionResult<string>> deleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string Message = _recipesService.deleteRecipe(recipeId, userInfo.Id);
      return Message;

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> getRecipeIngredients(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.getRecipeIngredients(recipeId);
      return ingredients;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
