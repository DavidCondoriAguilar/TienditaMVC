using Newtonsoft.Json;
using serviciosrest.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace serviciosrest.Controllers
{
    [RoutePrefix("api-tiendita/producto")]
    public class ProductoController : ApiController
    {
        private readonly bdtienditaEntities _modelo; // Aquí declaramos una variable privada que será nuestra instancia del modelo de la base de datos.

        public ProductoController()
        {
            _modelo = new bdtienditaEntities(); // En el constructor del controlador, inicializamos nuestra instancia del modelo.
        }

        // GET: api-tiendita/producto
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllProductos() // Este método maneja la solicitud GET para obtener todos los productos.
        {
            var productos = await _modelo.producto.ToListAsync(); // Consultamos todos los productos de la base de datos.
            var listaProducto = productos.Select(p => new ProductoModels // Creamos una lista de modelos de productos para enviar como respuesta.
            {
                codpro = p.codpro,
                nompro = p.nompro,
                despro = p.despro,
                prepro = p.prepro,
                canpro = p.canpro,
                estpro = p.estpro
            }).ToList();

            return Ok(listaProducto); // Devolvemos la lista de productos como respuesta HTTP 200 (OK).
        }

        // GET: api-tiendita/producto/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetProductoById(int id) // Este método maneja la solicitud GET para obtener un producto por su ID.
        {
            var producto = await _modelo.producto.FindAsync(id); // Buscamos el producto en la base de datos por su ID.
            if (producto == null) // Si no se encuentra el producto, devolvemos un error HTTP 404 (Not Found).
            {
                return NotFound();
            }

            var objProducto = new ProductoModels // Creamos un modelo de producto para enviar como respuesta.
            {
                codpro = producto.codpro,
                nompro = producto.nompro,
                despro = producto.despro,
                prepro = producto.prepro,
                canpro = producto.canpro,
                estpro = producto.estpro
            };

            return Ok(objProducto); // Devolvemos el producto encontrado como respuesta HTTP 200 (OK).
        }

        // POST: api-tiendita/producto
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateProducto([FromBody] ProductoModels productoModel) // Este método maneja la solicitud POST para crear un nuevo producto.
        {
            if (!ModelState.IsValid) // Validamos si el modelo recibido es válido.
            {
                return BadRequest(ModelState); // Si no es válido, devolvemos un error HTTP 400 (Bad Request).
            }

            var producto = new producto // Creamos un nuevo objeto de producto con los datos recibidos.
            {
                nompro = productoModel.nompro,
                despro = productoModel.despro,
                prepro = productoModel.prepro,
                canpro = productoModel.canpro,
                estpro = productoModel.estpro,
                codcat = productoModel.codcat
            };

            _modelo.producto.Add(producto); // Agregamos el nuevo producto al contexto de la base de datos.
            await _modelo.SaveChangesAsync(); // Guardamos los cambios en la base de datos.

            return CreatedAtRoute("", new { id = producto.codpro }, productoModel); // Devolvemos la respuesta HTTP 201 (Created) con la URL del nuevo producto creado.
        }

        // PUT: api-tiendita/producto/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateProducto(int id, [FromBody] ProductoModels productoModel) // Este método maneja la solicitud PUT para actualizar un producto existente.
        {
            if (!ModelState.IsValid) // Validamos si el modelo recibido es válido.
            {
                return BadRequest(ModelState); // Si no es válido, devolvemos un error HTTP 400 (Bad Request).
            }

            var producto = await _modelo.producto.FindAsync(id); // Buscamos el producto en la base de datos por su ID.
            if (producto == null) // Si no se encuentra el producto, devolvemos un error HTTP 404 (Not Found).
            {
                return NotFound();
            }

            producto.nompro = productoModel.nompro; // Actualizamos los datos del producto con los recibidos.
            producto.despro = productoModel.despro;
            producto.prepro = productoModel.prepro;
            producto.canpro = productoModel.canpro;
            producto.estpro = productoModel.estpro;
            producto.codcat = productoModel.codcat;

            await _modelo.SaveChangesAsync(); // Guardamos los cambios en la base de datos.

            return StatusCode(HttpStatusCode.NoContent); // Devolvemos una respuesta HTTP 204 (No Content) indicando que la actualización fue exitosa.
        }

        // DELETE: api-tiendita/producto/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteProducto(int id) // Este método maneja la solicitud DELETE para eliminar un producto por su ID.
        {
            var producto = await _modelo.producto.FindAsync(id); // Buscamos el producto en la base de datos por su ID.
            if (producto == null) // Si no se encuentra el producto, devolvemos un error HTTP 404 (Not Found).
            {
                return NotFound();
            }

            _modelo.producto.Remove(producto); // Eliminamos el producto del contexto de la base de datos.
            await _modelo.SaveChangesAsync(); // Guardamos los cambios en la base de datos.

            return StatusCode(HttpStatusCode.NoContent); // Devolvemos una respuesta HTTP 204 (No Content) indicando que la eliminación fue exitosa.
        }

        protected override void Dispose(bool disposing) // Este método se encarga de liberar recursos cuando el controlador se elimina.
        {
            if (disposing)
            {
                _modelo.Dispose(); // Liberamos la instancia del modelo.
            }
            base.Dispose(disposing);
        }
    }
}
