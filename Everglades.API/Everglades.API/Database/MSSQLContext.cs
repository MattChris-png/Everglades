using Everglades.Library.DTO;
using Everglades.Library.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Everglades.API.Database
{
    public class MSSQLContext
    {

        string connectionString = @"Server = DESKTOP-B0QRMKN\EVERGLADESSQL; Database = Everglades; Trusted_Connection = yes;TrustServerCertificate=True";
        public Product AddProduct(Product p)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    /*
                     * 
                     * exec Product.InsertProduct @Name = 'SP Product',
@Description = 'Product inserted from SP',
@Quantity = 10,
@Price = 1.23,
@Id = @newID out
                     */
                    var sql = $"Product.InsertProduct";
                    cmd.CommandText = sql;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", $"'{p.Name}'"));
                    cmd.Parameters.Add(new SqlParameter("@Description", $"'{p.Description}'"));
                    cmd.Parameters.Add(new SqlParameter("Quantity", p.Quantity));
                    cmd.Parameters.Add(new SqlParameter("Price", p.Price));

                    var id = new SqlParameter("Id", p.Id);
                    id.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    p.Id = (int)id.Value;
                }
            }
            return p;
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    var sql = $"SELECT Id, REPLACE(name, '''','') as Name, Description, Price, Quantity FROM PRODUCT";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                Quantity = (int)reader["Quantity"]
                            });

                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return products;
        }

        public ProductDTO? DeleteProductAsync(int id)
        {
            ProductDTO? deletedProduct = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Product.DeleteProduct";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", id));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                        // Optionally, retrieve the deleted product details before deleting
                    deletedProduct = new ProductDTO { Id = id };
                }
            }

            return deletedProduct;
        }


        public ProductDTO? UpdateProduct(Product p)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Product.UpdateProduct";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Name", p.Name));
                    cmd.Parameters.Add(new SqlParameter("@Description", p.Description));
                    cmd.Parameters.Add(new SqlParameter("@Quantity", p.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@Price", p.Price));
                    cmd.Parameters.Add(new SqlParameter("@Id", p.Id));

                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    return new ProductDTO(p);
                    
                }
            }
        }

    }
}
