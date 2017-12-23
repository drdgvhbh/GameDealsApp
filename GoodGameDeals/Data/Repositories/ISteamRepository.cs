namespace GoodGameDeals.Data.Repositories {
    using System;

    using GoodGameDeals.Data.Entity.Responses.Steam;

    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    ///     Defines a repository for retrieving data from the
    ///     <code>Steam</code> api.
    /// </summary>
    public interface ISteamRepository {
        /// <summary>
        ///     Retrieves the game logo for a game.
        /// </summary>
        /// <param name="title">
        ///     The title of the game.
        /// </param>
        /// <returns>
        ///     The game logo if found; otherwise, a placeholder image.
        /// </returns>
        IObservable<BitmapImage> GameLogo(string title);

        /// <summary>
        ///     Retrieves the list of games with their titles and their
        ///      associated Steam Ids.
        /// </summary>
        /// <returns>
        ///     The list of games with their titles and their associated
        ///      Steam Ids.
        /// </returns>
        IObservable<GetAppListResponse> AppList();
    }
}
