namespace allspice_dotnet.Controllers;

[ApiController, Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
  }
  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0Provider;


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
    List<Recipe> recipes = _recipesService.getRecipes();
    return Ok(recipes);
  }
  //   [HttpGet("{recipeId}")]
  //   public ActionResult<Recipe> getRecipeById(int recipeId)
  //   {
  //     Recipe recipe = _recipesService.getRecipeById(recipeId);
  //     return recipe;
  //   }
}
