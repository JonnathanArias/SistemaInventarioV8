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
    public class CategoriaController : Controller
    {
      

        //vamos a referenciar nuestro servicio vamos a instanciarlo de la siguiente manera 

        private readonly IUnidadTrabajo _unidadtrabajo;

        //creamos un contructor

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadtrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
          Categoria categoria = new Categoria();

            if (id == null) 
            { 
            //crear nueva bodega
                categoria.Estado = true;   
                return View(categoria);    
            }
            //actualizacion 
            else
            {
                categoria = await _unidadtrabajo.Categoria.Obtener(id.GetValueOrDefault());
                if(categoria ==  null)
                {
                    return NotFound();
                }
                return View(categoria);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//evitar falsificaciones del sitio cargado de otra pagina que puedan cargar datos a nuestra pagina
        public async Task<IActionResult> Upsert(Categoria categoria) 
        {
            if (ModelState.IsValid) 
            {
                if (categoria.Id== 0) 
                {
                    await _unidadtrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "Categoria Creada Exitosamente";
                }
                else 
                {
                  _unidadtrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoria Actualizada Exitosamente";
                }
                await _unidadtrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al tratar de grabar Categoria";
            return View(categoria);
        }

        #region API

            [HttpGet]
            public async Task<IActionResult> ObtenerTodos() 
        { 
            var todos = await _unidadtrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id) 
        { 
            var categoriaDb = await _unidadtrabajo.Categoria.Obtener(id);
            if(categoriaDb == null) 
            {
                return Json(new {Success = false, message ="Error al borrar Categoria"});
            }
            _unidadtrabajo.Categoria.Remover(categoriaDb);
            await _unidadtrabajo.Guardar();
            return Json(new { Success = true, message = "Categoria Eliminada Exitosamente!" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre,int  id = 0)
        {
                bool valor = false;
                 var lista = await _unidadtrabajo.Categoria.ObtenerTodos();

            if (id == 0) 
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id !=id);
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
