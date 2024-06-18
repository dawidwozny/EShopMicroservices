namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid id) : base($"Product not found.")
        {
        }
    }
}
