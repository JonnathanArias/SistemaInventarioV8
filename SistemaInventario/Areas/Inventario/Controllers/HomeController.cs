using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.Especificaciones;
using SistemaInventario.Modelos.ViewModels;
using System.Diagnostics;

namespace SistemaInventario.Areas.Inventario.Controllers
{
    [Area("Inventario")] //para cada controlador colocarle aque area pertenece AREA para este para un controlador especifico
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index(int pageNumber = 1, string busqueda="",string busquedaActual="")
        {
            if (!String.IsNullOrEmpty(busqueda)) //aqui vamos hacer la validacion del filtro de los productos
            {
                pageNumber = 1;         
             }
              else
            { 
                busqueda = busquedaActual;
            }
            ViewData["BusquedaActual"] = busqueda;

            if (pageNumber < 1) { pageNumber = 1; }
            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 4 // aqui puedo modificar la cantidad de productos que quiero que se muestre en el index puedo colocar 4 8 10 los que se necesite
            };
                

           var resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros);

            if (!String.IsNullOrEmpty(busqueda)) 
            {
                resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros, p => p.Descripcion.Contains(busqueda));//si son varios productos me lo va colocar en la lista
            }
            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";// este metodo nos ayuda a deshabilitar el boton de la clase Css para desactivar
            ViewData["Siguiente"] = ""; // este es para que el boton ed 
            if (pageNumber > 1) { ViewData["Previo"] = ""; } // este solo le quitamos ese disable al ser mayor que 1 se inhabilita 
            if(resultado.MetaData.TotalPages <= pageNumber) { ViewData["Siguiente"] = "disabled"; }

            return View(resultado);

            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
