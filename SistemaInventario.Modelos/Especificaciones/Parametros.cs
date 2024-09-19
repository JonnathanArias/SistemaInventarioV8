using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.Especificaciones
{
    public   class Parametros
    {

        //vamos a crear propiedades para la paginacion
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 4;  // este es para el tamaño de cada una de las pagina 4 registros por pagina para que se muestre 
    }
}
