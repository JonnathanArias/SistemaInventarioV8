using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;

namespace SistemaInventario.Areas.Admin.Controllers
{
    //vamos aubicarlo al area que pertenece donde pertenece

    [Area("Admin")]
    public class UsuarioController : Controller
    {
        //vamos a traer nuestra area de trabajo 

        private readonly IUnidadTrabajo _unidadTrabajo;

        //vamos a traer los usuarios

        private readonly ApplicationDbContext _db;

        public UsuarioController(IUnidadTrabajo unidadTrabajo, ApplicationDbContext db)
        {
            _unidadTrabajo = unidadTrabajo;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //vamois acrear un API para que nos traiga todos los usuarios

        #region
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos () 
        {
            var usuarioLista = await _unidadTrabajo.UsuarioAplicacion.ObtenerTodos();
            var userRole = await _db.UserRoles.ToListAsync();
            var roles = await _db.Roles.ToListAsync();

            //vamos a recorrer la lista de usuario
            foreach (var usuario in usuarioLista) 
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == usuario.Id).RoleId;
                usuario.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;//aqui traemos desde usuarios Role la que no esta mapeada y creada ala tabla usuarios que esta en usuarioAplicacion.cs

            }
            return Json(new { data = usuarioLista });

        }

        [HttpPost]

        public async Task<IActionResult> BloquearDesbloquear([FromBody] string id) 
        { 
            var usuario = await _unidadTrabajo.UsuarioAplicacion.Obtenerprimero(u=> u.Id == id);
            if (usuario == null) 
            { 
                return Json(new {success = false, Message = "Error de Usuario!"});
            }
            if (usuario.LockoutEnd != null && usuario.LockoutEnd > DateTime.Now) 
            {
                    //estamos trabajando un usuario bloqueado
                usuario.LockoutEnd = DateTime.Now;

            }
            else
            {
                usuario.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            await _unidadTrabajo.Guardar();
            return Json(new { success = true, Message = "Operacion Exitosa" });
        }

        #endregion
    }
}
