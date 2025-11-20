using Dapper;
using Microsoft.Extensions.Configuration;
using API_Product.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Threading.Tasks;


namespace API_Product.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly string _connectionString;

        // Constructor
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected IDbConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = CreateConnection();
            string sql = "SELECT Id, Name, Description, Price, Category FROM Products";
            var products = await connection.QueryAsync<Product>(sql);
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                string sql = "SELECT Id, Name, Description, Price, Category " +
                    "FROM Products WHERE Id = @Id";
                return await dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
            }
        }

        public async Task AddAsync(Product product)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                string sql = "INSERT INTO Products (Name, Description, Price, Category) " +
                    "VALUES (@Name, @Description, @Price, @Category); " +
                    "SELECT last_insert_rowid();";
                product.Id = await dbConnection.QuerySingleAsync<int>(sql, product);
            }
        }

        public async Task UpdateAsync(Product product)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                string sql = "UPDATE Products SET Name = @Name, Description = @Description, " +
                    "Price = @Price, Category = @Category WHERE Id = @Id";
                await dbConnection.ExecuteAsync(sql, product);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                string sql = "DELETE FROM Products WHERE Id = @Id";
                await dbConnection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
