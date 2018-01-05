/*
namespace GoodGameDeals.Data.Cache {
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage;

    using Microsoft.Toolkit.Uwp.UI;

    public abstract class AbstractCache<T> {
        private HttpClient httpClient;

        private StorageFolder rootFolder;

        private string rootFolderName;

        private StorageFolder cacheFolder;

        private readonly SemaphoreSlim cacheFolderSemaphore;

        private InMemoryStorage<T> inMemoryFileStorage;

        public TimeSpan CacheDuration { get; set; }



        protected AbstractCache(HttpClient client) {
            this.httpClient = client;
            this.CacheDuration = TimeSpan.FromDays(1);
            this.inMemoryFileStorage = new InMemoryStorage<T>();
            this.cacheFolderSemaphore = new SemaphoreSlim(1);
        }

        public virtual async Task InitializeAsync(
                StorageFolder folder = null,
                string folderName = null) {
            this.rootFolder = folder;
            this.rootFolderName = folderName;
            this.cacheFolder = await this.GetCacheFolderAsync().ConfigureAwait(false);
        }

        private async Task ForceInitializeAsync() {
            if (this.cacheFolder != null) {
                return;
            }

            await this.cacheFolderSemaphore.WaitAsync().ConfigureAwait(false);

            this.inMemoryFileStorage = new InMemoryStorage<T>();

            if (this.rootFolder == null) {

            }


        }

        private async Task<StorageFolder> GetCacheFolderAsync()
        {
            if (this.cacheFolder != null) {
                await this.ForceInitializeAsync();
            }

            return this.cacheFolder;
        }


    }
}
*/
