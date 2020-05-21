namespace AutoParts.Web.Client.Public.Cart.Models
{
    using Protos;

    public class CartItemModel
    {
        public AutoPart AutoPart { get; set; }

        public int Quantity { get; set; }
    }
}
