// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace GoodGameDeals.Data.Cache
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.Web.Http;

    using Microsoft.Toolkit.Uwp.UI;



    /// <summary>
    /// Provides methods and tools to cache files in a folder
    /// </summary>
    /// <typeparam name="T">Generic type as supplied by consumer of the class</typeparam>
    public abstract class CacheBase<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether context should be maintained until type has been instantiated or not.
        /// </summary>
        protected bool MaintainContext { get; set; }

        private class ConcurrentRequest
        {
            public Task<T> Task { get; set; }

            public bool EnsureCachedCopy { get; set; }
        }

        private readonly SemaphoreSlim _cacheFolderSemaphore = new SemaphoreSlim(1);
        private StorageFolder _baseFolder = null;
        private string _cacheFolderName = null;

        private StorageFolder _cacheFolder = null;
        private InMemoryStorage<T> _inMemoryFileStorage = null;

        private ConcurrentDictionary<string, ConcurrentRequest> _concurrentTasks = new ConcurrentDictionary<string, ConcurrentRequest>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheBase{T}"/> class.
        /// </summary>
        protected CacheBase(HttpClient httpClient)
        {
            this.CacheDuration = TimeSpan.FromDays(1);
            this._inMemoryFileStorage = new InMemoryStorage<T>();
            this.RetryCount = 1;
            this.HttpClient = httpClient;
        }

        /// <summary>
        /// Gets or sets the life duration of every cache entry.
        /// </summary>
        public TimeSpan CacheDuration { get; set; }

        /// <summary>
        /// Gets or sets the number of retries trying to ensure the file is cached.
        /// </summary>
        public uint RetryCount { get; set; }

        /// <summary>
        /// Gets or sets max in-memory item storage count
        /// </summary>
        public int MaxMemoryCacheCount
        {
            get
            {
                return this._inMemoryFileStorage.MaxItemCount;
            }

            set
            {
                this._inMemoryFileStorage.MaxItemCount = value;
            }
        }

        /// <summary>
        /// Gets instance of <see cref="HttpClient"/>
        /// </summary>
        protected HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Initializes FileCache and provides root folder and cache folder name
        /// </summary>
        /// <param name="folder">Folder that is used as root for cache</param>
        /// <param name="folderName">Cache folder name</param>
        /// <param name="httpMessageHandler">instance of <see cref="HttpMessageHandler"/></param>
        /// <returns>awaitable task</returns>
        public virtual async Task InitializeAsync(StorageFolder folder = null, string folderName = null)
        {
            this._baseFolder = folder;
            this._cacheFolderName = folderName;

            this._cacheFolder = await this.GetCacheFolderAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Clears all files in the cache
        /// </summary>
        /// <returns>awaitable task</returns>
        public async Task ClearAsync()
        {
            var folder = await this.GetCacheFolderAsync().ConfigureAwait(false);
            var files = await folder.GetFilesAsync().AsTask().ConfigureAwait(false);

            await this.InternalClearAsync(files).ConfigureAwait(false);

            this._inMemoryFileStorage.Clear();
        }

        /// <summary>
        /// Clears file if it has expired
        /// </summary>
        /// <param name="duration">timespan to compute whether file has expired or not</param>
        /// <returns>awaitable task</returns>
        public Task ClearAsync(TimeSpan duration)
        {
            return this.RemoveExpiredAsync(duration);
        }

        /// <summary>
        /// Removes cached files that have expired
        /// </summary>
        /// <param name="duration">Optional timespan to compute whether file has expired or not. If no value is supplied, <see cref="CacheDuration"/> is used.</param>
        /// <returns>awaitable task</returns>
        public async Task RemoveExpiredAsync(TimeSpan? duration = null)
        {
            TimeSpan expiryDuration = duration.HasValue ? duration.Value : this.CacheDuration;

            var folder = await this.GetCacheFolderAsync().ConfigureAwait(false);
            var files = await folder.GetFilesAsync().AsTask().ConfigureAwait(false);

            var filesToDelete = new List<StorageFile>();

            foreach (var file in files)
            {
                if (file == null)
                {
                    continue;
                }

                if (await this.IsFileOutOfDateAsync(file, expiryDuration, false).ConfigureAwait(false))
                {
                    filesToDelete.Add(file);
                }
            }

            await this.InternalClearAsync(filesToDelete).ConfigureAwait(false);

            this._inMemoryFileStorage.Clear(expiryDuration);
        }

        /// <summary>
        /// Removed items based on uri list passed
        /// </summary>
        /// <param name="uriForCachedItems">Enumerable uri list</param>
        /// <returns>awaitable Task</returns>
        public async Task RemoveAsync(IEnumerable<Uri> uriForCachedItems)
        {
            if (uriForCachedItems == null || !uriForCachedItems.Any())
            {
                return;
            }

            var folder = await this.GetCacheFolderAsync().ConfigureAwait(false);
            var files = await folder.GetFilesAsync().AsTask().ConfigureAwait(false);

            var filesToDelete = new List<StorageFile>();
            var keys = new List<string>();

            Dictionary<string, StorageFile> hashDictionary = new Dictionary<string, StorageFile>();

            foreach (var file in files)
            {
                hashDictionary.Add(file.Name, file);
            }

            foreach (var uri in uriForCachedItems)
            {
                string fileName = GetCacheFileName(uri);

                StorageFile file = null;

                if (hashDictionary.TryGetValue(fileName, out file))
                {
                    filesToDelete.Add(file);
                    keys.Add(fileName);
                }
            }

            await this.InternalClearAsync(filesToDelete).ConfigureAwait(false);

            this._inMemoryFileStorage.Remove(keys);
        }

        /// <summary>
        /// Assures that item represented by Uri is cached.
        /// </summary>
        /// <param name="uri">Uri of the item</param>
        /// <param name="throwOnError">Indicates whether or not exception should be thrown if item cannot be cached</param>
        /// <param name="storeToMemoryCache">Indicates if item should be loaded into the in-memory storage</param>
        /// <param name="cancellationToken">instance of <see cref="CancellationToken"/></param>
        /// <returns>Awaitable Task</returns>
        public Task<T> PreCacheAsync(Uri uri, bool throwOnError = false, bool storeToMemoryCache = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetItemAsync(uri, throwOnError, !storeToMemoryCache, cancellationToken, null);
        }

        /// <summary>
        /// Retrieves item represented by Uri from the cache. If the item is not found in the cache, it will try to downloaded and saved before returning it to the caller.
        /// </summary>
        /// <param name="uri">Uri of the item.</param>
        /// <param name="throwOnError">Indicates whether or not exception should be thrown if item cannot be found / downloaded.</param>
        /// <param name="cancellationToken">instance of <see cref="CancellationToken"/></param>
        /// <param name="initializerKeyValues">key value pairs used when initializing instance of generic type</param>
        /// <returns>an instance of Generic type</returns>
        public Task<T> GetFromCacheAsync(Uri uri, bool throwOnError = false, CancellationToken cancellationToken = default(CancellationToken), List<KeyValuePair<string, object>> initializerKeyValues = null)
        {
            return this.GetItemAsync(uri, throwOnError, false, cancellationToken, initializerKeyValues);
        }

        /// <summary>
        /// Gets the StorageFile containing cached item for given Uri
        /// </summary>
        /// <param name="uri">Uri of the item.</param>
        /// <returns>a StorageFile</returns>
        public async Task<StorageFile> GetFileFromCacheAsync(Uri uri)
        {
            var folder = await this.GetCacheFolderAsync().ConfigureAwait(false);

            string fileName = GetCacheFileName(uri);

            var item = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(false);

            return item as StorageFile;
        }

        /// <summary>
        /// Retrieves item represented by Uri from the in-memory cache if it exists and is not out of date. If item is not found or is out of date, default instance of the generic type is returned.
        /// </summary>
        /// <param name="uri">Uri of the item.</param>
        /// <returns>an instance of Generic type</returns>
        public T GetFromMemoryCache(Uri uri)
        {
            T instance = default(T);

            string fileName = GetCacheFileName(uri);

            if (this._inMemoryFileStorage.MaxItemCount > 0)
            {
                var msi = this._inMemoryFileStorage.GetItem(fileName, this.CacheDuration);
                if (msi != null)
                {
                    instance = msi.Item;
                }
            }

            return instance;
        }

        /// <summary>
        /// Cache specific hooks to process items from HTTP response
        /// </summary>
        /// <param name="stream">input stream</param>
        /// <param name="initializerKeyValues">key value pairs used when initializing instance of generic type</param>
        /// <returns>awaitable task</returns>
        protected abstract Task<T> InitializeTypeAsync(Stream stream, List<KeyValuePair<string, object>> initializerKeyValues = null);

        /// <summary>
        /// Cache specific hooks to process items from HTTP response
        /// </summary>
        /// <param name="baseFile">storage file</param>
        /// <param name="initializerKeyValues">key value pairs used when initializing instance of generic type</param>
        /// <returns>awaitable task</returns>
        protected abstract Task<T> InitializeTypeAsync(StorageFile baseFile, List<KeyValuePair<string, object>> initializerKeyValues = null);

        /// <summary>
        /// Override-able method that checks whether file is valid or not.
        /// </summary>
        /// <param name="file">storage file</param>
        /// <param name="duration">cache duration</param>
        /// <param name="treatNullFileAsOutOfDate">option to mark uninitialized file as expired</param>
        /// <returns>bool indicate whether file has expired or not</returns>
        protected virtual async Task<bool> IsFileOutOfDateAsync(StorageFile file, TimeSpan duration, bool treatNullFileAsOutOfDate = true)
        {
            if (file == null)
            {
                return treatNullFileAsOutOfDate;
            }

            var properties = await file.GetBasicPropertiesAsync().AsTask().ConfigureAwait(false);

            return properties.Size == 0 || DateTime.Now.Subtract(properties.DateModified.DateTime) > duration;
        }

        private static string GetCacheFileName(Uri uri)
        {
            return CreateHash64(uri.ToString()).ToString();
        }

        private static ulong CreateHash64(string str)
        {
            byte[] utf8 = System.Text.Encoding.UTF8.GetBytes(str);

            ulong value = (ulong)utf8.Length;
            for (int n = 0; n < utf8.Length; n++)
            {
                value += (ulong)utf8[n] << ((n * 5) % 56);
            }

            return value;
        }

        private async Task<T> GetItemAsync(Uri uri, bool throwOnError, bool preCacheOnly, CancellationToken cancellationToken, List<KeyValuePair<string, object>> initializerKeyValues)
        {
            T instance = default(T);

            string fileName = GetCacheFileName(uri);

            ConcurrentRequest request = null;

            this._concurrentTasks.TryGetValue(fileName, out request);

            // if similar request exists check if it was preCacheOnly and validate that current request isn't preCacheOnly
            if (request != null && request.EnsureCachedCopy && !preCacheOnly)
            {
                await request.Task.ConfigureAwait(this.MaintainContext);
                request = null;
            }

            if (request == null)
            {
                request = new ConcurrentRequest()
                {
                    Task = this.GetFromCacheOrDownloadAsync(uri, fileName, preCacheOnly, cancellationToken, initializerKeyValues),
                    EnsureCachedCopy = preCacheOnly
                };

                this._concurrentTasks[fileName] = request;
            }

            try {
                instance =
                    await request.Task.ConfigureAwait(this.MaintainContext);
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                if (throwOnError) {
                    throw;
                }
            }
            finally {
                this._concurrentTasks.TryRemove(fileName, out request);
                request = null;
            }

            return instance;
        }

        private async Task<T> GetFromCacheOrDownloadAsync(Uri uri, string fileName, bool preCacheOnly, CancellationToken cancellationToken, List<KeyValuePair<string, object>> initializerKeyValues)
        {
            StorageFile baseFile = null;
            T instance = default(T);

            if (this._inMemoryFileStorage.MaxItemCount > 0)
            {
                var msi = this._inMemoryFileStorage.GetItem(fileName, this.CacheDuration);
                if (msi != null)
                {
                    instance = msi.Item;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            var folder = await this.GetCacheFolderAsync().ConfigureAwait(this.MaintainContext);
            baseFile = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(this.MaintainContext) as StorageFile;

            await this.IsFileOutOfDateAsync(baseFile, this.CacheDuration)
                .ConfigureAwait(this.MaintainContext);
            if (baseFile == null || await this.IsFileOutOfDateAsync(baseFile, this.CacheDuration).ConfigureAwait(this.MaintainContext))
            {
                baseFile = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting).AsTask().ConfigureAwait(this.MaintainContext);

                uint retries = 0;
                try
                {
                    while (retries < this.RetryCount)
                    {
                        try
                        {
                            instance = await this.DownloadFileAsync(uri, baseFile, preCacheOnly, cancellationToken, initializerKeyValues).ConfigureAwait(this.MaintainContext);

                            if (instance != null)
                            {
                                break;
                            }
                        }
                        catch (FileNotFoundException)
                        {
                        }

                        retries++;
                    }
                }
                catch (Exception)
                {
                    await baseFile.DeleteAsync().AsTask().ConfigureAwait(false);
                    throw; // re-throwing the exception changes the stack trace. just throw
                }
            }

            if (EqualityComparer<T>.Default.Equals(instance, default(T)) && !preCacheOnly)
            {
                instance = await this.InitializeTypeAsync(baseFile, initializerKeyValues).ConfigureAwait(this.MaintainContext);

                if (this._inMemoryFileStorage.MaxItemCount > 0)
                {
                    var properties = await baseFile.GetBasicPropertiesAsync().AsTask().ConfigureAwait(false);

                    var msi = new InMemoryStorageItem<T>(fileName, properties.DateModified.DateTime, instance);
                    this._inMemoryFileStorage.SetItem(msi);
                }
            }

            return instance;
        }

        private async Task<T> DownloadFileAsync(Uri uri, StorageFile baseFile, bool preCacheOnly, CancellationToken cancellationToken, List<KeyValuePair<string, object>> initializerKeyValues)
        {
            T instance = default(T);

            var file = await StorageFile.ReplaceWithStreamedFileFromUriAsync(
                           baseFile,
                           uri,
                           null);
            using (var stream = await file.OpenStreamForReadAsync()) {
                using (var reader = new StreamReader(stream)) {
                    var text = reader.ReadToEnd();
                }
            }

            instance = await InitializeTypeAsync(file, initializerKeyValues);
            return instance;
        }

        private async Task InternalClearAsync(IEnumerable<StorageFile> files)
        {
            foreach (var file in files)
            {
                try
                {
                    await file.DeleteAsync().AsTask().ConfigureAwait(false);
                }
                catch
                {
                    // Just ignore errors for now}
                }
            }
        }

        /// <summary>
        /// Initializes with default values if user has not initialized explicitly
        /// </summary>
        /// <returns>awaitable task</returns>
        private async Task ForceInitialiseAsync()
        {
            if (this._cacheFolder != null)
            {
                return;
            }

            await this._cacheFolderSemaphore.WaitAsync().ConfigureAwait(false);

            this._inMemoryFileStorage = new InMemoryStorage<T>();

            if (this._baseFolder == null)
            {
                this._baseFolder = ApplicationData.Current.TemporaryFolder;
            }

            if (string.IsNullOrWhiteSpace(this._cacheFolderName))
            {
                this._cacheFolderName = this.GetType().Name;
            }

            try
            {
                this._cacheFolder = await this._baseFolder.CreateFolderAsync(this._cacheFolderName, CreationCollisionOption.OpenIfExists).AsTask().ConfigureAwait(false);
            }
            finally
            {
                this._cacheFolderSemaphore.Release();
            }
        }

        private async Task<StorageFolder> GetCacheFolderAsync()
        {
            if (this._cacheFolder == null)
            {
                await this.ForceInitialiseAsync().ConfigureAwait(false);
            }

            return this._cacheFolder;
        }
    }
}
