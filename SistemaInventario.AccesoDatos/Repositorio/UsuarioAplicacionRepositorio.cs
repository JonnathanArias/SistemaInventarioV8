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
    public class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IUsuarioAplicacionRepositorio// este va heredar del repositorio de bodega  y Ibodega , nos va generar
        
    {
        private readonly ApplicationDbContext _db;

        public UsuarioAplicacionRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        //el metodo actualizar no existe aqui por el metodo .net Identity

                    
            
                
        
        


    }
}
