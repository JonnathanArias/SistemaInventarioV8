using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{

    //vamos a colocarle atributo de areas para decirle donde pertenece
    [Area("Admin")]
    public class MarcaController : Controller
    {
      

        //vamos a referenciar nuestro servicio vamos a instanciarlo de la siguiente manera 

        private readonly IUnidadTrabajo _unidadtrabajo;

        //creamos un contructor

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadtrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
          Marca marca = new Marca();

            if (id == null) 
            { 
            //crear nueva marca
                marca.Estado = true;   
                return View(marca);    
            }
            //actualizacion 
            else
            {
                marca = await _unidadtrabajo.Marca.Obtener(id.GetValueOrDefault());
                if(marca ==  null)
                {
                    return NotFound();
                }
                return View(marca);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//evitar falsificaciones del sitio cargado de otra pagina que puedan cargar datos a nuestra pagina
        public async Task<IActionResult> Upsert(Marca marca) 
        {
            if (ModelState.IsValid) 
            {
                if (marca.Id== 0) 
                {
                    await _unidadtrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca Creada Exitosamente";
                }
                else 
                {
                  _unidadtrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca Actualizada Exitosamente";
                }
                await _unidadtrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al tratar de grabar Marca";
            return View(marca);
        }

        #region API

            [HttpGet]
            public async Task<IActionResult> ObtenerTodos() 
        { 
            var todos = await _unidadtrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });
        
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id) 
        { 
            var marcaDb = await _unidadtrabajo.Marca.Obtener(id);
            if(marcaDb == null) 
            {
                return Json(new {Success = false, message ="Error al borrar Marca"});
            }
            _unidadtrabajo.Marca.Remover(marcaDb);
            await _unidadtrabajo.Guardar();
            return Json(new { Success = true, message = "Marca Eliminada Exitosamente!" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre,int  id = 0)
        {
                bool valor = false;
                 var lista = await _unidadtrabajo.Marca.ObtenerTodos();

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
