namespace allspice_dotnet.Controllers;

[ApiController, Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0provider)
  {
    _ingredientsService = ingredientsService;
    _auth0provider = auth0provider;
  }

  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0provider;


  [Authorize, HttpPost]
  public async Task<ActionResult<Ingredient>> AddIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.AddIngredient(ingredientData, userInfo.Id);
      return ingredient;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
