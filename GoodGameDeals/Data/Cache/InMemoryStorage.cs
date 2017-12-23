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
    using System.Linq;

    using Microsoft.Toolkit.Uwp.UI;

    /// <summary>
    /// Generic in-memory storage of items
    /// </summary>
    /// <typeparam name="T">T defines the type of item stored</typeparam>
    public class InMemoryStorage<T>
    {
        private int _maxItemCount;
        private ConcurrentDictionary<string, InMemoryStorageItem<T>> _inMemoryStorage = new ConcurrentDictionary<string, InMemoryStorageItem<T>>();
        private object _settingMaxItemCountLocker = new object();

        /// <summary>
        /// Gets or sets the maximum count of Items that can be stored in this InMemoryStorage instance.
        /// </summary>
        public int MaxItemCount
        {
            get
            {
                return this._maxItemCount;
            }

            set
            {
                if (this._maxItemCount == value)
                {
                    return;
                }

                this._maxItemCount = value;

                lock (this._settingMaxItemCountLocker)
                {
                    this.EnsureStorageBounds(value);
                }
            }
        }

        /// <summary>
        /// Clears all items stored in memory
        /// </summary>
        public void Clear()
        {
            this._inMemoryStorage.Clear();
        }

        /// <summary>
        /// Clears items stored in memory based on duration passed
        /// </summary>
        /// <param name="duration">TimeSpan to identify expired items</param>
        public void Clear(TimeSpan duration)
        {
            DateTime expirationDate = DateTime.Now.Subtract(duration);

            var itemsToRemove = this._inMemoryStorage.Where(kvp => kvp.Value.LastUpdated <= expirationDate).Select(kvp => kvp.Key);

            if (itemsToRemove.Any())
            {
                this.Remove(itemsToRemove);
            }
        }

        /// <summary>
        /// Remove items based on provided keys
        /// </summary>
        /// <param name="keys">identified of the in-memory storage item</param>
        public void Remove(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                InMemoryStorageItem<T> tempItem = null;

                this._inMemoryStorage.TryRemove(key, out tempItem);

                tempItem = null;
            }
        }

        /// <summary>
        /// Add new item to in-memory storage
        /// </summary>
        /// <param name="item">item to be stored</param>
        public void SetItem(InMemoryStorageItem<T> item)
        {
            if (this.MaxItemCount == 0)
            {
                return;
            }

            this._inMemoryStorage[item.Id] = item;

            // ensure max limit is maintained. trim older entries first
            if (this._inMemoryStorage.Count > this.MaxItemCount)
            {
                var itemsToRemove = this._inMemoryStorage.OrderBy(kvp => kvp.Value.Created).Take(this._inMemoryStorage.Count - this.MaxItemCount).Select(kvp => kvp.Key);
                this.Remove(itemsToRemove);
            }
        }

        /// <summary>
        /// Get item from in-memory storage as long as it has not ex
        /// </summary>
        /// <param name="id">id of the in-memory storage item</param>
        /// <param name="duration">timespan denoting expiration</param>
        /// <returns>Valid item if not out of date or return null if out of date or item does not exist</returns>
        public InMemoryStorageItem<T> GetItem(string id, TimeSpan duration)
        {
            InMemoryStorageItem<T> tempItem = null;

            if (!this._inMemoryStorage.TryGetValue(id, out tempItem))
            {
                return null;
            }

            DateTime expirationDate = DateTime.Now.Subtract(duration);

            if (tempItem.LastUpdated > expirationDate)
            {
                return tempItem;
            }

            this._inMemoryStorage.TryRemove(id, out tempItem);

            return null;
        }

        private void EnsureStorageBounds(int maxCount)
        {
            if (this._inMemoryStorage.Count == 0)
            {
                return;
            }

            if (maxCount == 0)
            {
                this._inMemoryStorage.Clear();
                return;
            }

            if (this._inMemoryStorage.Count > maxCount)
            {
                this.Remove(this._inMemoryStorage.Keys.Take(this._inMemoryStorage.Count - maxCount));
            }
        }
    }
}
