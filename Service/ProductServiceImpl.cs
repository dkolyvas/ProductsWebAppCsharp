using AutoMapper;
using ProductsDBApp.DAO;
using ProductsDBApp.DTO;
using ProductsDBApp.Model;
using System.Transactions;

namespace ProductsDBApp.Service
{
    public class ProductServiceImpl : IProductService
    {
        private IProductDAO dao;
        private IMapper mapper;
        private ILogger<ProductServiceImpl> logger;

        public ProductServiceImpl(IProductDAO dao, IMapper mapper, ILogger<ProductServiceImpl> logger)
        {
            this.dao = dao;
            this.mapper = mapper;
            this.logger = logger;
        }

        public ProductShowDTO? AddProduct(ProductInsertDTO insertDTO)
        {
            if (insertDTO is null) return null;
            Product product = mapper.Map<Product>(insertDTO);
            try
            {
                using TransactionScope scope = new();
                Product? newProduct = dao.AddProduct(product);
                scope.Complete();
                return mapper.Map<ProductShowDTO>(newProduct);
            }catch(Exception e)
            {
                logger.LogError("An error occured while inserting a new product", e.Message);
                throw;
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                using TransactionScope scope = new();
                dao.DeleteProduct(id);
                scope.Complete();
            }catch(Exception e)
            {
                logger.LogError("An error occured while deleting product", e.Message);
                throw;
            }
        }

        public List<ProductShowDTO> GetAll()
        {
            List<ProductShowDTO> results = new();
            try
            {
                
                List<Product> products = dao.GetAll();
                foreach (Product product in products)
                {
                    ProductShowDTO currDTO = mapper.Map<ProductShowDTO>(product);
                    results.Add(currDTO);
                }
            }catch(Exception e)
            {
                logger.LogError("An error ocurred while retrieving product list", e.Message);
                throw;
            }
            return results;
        }

        public Product? GetById(int id)
        {
            Product? product = null;
            try
            {
                
                product = dao.GetById(id);
                

            }catch(Exception e)
            {
                logger.LogError("Error retrieving product" , e.Message);
                throw;
            }
            return product;
        }

        public ProductShowDTO? UpdateProduct(ProductUpdateDTO updateDTO)
        {
            if (updateDTO is null) return null;
            try
            {
                using TransactionScope scope = new();
                Product product = mapper.Map<Product>(updateDTO);
                Product? updatedProduct = dao.UpdateProduct(product);
                scope.Complete();
                return mapper.Map<ProductShowDTO>(updatedProduct);
            }catch(Exception e)
            {
                logger.LogError("An error occured while updating product", e.Message);
                throw;
            }
        }
    }
}
