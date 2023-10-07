using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsDBApp.DTO;
using ProductsDBApp.Model;
using ProductsDBApp.Service;

namespace ProductsDBApp.Pages.Products
{
    public class UpdateModel : PageModel
    {
        public ProductUpdateDTO UpdateDTO { get; set; } = new();
        public List<Error> ErrorList { get; set; } = new();
        private IProductService _productService;
        private IValidator<ProductUpdateDTO> _validator;
        private IMapper _mapper;

        public UpdateModel(IProductService productService, IValidator<ProductUpdateDTO> validator, IMapper mapper)
        {
            _productService = productService;
            _validator = validator;
            _mapper = mapper;
        }

        public void OnGet(int id)
        {
            try
            {
                Product? product = _productService.GetById(id);
                if (product is null) 
                {
                    ErrorList.Add(new Error("", "We couldn't find a product with id" + id, "id"));
                    return;
                }
                UpdateDTO = _mapper.Map<ProductUpdateDTO>(product);
                
            }catch (Exception ex)
            {
                ErrorList.Add(new Error("", ex.Message, ""));
            }
        }

        public void OnPost(ProductUpdateDTO dto)
        {
            UpdateDTO = dto;
            var validationResults = _validator.Validate(UpdateDTO);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    ErrorList.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
            }
            if (!int.TryParse(Request.Form["quantity"], out int testInt))
            {
                ErrorList.Add(new Error("", "You must provide a valid integer number for quantity", "quantity"));
            }
            if (!decimal.TryParse(Request.Form["price"], out decimal testDec))
            {
                ErrorList.Add(new Error("", "You must provide a valid decimal number for price", "price"));
            }
            if (ErrorList.Count > 0) return;
            try
            {
                ProductShowDTO? result = _productService.UpdateProduct(UpdateDTO);
                if(result is null)
                {
                    ErrorList.Add(new Error("", "We couldn't find product with id" + dto.Id, ""));
                    return;
                }
                Response.Redirect("/products");
            }catch(Exception e)
            {
                ErrorList.Add(new Error("", e.Message, ""));
                return;
            }

            
        }
    }
}
