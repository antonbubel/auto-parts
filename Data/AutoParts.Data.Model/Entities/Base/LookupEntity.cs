namespace AutoParts.Data.Model.Entities.Base
{
    public abstract class LookupEntity<TKey> : BaseEntity<TKey>
    {
        public string Name { get; set; }
    }
}
