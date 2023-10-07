using FluentValidation;
using ProductsDBApp.DTO;

namespace ProductsDBApp.Validator
{
    public class ProductInsertValidator: AbstractValidator<ProductInsertDTO>
    {
        public ProductInsertValidator()
        {
            RuleFor(p =>p.Name).NotEmpty().WithMessage("Product name is a required field")
                .Length(2,50).WithMessage("Product name must be 2-50 characters long");
            
        }
    }
}
