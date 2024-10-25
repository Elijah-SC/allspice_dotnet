







namespace allspice_dotnet.Repositories;

public class FavoritesRepository
{
  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal FavoriteRecipe createFavorite(Favorite favoriteData)
  {
    string sql = @"
      INSERT INTO 
      favorites(recipeId, accountId) 
      values(@RecipeId, @AccountId);
      
      SELECT 
      favorites.*,
      recipes.*,
      accounts.*
      FROM favorites 
      JOIN recipes ON recipes.id = favorites.recipeId
      JOIN accounts ON accounts.id = favorites.accountId
      WHERE favorites.id = LAST_INSERT_ID();";

    FavoriteRecipe favorite = _db.Query<Favorite, FavoriteRecipe, FavoriteProfile, FavoriteRecipe>(sql, (favorite, recipe, profile) =>
    {
      recipe.FavoriteId = favorite.Id;
      recipe.AccountId = favorite.AccountId;
      recipe.RecipeId = favorite.RecipeId;
      recipe.Creator = profile;
      return recipe;
    }, favoriteData).FirstOrDefault();
    return favorite;
  }

  internal List<FavoriteRecipe> getAccountFavorites(string userId)
  {
    string sql = @"
    SELECT 
      favorites.*,
      recipes.*,
      accounts.*
      FROM favorites 
      JOIN recipes ON recipes.id = favorites.recipeId
      JOIN accounts ON accounts.id = favorites.accountId
      WHERE favorites.accountId = @userId;";

    List<FavoriteRecipe> favorites = _db.Query<Favorite, FavoriteRecipe, FavoriteProfile, FavoriteRecipe>(sql, (favorite, recipe, profile) =>
  {
    recipe.FavoriteId = favorite.Id;
    recipe.AccountId = favorite.AccountId;
    recipe.RecipeId = favorite.RecipeId;
    recipe.Creator = profile;
    return recipe;
  }, new { userId }).ToList();
    return favorites;
  }

  internal Favorite getFavoriteById(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";

    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal void deleteFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId;";

    int RowsAffected = _db.Execute(sql, new { favoriteId });

    switch (RowsAffected)
    {
      case 0:
        throw new Exception("Not Favorites were Deleted");
      case 1:
        break;
      default:
        throw new Exception($"{RowsAffected} where deleted, thats not good");
    }
  }
}
