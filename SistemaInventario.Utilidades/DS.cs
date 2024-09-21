using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Utilidades
{
    public static class DS
    {
        //vamos acrear 2 constantes para _Notificaciones
        public const string Exitosa = "Exitosa";
        public const string Error = "Error";

        // la ruta donde se guardaran las imagenes para productos 

        public const string ImagenRuta = @"\imagenes\producto\";

        //vamos a detallar todos los roles que se van a utilizar en el sistema de informacion 

        public const string Role_Admin = "Admin"; 
        
        public const string Role_Cliente = "Cliente";//cuando un cliente se registre tendra ese rol de cliente

        public const string Role_Inventario = "Inventario";//es un modulo de completo que van atener usuarios que tenga este rol
    }
}
