using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.Especificaciones
{
    public class MetaData
    {

        //vamos acrear la propiedad donde me mostrara toda la contidad que tendra la vista 

        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; } //va tener la cantidad de registros

    }
}
