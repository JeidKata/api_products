using Microsoft.AspNetCore.Mvc;
using API_Product.Models;
using API_Product.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Product.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        //Constructor
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Get: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        // Get: api/products/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Post: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productRepository.AddAsync(product);

            // Intentar obtener el producto creador (si el repositorio asigna Id)
            var created = await _productRepository.GetByIdAsync(product.Id);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, created ?? product);
        }

        // Put: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product updatedProduct)
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
