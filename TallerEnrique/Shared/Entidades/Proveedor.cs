﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerEnrique.Shared.Entidades
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(100, ErrorMessage = "{0} el nombre debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string NombreEmpresa { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(100, ErrorMessage = "{0} el nombre debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string NombreContacto { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(50, ErrorMessage = "{0} el nombre debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(50, ErrorMessage = "{0} el nombre debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(50, ErrorMessage = "{0} el nombre debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(50, ErrorMessage = "{0} el nombre debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Telefono { get; set; }

        public bool Estado { get; set; } = true;

       
    }
}
