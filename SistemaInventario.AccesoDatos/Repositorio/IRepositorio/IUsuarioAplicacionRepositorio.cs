using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUsuarioAplicacionRepositorio : IRepositorio<UsuarioAplicacion>
    {
        //nos vamos a utilizar el metodo actualizar como los otro IRepositorios ya que .NetIdentity maneja otros procesos en la creacion y actualizacion

        

    }
}
