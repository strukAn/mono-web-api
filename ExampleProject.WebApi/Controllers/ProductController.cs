using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.Extensions.FileSystemGlobbing;
using Npgsql;

namespace ExampleProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=admin;" +
            "Database=db2";

        private NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] int stock = -1, [FromQuery] string name = "", [FromQuery] string description = "")
        {
            List<Product> products = new List<Product>();
            StringBuilder builder = new StringBuilder();
            string command = "select p.*, c.\"Name\" as \"CategoryName\" from \"Product\" as p left join \"Category\" as c on (p.\"CategoryId\" = c.\"Id\") where (1=1";
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
            builder.Append(command);

            if (name != "")
            {
                builder.Append(" and p.\"Name\" = @Name");
                cmd.Parameters.AddWithValue("@Name", name);
            }
            if (description != "")
            {
                builder.Append(" and p.\"Description\" LIKE @Description");
                string desc = $"%{description}%";
                cmd.Parameters.AddWithValue("@Description", desc);
            }
            if (stock != -1)
            {
                builder.Append(" and p.\"Stock\" = @Stock");
                cmd.Parameters.AddWithValue("@Stock", stock);
            }
            builder.Append(")");
                
            cmd.CommandText = builder.ToString();

            connection.Open();

            try
            {
                using NpgsqlDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(
                            (int)reader["Id"],
                            (string)reader["Name"],
                            reader.IsDBNull(2) ? "" : (string)reader["Description"],
                            (int)reader["Stock"],
                            reader.IsDBNull(4) || reader.IsDBNull(5) ? null : new Category((int)reader["CategoryId"], (string)reader["CategoryName"])
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = new Product();
            string command = "select p.*, c.\"Name\" as \"CategoryName\" from \"Product\" as p left join \"Category\" as c on (p.\"CategoryId\" = c.\"Id\") where (p.\"Id\" = @Id)";
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            connection.Open();

            try
            {
                using NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    product.Id = (int)reader["Id"];
                    product.Name = (string)reader["Name"];
                    product.Description = reader.IsDBNull(2) ? "" : (string)reader["Description"];
                    product.Stock = (int)reader["Stock"];
                    product.Category = reader.IsDBNull(4) || reader.IsDBNull(5) ? null : new Category((int)reader["CategoryId"], (string)reader["CategoryName"]);
                } else
                {
                    return NotFound($"Product with id={id} not found");
                }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } finally
            {
                connection.Close();
            }

            return Ok(product);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            int result = 0;

            string command = "delete from \"Product\" where (\"Id\" = @Id)";
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
            cmd.Parameters.AddWithValue("Id", id);

            connection.Open();

            try
            {
                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    return BadRequest("Failed to delete product");
                }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            } finally
            {
                connection.Close();
            }

            return Ok("Product deleted");
        }

        [HttpPost("add")]
        public IActionResult Post(ProductCategoryDTO productDto)
        {
            int result = 0;
            string command = "insert into \"Product\" (\"Id\", \"Name\", \"Description\", \"Stock\", \"CategoryId\") values(DEFAULT, @Name, @Description, @Stock, @CategoryId)";
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);
            Product product = new Product();

            if (productDto.CategoryId == 0)
            {
                return UnprocessableEntity("Missing important fields");
            }

            if (productDto.ProductId == 0)
            {
                cmd.Parameters.AddWithValue("@Id", productDto.ProductId);
            }
            cmd.Parameters.AddWithValue("@Name", productDto.Name);
            cmd.Parameters.AddWithValue("@Description", productDto.Description);
            cmd.Parameters.AddWithValue("@Stock", productDto.Stock);
            cmd.Parameters.AddWithValue("@CategoryId", productDto.CategoryId);

            try
            {
                connection.Open();
                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    return BadRequest("Failed to add product");
                }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } finally
            {
                connection.Close();
            }

            return Created("", "Product created");
        }

        [HttpPut("update")]
        public IActionResult Put(int id, ProductCategoryDTO updated)
        {
            int result = 0;
            string command = "update \"Product\" set \"Name\" = @Name, \"Description\" = @Description, \"Stock\" = @Stock, \"CategoryId\" = @CategoryId where (\"Id\" = @Id)";
            NpgsqlCommand cmd = new NpgsqlCommand(command, connection);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", updated.Name);
            cmd.Parameters.AddWithValue("@Description", updated.Description);
            cmd.Parameters.AddWithValue("@Stock", updated.Stock);
            cmd.Parameters.AddWithValue("@CategoryId", updated.CategoryId);

            try
            {

                connection.Open();
                result = cmd.ExecuteNonQuery();

                if(result == 0)
                {
                    return BadRequest("Failed to update product");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } finally
            {
                connection.Close();
            }

            return Created("", "Product updated");
        }
    }
}