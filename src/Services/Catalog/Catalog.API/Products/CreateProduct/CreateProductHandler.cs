



namespace Catalog.API.Products.CreateProduct
{

    public record  CreateProductCommand(Guid Id, string Name, List<string> Category,string Description,string ImageFile ,decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHnadler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // create product entity
            // save to databse
            // return product result
            var prodct = new Product
            {
                Name = command.Name,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Category = command.Category,
            };

            session.Store(prodct);
            await session.SaveChangesAsync(cancellationToken);


            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
