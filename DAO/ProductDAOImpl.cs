using ProductsDBApp.Model;
using ProductsDBApp.Util;
using System.Data.SqlClient;

namespace ProductsDBApp.DAO
{
    public class ProductDAOImpl : IProductDAO
    {
        public Product? AddProduct(Product product)
        {
            string sql = "INSERT INTO PRODUCTS(NAME, DESCRIPTION, PRICE, QUANTITY) VALUES(@name, @description, @price, @quantity); ";
            sql += "SELECT SCOPE_IDENTITY();";
            Product? newProduct = null;
            int id;
            using SqlConnection conn = DBUtil.GetConnection();
            conn!.Open();
            using SqlCommand insertQuery = new(sql, conn);
            insertQuery.Parameters.AddWithValue("name", product.Name);
            insertQuery.Parameters.AddWithValue("description", product.Description);
            insertQuery.Parameters.AddWithValue("price", product.Price);
            insertQuery.Parameters.AddWithValue("quantity", product.Quantity);
            var returnId = insertQuery.ExecuteScalar();
            if(!int.TryParse(returnId.ToString(), out id)){
                return null;
            }
            sql = "SELECT * FROM PRODUCTS WHERE ID = @id";
            using SqlCommand selectQuery = new(sql, conn);
            selectQuery.Parameters.AddWithValue("id", id);
            using SqlDataReader rs= selectQuery.ExecuteReader();
            if (rs.Read())
            {
                newProduct = new()
                {
                    Id = rs.GetInt32(rs.GetOrdinal("ID")),
                    Name = rs.GetString(rs.GetOrdinal("NAME")),
                    Description = rs.GetString(rs.GetOrdinal("DESCRIPTION")),
                    Price = rs.GetDecimal(rs.GetOrdinal("PRICE")),
                    Quantity = rs.GetInt32(rs.GetOrdinal("QUANTITY"))
                };
            }
            return newProduct;
            
        }

        public void DeleteProduct(int id)
        {
            string sql = "DELETE FROM PRODUCTS WHERE ID = @id";
            using SqlConnection conn = DBUtil.GetConnection();
            conn!.Open();
            using SqlCommand query = new(sql, conn);
            query.Parameters.AddWithValue("id", id);
            query.ExecuteNonQuery();
        }

        public List<Product> GetAll()
        {
            List<Product> products = new();
            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();
            string sql = "SELECT * FROM PRODUCTS";
            using SqlCommand query = new(sql, conn);
            using SqlDataReader rs = query.ExecuteReader();
            while (rs.Read())
            {
                Product product = new()
                {
                    Id = rs.GetInt32(rs.GetOrdinal("ID")),
                    Name = rs.GetString(rs.GetOrdinal("NAME")),
                    Description = rs.GetString(rs.GetOrdinal("DESCRIPTION")),
                    Price = rs.GetDecimal(rs.GetOrdinal("PRICE")),
                    Quantity = rs.GetInt32(rs.GetOrdinal("QUANTITY"))

                };
                products.Add(product);
            }
            return products;

        }

        public Product? GetById(int id)
        {
            Product? product =null;
            using SqlConnection? conn  =DBUtil.GetConnection();
            conn!.Open();
            using SqlCommand selectQuery = new("SELECT * FROM PRODUCTS WHERE ID = @id", conn);
            selectQuery.Parameters.AddWithValue("id", id);
            using SqlDataReader rs = selectQuery.ExecuteReader();
            if (rs.Read())
            {
                product = new()
                {
                    Id = rs.GetInt32(rs.GetOrdinal("ID")),
                    Name = rs.GetString(rs.GetOrdinal("NAME")),
                    Description = rs.GetString(rs.GetOrdinal("DESCRIPTION")),
                    Price = rs.GetDecimal(rs.GetOrdinal("PRICE")),
                    Quantity = rs.GetInt32(rs.GetOrdinal("QUANTITY"))
                };

                
            }
            return product;

        }

        public Product? UpdateProduct(Product product)
        {
            string sql = "UPDATE PRODUCTS SET NAME = @name, DESCRIPTION = @description, PRICE = @price, QUANTITY = @quantity " 
                +"WHERE ID = @id";
            using SqlConnection conn = DBUtil.GetConnection();
            conn!.Open();
            using SqlCommand query = new(sql, conn);
            query.Parameters.AddWithValue("name", product.Name);
            query.Parameters.AddWithValue("description", product.Description);
            query.Parameters.AddWithValue("price", product.Price);
            query.Parameters.AddWithValue("quantity", product.Quantity);
            query.Parameters.AddWithValue("id", product.Id);
            query.ExecuteNonQuery();
            return product;
        }
    }
}
