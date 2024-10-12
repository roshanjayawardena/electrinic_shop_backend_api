namespace Electronic_Domain.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
