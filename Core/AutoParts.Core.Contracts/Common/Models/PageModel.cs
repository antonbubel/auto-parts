namespace AutoParts.Core.Contracts.Common.Models
{
    public class PageModel<TModel> where TModel : class
    {
        public int TotalNumberOfItems { get; set; }

        public TModel[] Items { get; set; }
    }
}
