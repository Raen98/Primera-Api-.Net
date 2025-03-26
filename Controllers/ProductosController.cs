using Microsoft.AspNetCore.Mvc;
using MiProyectoApi.Data;
using MiProyectoApi.Models;

namespace MiProyectoApi.Controllers
{
    [Route("api/[controller]")] // Define la ruta base como "api/productos"
    [ApiController] // Indica que es un controlador de API REST
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor que inyecta el DbContext 
        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos - Lista todos los productos
        [HttpGet]
        public IActionResult GetProductos()
        {
            var productos = _context.Productos.ToList();
            return Ok(productos); // Devuelve un 200 OK con la lista en JSON
        }

        // GET: api/productos/5 - Obtiene un producto por ID
        [HttpGet("{id}")]
        public IActionResult GetProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound(); // Devuelve 404 si no existe
            }
            return Ok(producto); // Devuelve 200 con el producto en JSON
        }

        // POST: api/productos - Crea un nuevo producto
        [HttpPost]
        public IActionResult CreateProducto([FromBody] ProductoCreateDto productoDto)
        {
            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Precio = productoDto.Precio
                // No seteamos Id, se genera automáticamente
            };
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // PUT: api/productos/5 - Actualiza un producto existente
        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest(); // Devuelve 400 si los IDs no coinciden
            }

            var existingProducto = _context.Productos.Find(id);
            if (existingProducto == null)
            {
                return NotFound(); // Devuelve 404 si no existe
            }

            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;
            _context.SaveChanges();

            return NoContent(); // Devuelve 204 No Content
        }

        // DELETE: api/productos/5 - Elimina un producto
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound(); // Devuelve 404 si no existe
            }

            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return NoContent(); // Devuelve 204 No Content
        }
    }
}