namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Base interface with methods for database interaction for repositories.
    /// </summary>
    /// <typeparam name="T">The object type</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Create new database table entry.
        /// </summary>
        /// <param name="_object">Object type</param>
        /// <returns></returns>
        public Task<T> Create(T _object);

        /// <summary>
        /// Deletes a database table entry.
        /// </summary>
        /// <param name="_object">Object type</param>
        public void Delete(T _object);

        /// <summary>
        /// Updates a database table entry.
        /// </summary>
        /// <param name="_object">Object type</param>
        public void Update(T _object); 

        /// <summary>
        /// Gets all database table entries.
        /// </summary>
        /// <returns>Database table</returns>
        public IEnumerable<T> GetAll(); 

        /// <summary>
        /// Returns a database table entry by its Id.
        /// </summary>
        /// <param name="Id">Object Id</param>
        /// <returns>Object with the parameter Id</returns>
        public T GetById(int Id); 
    }
}
