using ProductsDBApp.Model;

namespace ProductsDBApp.DAO
{
    public interface IProductDAO
    {
        public List<Product> GetAll();
        public Product? GetById(int id);
        public Product? AddProduct(Product product);

        public Product? UpdateProduct(Product product);
        public void DeleteProduct(int id);

    }
}
