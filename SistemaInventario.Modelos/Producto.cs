using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Producto
    {
        //vamos a crear todos los campos que necesita la tabla Producto
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numero de serie Obligatorio")]
        [MaxLength(60, ErrorMessage = "Maximo 60 caracteres")]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "Descripcion de producto Obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "Precio producto es Obligatorio")]
        public double Precio { get; set; }


        [Required(ErrorMessage = " El Costo del producto es Obligatorio")]
        public double Costo { get; set; }

        public string ImagenUrl { get; set; } // no se necesita que la imagen del producto sea obligatoria 

        [Required(ErrorMessage = " El Estado del producto es Obligatorio")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = " Categoria es Obligatoria")]
        public int CategoriaId { get; set; }// es obligatorio colocar una categoria al producto

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }// este solo es una navegacion relacionamos y lo agregagamos con ForeignKey [ForeignKey("CategoriaId")]


        [Required(ErrorMessage = " Marca es Obligatoria")]
        public int MarcaId { get; set; }// es obligatorio colocar una Marca al producto

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }// este solo es una navegacion relacionamos y lo agregagamos con ForeignKey [ForeignKey("MarcaId")]

        public int? PadreId { get; set; }//recursividad -propiedad padre relacionar un mismo id de modelos productos dejarlo nulo int?

        public virtual Producto Padre { get; set; }//aqui se realiza recursividad un producto puede tener enlace a un padre 
    }
}
