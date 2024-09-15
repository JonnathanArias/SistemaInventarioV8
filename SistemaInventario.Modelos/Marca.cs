using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Marca
    {

        //aqui se crean los campos y tipos de Datos para la BD  en este caso Marca

        [Key]
        public int Id { get; set; }


        [Required (ErrorMessage ="Nombre requerido")]
        [MaxLength(60, ErrorMessage ="Nombre debe ser Maximo de 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion es  requerida")]
        [MaxLength(100, ErrorMessage = "Descripcion debe ser Maximo de 100 caracteres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "Estado es  requerido")]
        public bool Estado { get; set; }


    }
}
