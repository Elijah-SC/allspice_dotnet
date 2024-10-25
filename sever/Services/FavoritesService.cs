




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

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repository.getFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception($"No Favorite with Id of {favoriteId}");
    }
    return favorite;
  }

  internal string deleteFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != userId)
    {
      throw new Exception("Thats not your Favorite to Delete, Pal");
    }

    _repository.deleteFavorite(favoriteId);

    return "Favorite was Deleted";
  }
}


