using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsDBApp.DTO;
using ProductsDBApp.Model;
using ProductsDBApp.Service;

namespace ProductsDBApp.Pages.Products
{
    public class AddModel : PageModel
    {
        public ProductInsertDTO InsertDTO { get; set; } = new();
        public List<Error> ErrorList { get; set; } = new();
        private IProductService _productService;
        private IValidator<ProductInsertDTO> _validator;

        public AddModel(IProductService productService, IValidator<ProductInsertDTO> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        public void OnGet()
        {
        }

        public void OnPost(ProductInsertDTO formData)
        {
            InsertDTO = formData;
            var validationResult = _validator.Validate(InsertDTO);
            if(!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
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
                ProductShowDTO? newProduct = _productService.AddProduct(InsertDTO);
                Response.Redirect("/products");


            }catch(Exception ex)
            {
                ErrorList.Add(new Error("", ex.Message, ""));
            }

        }
    }
}
