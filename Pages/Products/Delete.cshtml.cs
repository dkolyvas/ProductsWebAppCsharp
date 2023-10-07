using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsDBApp.Model;
using ProductsDBApp.Service;

namespace ProductsDBApp.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private IProductService _productService;
        public Error? error { get; set; }=null;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet(int id)
        {
            try
            {
                _productService.DeleteById(id);
                Response.Redirect("/products");
            }catch (Exception ex)
            {
                error = new("", ex.Message, "");
                Console.Error.WriteLine(ex.Message);
            
                Response.Redirect("/products?deleteError=true");
            }
        }
    }
}
