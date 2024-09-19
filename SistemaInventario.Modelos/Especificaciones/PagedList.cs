using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.Especificaciones
{
    public class PagedList<T>  : List<T>//vamos a dejar que esta clase sea generica que se pueda utilizar 
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize) // acerca de los valores decimales a enteros de 1.5 a  2
            };
            AddRange(items); //Agrega los elemento de la coleccion a la final de cada lista
        }


        public static PagedList<T> ToPagedList(IEnumerable<T> entidad, int pageNumber, int pageSize)
        { 
            var count = entidad.Count ();
            var items = entidad.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); 
            return new PagedList<T> (items, count, pageNumber, pageSize);
            
        }
    }
}
