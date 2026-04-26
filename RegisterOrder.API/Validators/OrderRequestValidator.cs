using FluentValidation;
using RegisterOrder.API.DTOs;

namespace RegisterOrder.API.Validators;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(x => x.MenuItemIds)
            .NotNull().WithMessage("A lista de itens não pode ser nula.")
            .NotEmpty().WithMessage("O pedido deve conter ao menos um item.");

        When(x => x.MenuItemIds is { Count: > 0 }, () =>
        {
            RuleFor(x => x.MenuItemIds)
                .Must(ids => ids.Count <= 3)
                .WithMessage("Um pedido pode conter no máximo 3 itens (um sanduíche, uma batata e um refrigerante).");

            RuleFor(x => x.MenuItemIds)
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("O pedido contém identificadores de itens duplicados.");

            RuleForEach(x => x.MenuItemIds)
                .GreaterThan(0)
                .WithMessage("Todos os identificadores de itens devem ser maiores que zero.");
        });
    }
}
