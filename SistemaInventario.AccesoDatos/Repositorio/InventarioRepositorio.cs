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
    public class BodegaProductoRepositorio : Repositorio<BodegaProducto>, IBodegaProductoRepositorio // este va heredar del repositorio de bodega  y Ibodega , nos va generar
        //error incialmente de Bodega repositorio vamos a crear una clase private readonly para traer las variables de DBcontext, ya que nos va pedir
        //el repositorio al  padre 
    {

        //

        private readonly ApplicationDbContext _db;

        public BodegaProductoRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Actualizar(BodegaProducto bodegaProducto)
        {
            //vamos a implementa el metodo actualizar el regsitro de marca que se envia ahora vamos a capturarlo 
           

            var bodegaProductoBD = _db.BodegaProductos.FirstOrDefault(b => b.Id == bodegaProducto.Id);  // aqui estamos capturando el registro antes de actulaizarlo ahora vamos a validar si este es un valor a diferente anulo 
            //cuando se incremenre la cantidad 
                if (bodegaProductoBD != null) 
                {

                bodegaProductoBD.Cantidad = bodegaProducto.Cantidad;

                //realizaremos con el metodo SaveChanges para que actualize los datos en la BD lo registros 
                _db.SaveChanges();

                //el id nos e actualiza 

                    
            
                }
        
        }

       
    }
}
