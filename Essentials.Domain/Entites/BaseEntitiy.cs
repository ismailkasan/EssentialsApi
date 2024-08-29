using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Essentials.Domain
{
    /// <summary>
    /// Defines the <see cref="BaseEntitiy" />.
    /// </summary>
    public abstract class BaseEntitiy : IEntity
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
