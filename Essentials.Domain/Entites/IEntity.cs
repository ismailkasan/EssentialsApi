namespace Essentials.Domain
{
    /// <summary>
    /// Defines the <see cref="IEntity" />.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
