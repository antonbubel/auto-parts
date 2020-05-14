namespace AutoParts.Data.Model.Results
{
    public class PaginatedResult<TItem>
    {
        public TItem[] Items { get; set; }

        public int TotalNumberOfItems { get; set; }
    }
}
