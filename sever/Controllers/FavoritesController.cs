namespace allspice_dotnet.Controllers;

[ApiController, Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth0Provider;

  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize, HttpPost]
  public async Task<ActionResult<FavoriteRecipe>> createFavorite([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      FavoriteRecipe favorite = _favoritesService.createFavorite(favoriteData);
      return Ok(favorite);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize, HttpDelete("{favoriteId}")]
  public async Task<ActionResult<string>> deleteFavorite(int favoriteId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string Message = _favoritesService.deleteFavorite(favoriteId, userInfo.Id);
      return Ok(Message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}