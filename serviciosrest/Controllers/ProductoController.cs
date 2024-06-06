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
        private readonly bdtienditaEntities _modelo;

        public ProductoController()
        {
            _modelo = new bdtienditaEntities();
        }

        // GET: api-tiendita/producto
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllProductos()
        {
            var productos = await _modelo.producto.ToListAsync();
            var listaProducto = productos.Select(p => new ProductoModels
            {
                codpro = p.codpro,
                nompro = p.nompro,
                despro = p.despro,
                prepro = p.prepro,
                canpro = p.canpro,
                estpro = p.estpro
            }).ToList();

            return Ok(listaProducto);
        }

        // GET: api-tiendita/producto/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetProductoById(int id)
        {
            var producto = await _modelo.producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var objProducto = new ProductoModels
            {
                codpro = producto.codpro,
                nompro = producto.nompro,
                despro = producto.despro,
                prepro = producto.prepro,
                canpro = producto.canpro,
                estpro = producto.estpro
            };

            return Ok(objProducto);
        }

        // POST: api-tiendita/producto
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateProducto([FromBody] ProductoModels productoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = new producto
            {
                nompro = productoModel.nompro,
                despro = productoModel.despro,
                prepro = productoModel.prepro,
                canpro = productoModel.canpro,
                estpro = productoModel.estpro,
                codcat = productoModel.codcat
            };

            _modelo.producto.Add(producto);
            await _modelo.SaveChangesAsync();

            return CreatedAtRoute("", new { id = producto.codpro }, productoModel);
        }

        // PUT: api-tiendita/producto/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateProducto(int id, [FromBody] ProductoModels productoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = await _modelo.producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.nompro = productoModel.nompro;
            producto.despro = productoModel.despro;
            producto.prepro = productoModel.prepro;
            producto.canpro = productoModel.canpro;
            producto.estpro = productoModel.estpro;
            producto.codcat = productoModel.codcat;

            await _modelo.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api-tiendita/producto/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteProducto(int id)
        {
            var producto = await _modelo.producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _modelo.producto.Remove(producto);
            await _modelo.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _modelo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
