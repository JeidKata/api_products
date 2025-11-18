using Microsoft.AspNetCore.Mvc;

namespace API_Product.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Models.Product> _products = new List<Models.Product>
        {
            new Models.Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop", Price = 999.99m, Category = "Electronics" },
            new Models.Product { Id = 2, Name = "Smartphone", Description = "A latest model smartphone", Price = 699.99m, Category = "Electronics" },
            new Models.Product { Id = 3, Name = "Desk Chair", Description = "Ergonomic office chair", Price = 149.99m, Category = "Furniture" },
            new Models.Product { Id = 4, Name = "Coffee Maker", Description = "Automatic coffee maker", Price = 89.99m, Category = "Appliances" },
            new Models.Product { Id = 5, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m, Category = "Electronics" }
        };

        // Get: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Models.Product>> GetProducts()
        {
            return Ok(_products);
        }

        // Get: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Models.Product> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Post: api/products
        [HttpPost]
        public ActionResult<Models.Product> CreateProduct([FromBody] Models.Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // Put: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Models.Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest();
            }

            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Category = updatedProduct.Category;
            return NoContent();
        }

        // Delete: api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var productRemove = _products.FirstOrDefault(p => p.Id == id);
            if (productRemove == null)
            {
                return NotFound();
            }
            _products.Remove(productRemove);
            return NoContent();
        }
    }
}
