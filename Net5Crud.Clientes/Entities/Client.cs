using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthChecks.UI.Client;
namespace Net5Crud.Clientes.Entities
{
    public class Client
    {
        [Key] //esto hace que este campo sea primary key en la BD
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(75)")]
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo nombres es obligatorio")]
        public string Nombres { get; set; }

        [Column(TypeName = "varchar(75)")]
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo apellidos es obligatorio")]
        public string Apellidos { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo Departamento es obligatorio")]
        public string Edad { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "El campo Pais es obligatorio")]

        public string Nivel { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Fecha de Registro")]
        [Required(ErrorMessage = "El campo fecha de ingreso es obligatorio")]
        public DateTime FechaRegistro { get; set; }
    }
}
