using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class UsuarioAplicacion : IdentityUser
    {
        //vamos a modificar de la tablas de usuarios en BD ya que .Net tiene identity y genera unos campos
        //por defecto ahora agregaremos los que nos falta


        [Required(ErrorMessage = "Nombres es Requerido")]
        [MaxLength(80)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos es Requerido")]
        [MaxLength(80)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Direccion es Requerido")]
        [MaxLength(80)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ciudad  es Requerido")]
        [MaxLength(200)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Pais es Requerido")]
        [MaxLength(60)]
        public string Pais { get; set; }

        //esta propiedad no se subira a la BD este es de referencia ya que este va hacer parte del rol del usuario con el [NotMapped]

        [NotMapped] //no se agrega  a la tabla como columna a la tabla 
        public string Role { get; set; }

    }
}
