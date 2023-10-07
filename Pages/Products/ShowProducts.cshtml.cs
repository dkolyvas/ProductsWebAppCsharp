using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsDBApp.DTO;
using ProductsDBApp.Model;
using ProductsDBApp.Service;

namespace ProductsDBApp.Pages.Products
{
    public class ShowProductsModel : PageModel
    {
        public List<ProductShowDTO> Products { get; set; } = new();
        public List<Error> ErrorList { get; set; } = new();
        
        private IProductService service;

        public ShowProductsModel(IProductService service)
        {
            this.service = service;
        }

        public void OnGet()
        {
            if (Request.Query["deleteError"] == "true")
            {
                ErrorList.Add(new Error("", "Error deleting the requested product", ""));
            }
            try
            {
                Products = service.GetAll();
            }catch(Exception ex)
            {
                ErrorList.Add(new Error("", ex.Message, ""));
            }
        }
    }
}
