namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// Give the unique keyword and get the cached object
        /// </summary>
        /// <typeparam name="T">
        /// Generic T is using for unguestable types at the return time
        /// </typeparam>
        /// <param name="key">
        /// This key is the unique keyword of the cached object
        /// </param>
        /// <returns>
        /// Returns the cached object what before cached
        /// </returns>
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object data, int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
