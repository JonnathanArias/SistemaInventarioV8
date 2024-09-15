using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IMarcaRepositorio : IRepositorio<Marca>
    {
        //vamos acrear el metodo actualizar para todos lso repositorios que realizien los metodos , manejarlo de manera individuales

        void Actualizar(Marca marca);

    }
}
