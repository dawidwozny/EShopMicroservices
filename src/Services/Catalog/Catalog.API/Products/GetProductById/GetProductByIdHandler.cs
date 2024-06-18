

namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdQuery> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
           logger.LogInformation("GetProductByIdHandler.Handle{@Query}",query);
           var product = await session.LoadAsync<Product>(query.id, cancellationToken);

            if(product is null)
            {
            throw new ProductNotFoundException(query.id);
            }

            return new GetProductByIdResult(product);
        }
    }
}
