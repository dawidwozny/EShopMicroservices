
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSucess);

    public class DeleteBasketComandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketComandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //return new DeleteBasketResult(true);
            await repository.DeleteBasket(command.UserName);
            return  new DeleteBasketResult(true);
        }
    }
}
