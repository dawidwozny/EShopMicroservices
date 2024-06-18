﻿

namespace Catalog.API.Products.CreateProduct
{

    public record  CreateProductCommand(Guid Id, string Name, List<String> Categories,string Description,string ImageFile ,decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHnadler : ICommandHandler<CreateProductCommand, CreateProductResult>
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
                Category = command.Categories,
            };


            return new CreateProductResult(Guid.NewGuid());
        }
    }
}