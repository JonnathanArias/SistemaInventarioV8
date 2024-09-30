using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{

    //vamos a colocarle atributo de areas para decirle donde pertenece
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]//este es para que el usuario tenga que autorizarse y que roles tiene esta autorizacion 
    public class BodegaController : Controller
    {
      

        //vamos a referenciar nuestro servicio vamos a instanciarlo de la siguiente manera 

        private readonly IUnidadTrabajo _unidadtrabajo;

        //creamos un contructor

        public BodegaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadtrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
          Bodega bodega = new Bodega();

            if (id == null) 
            { 
            //crear nueva bodega
                bodega.Estado = true;   
                return View(bodega);    
            }
            //actualizacion 
            else
            {
                bodega = await _unidadtrabajo.Bodega.Obtener(id.GetValueOrDefault());
                if(bodega ==  null)
                {
                    return NotFound();
                }
                return View(bodega);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//evitar falsificaciones del sitio cargado de otra pagina que puedan cargar datos a nuestra pagina
        public async Task<IActionResult> Upsert(Bodega bodega) 
        {
            if (ModelState.IsValid) 
            {
                if (bodega.id== 0) 
                {
                    await _unidadtrabajo.Bodega.Agregar(bodega);
                    TempData[DS.Exitosa] = "Bodega Creada Exitosamente";
                }
                else 
                {
                  _unidadtrabajo.Bodega.Actualizar(bodega);
                    TempData[DS.Exitosa] = "Bodega Actualizada Exitosamente";
                }
                await _unidadtrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al tratar de grabar Bodega";
            return View(bodega);
        }

        #region API

            [HttpGet]
            public async Task<IActionResult> ObtenerTodos() 
        { 
            var todos = await _unidadtrabajo.Bodega.ObtenerTodos();
            return Json(new { data = todos });
        
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id) 
        { 
            var bodegaDb = await _unidadtrabajo.Bodega.Obtener(id);
            if(bodegaDb == null) 
            {
                return Json(new {Success = false, message ="Error al borrar Bodega"});
            }
            _unidadtrabajo.Bodega.Remover(bodegaDb);
            await _unidadtrabajo.Guardar();
            return Json(new { Success = true, message = "Bodega Eliminada Exitosamente!" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre,int  id = 0)
        {
                bool valor = false;
                 var lista = await _unidadtrabajo.Bodega.ObtenerTodos();

            if (id == 0) 
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.id !=id);
            }
            if (valor) 
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }
            
        #endregion
    }
}
