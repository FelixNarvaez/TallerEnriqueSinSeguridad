﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerEnrique.Shared.Entidades
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }
        public long  NFactura { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")] public DateTime Fecha { get; set; } = DateTime.Today;
        public decimal Descuento { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal ManoObra { get; set; }
        public bool Estado { get; set; } = true;
        public decimal SubTotal
        {
            get
            {
                return DVentas.Sum
                      (x => (x.Cantidad * x.PrecioVenta) - ((x.Cantidad * x.PrecioVenta) * x.Descuento / 100));
            }
            set { }
        }
        public decimal IVA
        {
            get
            {

                return DVentas.Sum
                     (x => (x.Cantidad * x.PrecioVenta) * (15M / 100M));
            }
            set { }
        }
        public decimal Total { get; set; }
        public FormasPago FormaPago { get; set; } = FormasPago.Córdoba;


        //Estableciendo la relacion entre tablas
        public int VehiculoId { get; set; }
        public int? MonedaId { get; set; }
        public int? ClienteId { get; set; }
        public int MecanicoId { get; set; }
        public int ServicioId { get; set; }
        public int? CategoriaId { get; set; }

        public Vehiculo Vehiculo { get; set; }
        public Moneda Moneda { get; set; }
        public Cliente Cliente { get; set; }
        public Mecanico Mecanico { get; set; }
        public Servicio Servicio  { get; set; }
        public Categoria Categoria { get; set; }
        //public DVenta DVenta { get; set; }

        public List<DVenta> DVentas { get; set; } = new List<DVenta>();
    }

    public enum FormasPago // segun los requerimientos del cliente solo se aceptara córdoba como moneda de pago
    {
        Córdoba = 1
    }
}
