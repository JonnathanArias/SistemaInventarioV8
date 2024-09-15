using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Categoria
    {
        //aqui se crean los campos y tipos de Datos para la BD  en este caso Categoria

        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Nombre es Requerido")]
        [MaxLength (70, ErrorMessage ="Nombre debe ser Maximo de 70 Caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Descripcion es Requerido")]
        [MaxLength(100, ErrorMessage = "Nombre debe ser Maximo de 100 Caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es Requerido")]
        public bool Estado { get; set; }
    }
}
