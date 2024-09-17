using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio // este va heredar del repositorio de bodega  y Ibodega , nos va generar
        //error incialmente de Bodega repositorio vamos a crear una clase private readonly para traer las variables de DBcontext, ya que nos va pedir
        //el repositorio al  padre 
    {

        //

        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            //vamos a implementa el metodo actualizar el regsitro de marca que se envia ahora vamos a capturarlo 
           

            var productoBD = _db.Productos.FirstOrDefault(b => b.Id == producto.Id);  // aqui estamos capturando el registro antes de actulaizarlo ahora vamos a validar si este es un valor a diferente anulo 
            
                if (productoBD != null) 
                {
                    if(producto.ImagenUrl != null)  //validar la imagen url si el usuario esta actualizando y este es para que se actualize
                    {
                        productoBD.ImagenUrl= producto.ImagenUrl;
                    }

                productoBD.NumeroSerie = producto.NumeroSerie;
                productoBD.Descripcion = producto.Descripcion;
                productoBD.Precio = producto.Precio;
                productoBD.Costo = producto.Costo;
                productoBD.CategoriaId = producto.CategoriaId;
                productoBD.MarcaId = producto.MarcaId;
                productoBD.PadreId = producto.PadreId;
                productoBD.Estado = producto.Estado;


                //realizaremos con el metodo SaveChanges para que actualize los datos en la BD lo registros 
                _db.SaveChanges();

                //el id nos e actualiza 

                    
            
                }
        
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
        {
           if(obj == "Categoria") //consultar el objeto de categorias y trer categorias o si es de marcas
            {
                return _db.Categorias.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                     Text = c.Nombre,
                     Value = c.Id.ToString()
                    
                });


            }

            if (obj == "Marca") //consultar el objeto de cateorias y trer categorias o si es de marcas
            {
                return _db.Marcas.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()

                });
            }

            if (obj == "Producto") //consultar el objeto de cateorias y trer categorias o si es de marcas
            {
                return _db.Productos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()

                });
            }


            return null;
        }

        public Task<IEnumerable<Producto>> ObtenerTodosDropdownLista()
        {
            throw new NotImplementedException();
        }
    }
}
