using ProductsDBApp.DTO;
using ProductsDBApp.Model;

namespace ProductsDBApp.Service
{
    public interface IProductService
    {
        public List<ProductShowDTO> GetAll();
        public Product? GetById(int id);
        public ProductShowDTO? AddProduct(ProductInsertDTO insertDTO);
        public ProductShowDTO? UpdateProduct(ProductUpdateDTO updateDTO);
        public void DeleteById(int id);

    }
}
