﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerEnrique.Server;
using TallerEnrique.Shared.Entidades;

namespace TallerEnrique.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CategoriasController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Categoria categoria)
        {
            context.Add(categoria);
            await context.SaveChangesAsync();
            return categoria.Id;
        }
        //generico
        [HttpGet("cargartodos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            return await context.Categorias.Where(x => x.Estado == true).ToListAsync();
        }
        [HttpGet("categoriasinactivas")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Categoria>>> GetInactivos()
        {
            return await context.Categorias.Where(x => x.Estado == false).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            return await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Categoria categoria)
        {
            context.Attach(categoria).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Categorias.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Categoria { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
        //para buscar articulos
        [HttpGet("buscar/{textoBusqueda}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Categoria>>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda)) { return new List<Categoria>(); }
            textoBusqueda = textoBusqueda.ToLower();
            return await context.Categorias
                .Where(x => x.Nombre.ToLower().Contains(textoBusqueda)).ToListAsync();
        }
    }
}
