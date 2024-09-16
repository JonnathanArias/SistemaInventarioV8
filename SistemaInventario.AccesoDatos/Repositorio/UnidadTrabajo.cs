using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo //Ctrl . para que se implemenete lso metodos que se realizaron IUnidad de trabajo heredara
    {
        //vamos acrear una propiedad DBContext por que para cada uno de los repositorios vamos atener que pasar el Dbcontext para que asi lo pase a su padre 

        private readonly ApplicationDbContext _db;
        //vamos acrear una propiedad llamada  IBodegaRepositorio Bodega 

        public IBodegaRepositorio Bodega { get; private set; }
        public ICategoriaRepositorio Categoria{ get; private set; }

        public IMarcaRepositorio Marca { get; private set; }

        public IProductoRepositorio Producto { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            //vamos a incicalizar Bodega 

            Bodega = new BodegaRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
           
        }


        public void Dispose()
        {

            //traemos Dispose para que libere todo 
            _db.Dispose();
        }

        public  async Task Guardar() //vamos adejarlo global para podelo utilizarlo en todo los formularios lo dejaremos async
        {
            await _db.SaveChangesAsync();
        }
    }
}
