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
      return Ok(ingredient);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize, HttpDelete("{ingredientId}")]
  public async Task<ActionResult<string>> deleteIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      string Message = _ingredientsService.deleteIngredient(ingredientId, userInfo.Id);
      return Ok(Message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}

