namespace AutoParts.Data.Model.Entities.Base
{
    using System;

    public abstract class AuditableEntity<TKey> : BaseEntity<TKey>
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public long CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
