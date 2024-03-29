﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerEnrique.Server.Helpers;
using TallerEnrique.Shared.Complement;
using TallerEnrique.Shared.Entidades;

namespace TallerEnrique.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, vendedor")]
    public class ArticulosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ArticulosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Articulo articulo)
        {
            if (!Exists(articulo.Codigo))
            {
                context.Add(articulo);
                await context.SaveChangesAsync();
                return articulo.Id;
            }
            else
            {
                return BadRequest("El código del artículo ya existe.");
            }
                
        }
        //General
        [HttpGet("cargartodos")]
        [AllowAnonymous] //para que cualquier usario logueado o no puede acceder  a este end-ponit
        public async Task<ActionResult<List<Articulo>>> Get()
        {
            return await context.Articulos.Where(x => x.Estado == true).ToListAsync();
        }
        [HttpGet("articulosinactivos")]
        [AllowAnonymous] //para que cualquier usario logueado o no puede acceder  a este end-ponit
        public async Task<ActionResult<List<Articulo>>> GetInactivos()
        {
            return await context.Articulos.Where(x => x.Estado == false).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Articulo>> Get(int id)
        {
            return await context.Articulos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Articulo articulo)
        {
            context.Attach(articulo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Articulos.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Articulo { Id = id});
            await context.SaveChangesAsync();
            return NoContent();
        }
        //para buscar articulos
        [HttpGet("buscar/{textoBusqueda}")]
        public async Task<ActionResult<List<Articulo>>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda)) { return new List<Articulo>(); }
            textoBusqueda = textoBusqueda.ToLower();
            return await context.Articulos
                .Where(x => x.Nombre.ToLower().Contains(textoBusqueda)).ToListAsync();
        }
        private bool Exists(string codigo)
        {
            return (context.Articulos.Any(e => e.Codigo == codigo));
        }

    }
}
