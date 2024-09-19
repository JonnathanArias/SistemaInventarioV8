using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    //vamos a declarar que sea una clase public
    //como creamos las clases en IRepositorio que va hacer generica ella va heredar esas mismas funciones aqui en este mismo de la siguiente manera ya qu evamso autilizar los mismos parametros

    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        //aqui se llamo todas las clases que realizamos en IRepositorio ya que al generar el error importamos todas estas que nos trajo del otro formulario de la clase generica IRepositorio

        //vamos acrear una variable DBContext y le damos un nombre _db

        private readonly ApplicationDbContext _db;

        //vamos a necesitar una propiedad DbSet<T> con el tipo de objeto que vamos a trabajar 

        internal DbSet<T> dbSet;

        //Ahora vamos acrear un constructor para  inicializar la propiedad DbContext y la propiedad DbSet para hacer referencia del objeto que vamos a trabajar 

        public Repositorio(ApplicationDbContext db)
        {
            //vamos a recibir los parametros T y asi asignarlos 
            _db = db;
            //el this es para diferenciarlo del original 
            this.dbSet = _db.Set<T>();
        }

        //como dejamos en la clase IRepositario como asincrono aqui los vamos agregar listos  de la siguiente manera se agrega async y vamos agregar el await dentro de la implementacion 
        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);//esto es equivalente  a un insert into table
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id); // este solo va trabajar con select * from y solo trabaja con el id (Solo por Id )
        }


        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            //vamos a crear un if por si el filtro es ! a null debemos filtrar nuestr query 
            if(filtro != null) 
            {
                query = query.Where(filtro); // select para seleccionarlo y el where es para filtralo para alguna propiedad que estamos utilizando 
            }
            //ahora vamos a incluir propiedades es una cadena de caracteres, vamos avreificar si estamos recibiendo valores es para los include 

            if (incluirPropiedades != null)
            {
                //vamos a recorrer esa cadena de caracteres 
                foreach (var incluirProp in incluirPropiedades.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) 
                {
                //al realizar el recorrido del foreach va incluirme las propiedades de los objetos

                    query = query.Include(incluirProp); // ejemplo  "Categoria, Marca "
                }
            }
            if(orderBy != null) 
            {
                query = orderBy(query);
            }
            if (!isTracking) 
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public PagedList<T> ObtenerTodosPaginado(Parametros paramentros, Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            //vamos a crear un if por si el filtro es ! a null debemos filtrar nuestr query 
            if (filtro != null)
            {
                query = query.Where(filtro); // select para seleccionarlo y el where es para filtralo para alguna propiedad que estamos utilizando 
            }
            //ahora vamos a incluir propiedades es una cadena de caracteres, vamos avreificar si estamos recibiendo valores es para los include 

            if (incluirPropiedades != null)
            {
                //vamos a recorrer esa cadena de caracteres 
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //al realizar el recorrido del foreach va incluirme las propiedades de los objetos

                    query = query.Include(incluirProp); // ejemplo  "Categoria, Marca "
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return PagedList<T>.ToPagedList(query, paramentros.PageNumber,paramentros.PageSize);
        }

        //vamos acopira y pegar de Obtener todos a Obtenerprimero pero no va orderby y vamos a modificar return await query.ToListAsync(); a  este return await query.FirstOrDefaultAsync(); retorna un solo elemento 
        public async Task<T> Obtenerprimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            //vamos a crear un if por si el filtro es ! a null debemos filtrar nuestr query 
            if (filtro != null)
            {
                query = query.Where(filtro); // select para seleccionarlo y el where es para filtralo para alguna propiedad que estamos utilizando 
            }
            //ahora vamos a incluir propiedades es una cadena de caracteres, vamos avreificar si estamos recibiendo valores es para los include 

            if (incluirPropiedades != null)
            {
                //vamos a recorrer esa cadena de caracteres 
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //al realizar el recorrido del foreach va incluirme las propiedades de los objetos

                    query = query.Include(incluirProp); // ejemplo  "Categoria, Marca "
                }
            }
            
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }


        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);// este va realizar un delete en la BD 
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);//este eliminara un rango de una lista de tipo objeto y lo va eliminar 
        }

       
    }
}
