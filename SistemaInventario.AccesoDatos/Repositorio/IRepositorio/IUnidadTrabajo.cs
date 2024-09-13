using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable // este nos apoyara en deshacernos de cualquier recurso el sistema
                                                  // objetos que no se estaran utilizando para no consumir servcios innecesarios con IDisposable 
    {
        //vamos a traer todos los repositorios que hemos creado IBodegaRepositorio Bodega {get;} solo el get ya que aqui no actualizaremos 

        IBodegaRepositorio Bodega {  get; }
        //vamos a detallar el metodo guardar que va se rmetodo Asyncrono que se hara cargo de guardar los cambios y para que quede guardado 

        //en todo el area de trabajo y lo podamos llamar en otro formularios en cualquier momento 

        Task Guardar();
    }
}
