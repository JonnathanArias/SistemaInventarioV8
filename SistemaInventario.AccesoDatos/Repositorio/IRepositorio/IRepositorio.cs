using SistemaInventario.Modelos.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class // este va recibir cualquier tipo de objeto de bodega, productor categorias el T es para volverlo enerico para que T sea una clase
    {
        //vamos a detallar todos los metodos que vamos a utilizar ya que esta es uan interfaz generica 


        //vamos a crear un metodo 

        //vamos acrearlo asincrono con Task<T> y los mismo para 
      Task <T> Obtener(int id); //este es un objeto T que va recibir un id 


        //vamos acrearlo asincrono con  Task<IEnumerable<T>> 
        Task<IEnumerable<T>> ObtenerTodos( // este es una lista segun T (va recibir los parametros)
            Expression<Func<T, bool>> filtro = null,//filtro que nos va traer 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true// esto es cuando queramos acxceder a un objeto y al mismo tiempo queramos modificarlo es para esoo es el isTracking 


            );

        //vamos a crear la declaracion del metodo del PageList 

        PagedList<T> ObtenerTodosPaginado(Parametros paramentros, Expression<Func<T, bool>> filtro = null,//filtro que nos va traer 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true); // este metodo nos va traer todo paginado 

        //creacion de otro metodo que no nos retorne una lista si no que nos retorne un objeto segun el filtro 

        //vamos acrearlo asincrono con  Task<IEnumerable<T>> 
        Task<T> Obtenerprimero(// este va tener los mismos parametros del primer metodo de T Obtener a excepcion del orderby, ya que solo nos va retornar un solo registro no necesitamos organizar
             Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true

            );

        //vamos a crear un void este va realizar un nuevo registro la bases de datos y para dejar asincrono Agregar se cambia el void por Task

        Task Agregar(T entidad);

        //vamos a crear un void remover la entidad que queramos remover  atraves de entity framework core de la BD 

        // estas 2 ultimaos Void no se pueden dejar asincronos 

        void Remover(T entidad);

        //vamos a crear otro RemoverRango este nos funciona para eliminar uno o varios 

        void RemoverRango(IEnumerable<T> entidad);


    }
}
