﻿using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio // este va heredar del repositorio de bodega  y Ibodega , nos va generar
        //error incialmente de Bodega repositorio vamos a crear una clase private readonly para traer las variables de DBcontext, ya que nos va pedir
        //el repositorio al  padre 
    {

        //

        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Actualizar(Bodega bodega)
        {
            //vamos a implementa el metodo actualizar el regsitro de bodega que se envia ahora vamos a capturarlo 
           

            var bodegaBD = _db.Bodegas.FirstOrDefault(b => b.id == bodega.id);  // aqui estamos capturando el registro antes de actulaizarlo ahora vamos a validar si este es un valor a diferente anulo 
            
                if (bodegaBD != null) 
                {
                    bodegaBD.Nombre = bodega.Nombre;
                    bodegaBD.Descripcion = bodega.Descripcion;
                    bodegaBD.Estado = bodega.Estado;
                //realizaremos con el metodo SaveChanges para que actualize los datos en la BD lo registros 
                    _db.SaveChanges();

                //el id nos e actualiza 

                    
            
                }
        
        }


    }
}
