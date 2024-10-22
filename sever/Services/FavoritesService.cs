



namespace allspice_dotnet.Services;

public class FavoritesService
{
  public FavoritesService(FavoritesRepository repository)
  {
    _repository = repository;
  }
  private readonly FavoritesRepository _repository;


  internal FavoriteRecipe createFavorite(Favorite favoriteData)
  {
    FavoriteRecipe favorite = _repository.createFavorite(favoriteData);
    return favorite;
  }

  internal List<FavoriteRecipe> getAccountFavorites(string userId)
  {
    List<FavoriteRecipe> favoriteRecipes = _repository.getAccountFavorites(userId);
    return favoriteRecipes;
  }
}


