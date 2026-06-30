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
    public class KardexInventarioRepositorio : Repositorio<KardexInventario>, IKardexInventarioRepositorio
        // este va heredar del repositorio de bodega  y Ibodega , nos va generar
                                                                                                       //error incialmente de Bodega repositorio vamos a crear una clase private readonly para traer las variables de DBcontext, ya que nos va pedir
                                                                                                        //el repositorio al  padre 
    {

        //

        private readonly ApplicationDbContext _db;

        public KardexInventarioRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

       
        

       
    }
}
