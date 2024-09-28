using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Utilidades
{
   public class ErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresLower()
        {
            //vamos a crear nuestras propias reglas 
            return new IdentityError()
            {
                    Code = nameof(PasswordRequiresLower),
                    Description = "El password debe tener al menos una Minuscula valida"
            };
        }
    }
}
