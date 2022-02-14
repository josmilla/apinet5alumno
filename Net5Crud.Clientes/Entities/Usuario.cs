using Net5Crud.Clientes.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Crud.Clientes.Entities
{
    public class Usuario
    {

        public int id { get; set; }



        [Column(TypeName = "varchar(15)")]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        public string usuario { get; set; }

        [Column(TypeName = "varchar(15)")]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [ContrasenaValidate(ErrorMessage = "Contraseña no valida")]
        [DataType(DataType.Password)]

        public string contrasena { get; set; }

        public int intentos { get; set; }

        public decimal nivelSeg { get; set; }

        public DateTime? fechaReg { get; set; }
    }
}
