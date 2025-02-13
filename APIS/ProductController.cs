using Microsoft.AspNetCore.Mvc;
using Proyecto.Domain;

namespace Proyecto.APIS
{
    [Route("api/productos")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //DB Simulation to represent the search steps
        private static List<ProductsModel> _storage = new List<ProductsModel>();

        private ActionResult? ValidateProduct(ProductsModel product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("El nombre del producto no puede estar vacío.");
            }
            if (product.Price < 0)
            {
                return BadRequest("El precio del producto no puede ser negativo.");
            }

            if (product.Stock < 0)
            {
                return BadRequest("El stock del producto no puede ser negativo.");
            }
            return null;
        }
        [HttpGet]
        public ActionResult<List<ProductsModel>> GetAllProducts()
        {
            return Ok(_storage);
        }

        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            var product = _storage.Find(products => products.Id == id);
            if (product == null)
            {
                return NotFound("Producto no encontrado.");
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult CreateNewProduct([FromBody] ProductsModel newProduct)
        {
            var validationResult = ValidateProduct(newProduct);
            if (validationResult != null)
                return validationResult;

            newProduct.Id = _storage.Count + 1;
            newProduct.CreationDate = System.DateTime.Now;
            _storage.Add(newProduct);

            return Created();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProductById(int id, [FromBody] ProductsModel updatedProduct)
        {
            var product = _storage.Find(products => products.Id == id);
            if (product == null)
                return NotFound("Producto no encontrado.");

            var validationResult = ValidateProduct(updatedProduct);
            if (validationResult != null)
                return validationResult;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;

            return Ok("Producto actualizado con exito.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProductById(int id)
        {
            var product = _storage.Find(products => products.Id == id);
            if (product == null)
                return NotFound("Producto no encontrado.");
            _storage.Remove(product);

            return Ok("Producto eliminado con exito.");
        }
    }
}