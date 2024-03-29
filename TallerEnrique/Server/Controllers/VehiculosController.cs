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
    public class VehiculosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public VehiculosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Vehiculo vehiculo)
        {
            if (!Exists(vehiculo.Placa))
            {
                context.Add(vehiculo);
                await context.SaveChangesAsync();
                return vehiculo.Id;
            }
            else
            {
                return BadRequest("Número de plca ya existe.");
            }
            
        }

        [HttpGet("cargartodos")]
        public async Task<ActionResult<List<Vehiculo>>> Get()
        {
            return await context.Vehiculos.Where(x => x.Estado == true).Include("Cliente").ToListAsync();
        }
        [HttpGet("vehiculosinactivos")]
        public async Task<ActionResult<List<Vehiculo>>> GetInactivos()
        {
            return await context.Vehiculos.Where(x => x.Estado == false).Include("Cliente").ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> Get(int id)
        {
            return await context.Vehiculos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Vehiculo vehiculo)
        {
            context.Attach(vehiculo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Vehiculos.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Vehiculo { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private bool Exists(string placa)
        {
            return (context.Vehiculos.Any(e => e.Placa == placa));
        }
    }
}
