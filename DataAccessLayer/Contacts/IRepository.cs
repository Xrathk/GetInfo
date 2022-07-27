namespace DataAccessLayer.Contacts
{
    /// <summary>
    /// Base interface with methods for database interaction for repositories.
    /// </summary>
    /// <typeparam name="T">The object type</typeparam>
    public interface IRepository<T>
    {
        public Task<T> Create(T _object); // Create new object
        public void Delete(T _object); // Delete existing object
        public void Update(T _object); // Update existing object
        public IEnumerable<T> GetAll(); // Get all objects
        public T GetById(int Id); // Retrieve object by ID
    }
}
