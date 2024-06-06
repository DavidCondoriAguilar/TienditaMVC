using Newtonsoft.Json;
using serviciosrest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace serviciosrest.Controllers
{
    [RoutePrefix("api-tiendita/categoria")]
    public class CategoriaController : ApiController
    {
        //declaro un objeto del modelo creado con el ADO.Net Entity Data Model
        bdtienditaEntities modelo = new bdtienditaEntities();

        // GET: api-tiendita/categoria
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> FindAll()
        {
            List<CategoriaModels> listacategoria = new List<CategoriaModels>();
            var categorias = await modelo.categoria.ToListAsync();
            foreach (var categoria in categorias)
            {
                CategoriaModels objcategoria = new CategoriaModels
                {
                    codigo = categoria.codcat,
                    nombre = categoria.nomcat,
                    estado = categoria.estcat
                };
                listacategoria.Add(objcategoria);
            }

            return Ok(listacategoria);
        }

        // GET: api-tiendita/categoria/
        [HttpGet]
        [Route("custom")]
        public async Task<IHttpActionResult> FindAllCustom()
        {
            List<CategoriaModels> listacategoria = new List<CategoriaModels>();
            var categorias = await modelo.categoria.Where(c => c.estcat == true).ToListAsync();
            foreach (var categoria in categorias)
            {
                CategoriaModels objcategoria = new CategoriaModels
                {
                    codigo = categoria.codcat,
                    nombre = categoria.nomcat,
                    estado = categoria.estcat
                };
                listacategoria.Add(objcategoria);
            }

            return Ok(listacategoria);
        }

        // GET:BY ID
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var categoria = await modelo.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            CategoriaModels objcategoria = new CategoriaModels
            {
                codigo = categoria.codcat,
                nombre = categoria.nomcat,
                estado = categoria.estcat
            };

            return Ok(objcategoria);
        }

        // POST: 
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CategoriaModels categoriaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = new categoria
            {
                codcat = categoriaModel.codigo,
                nomcat = categoriaModel.nombre,
                estcat = categoriaModel.estado
            };

            modelo.categoria.Add(categoria);
            await modelo.SaveChangesAsync();

            return CreatedAtRoute("", new { id = categoria.codcat }, categoriaModel);
        }

        // PUT: 
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] CategoriaModels categoriaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await modelo.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            categoria.nomcat = categoriaModel.nombre;
            categoria.estcat = categoriaModel.estado;

            await modelo.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api-tiendita/categoria/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var categoria = await modelo.categoria.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }

                modelo.categoria.Remove(categoria);
                await modelo.SaveChangesAsync();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    return Content(HttpStatusCode.Conflict, "No se puede eliminar la categoría porque hay productos asociados a ella.");
                }
                else
                {
                    return InternalServerError(ex);
                }
            }
        }

    }
}
