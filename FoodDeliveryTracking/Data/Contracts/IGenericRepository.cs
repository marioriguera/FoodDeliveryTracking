using System.Linq.Expressions;

namespace FoodDeliveryTracking.Data.Contracts
{
    /// <summary>
    /// Represents a generic repository interface for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <returns>A collection of entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets entities that match the specified filter asynchronously.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>A collection of filtered entities.</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets an entity that matches the specified predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate expression.</param>
        /// <returns>The matching entity or null if not found.</returns>
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);
    }
}
