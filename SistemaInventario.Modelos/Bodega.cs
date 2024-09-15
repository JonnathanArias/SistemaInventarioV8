using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{

    //aqui se crean los campos y tipos de Datos para la BD  en este caso Bodega
    public class Bodega
    {
        [Key]   
        public int id { get; set; }

        [Required(ErrorMessage ="Nombre requerido por favor ")]
        [MaxLength(60, ErrorMessage ="Nombre debe ser Maximo de 60 caracteres por favor!")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Descripcion requerido por favor ")]
        [MaxLength(100, ErrorMessage = "Nombre debe ser Maximo de 100 caracteres por favor!")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Estado Requerido por favor ingresar! ")]
        public bool Estado { get; set;}

    }
}
