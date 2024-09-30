using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewModels;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{

    //vamos a colocarle atributo de areas para decirle donde pertenece
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin + "," +DS.Role_Inventario)]//este es para que el usuario tenga que autorizarse y que roles tiene esta autorizacion 
    public class ProductoController : Controller
    {
      

        //vamos a referenciar nuestro servicio vamos a instanciarlo de la siguiente manera 

        private readonly IUnidadTrabajo _unidadtrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

       // public IEnumerable<SelectListItem> CategoriaLista { get; private set; }
        //public IEnumerable<SelectListItem> MarcaLista { get; private set; }

        //creamos un contructor

        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadtrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id, Producto producto) 
        {

            //vamos a llamar la vista que relizamos en ProductoRepositorio.cs para que nos llene la lista 

            ProductoVM productoVM = new ProductoVM() //inicializamos el el objeto para utilizar CategoriaLista y MarcaLista
            {
                Producto = new Producto(),
                CategoriaLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Categoria"),
                MarcaLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Marca"),
                PadreLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Producto")

            };

            //vamos a validar si vamos a ingresar un nuevo producto o actualizar debemso traer los datos 

            if(id == null) 
            {
                //Crear nuevo producto
                productoVM.Producto.Estado = true;// nuevo registro de producto
                return View(productoVM);// me trae todas las listas 
            }
            else 
            { 
                productoVM.Producto = await _unidadtrabajo.Producto.Obtener(id.GetValueOrDefault());

                //validar si este producto existe 
                if(productoVM.Producto == null) 
                {
                    return NotFound();
                }

                return View(productoVM);
            }

          
            
        }

        [HttpPost]

        public async Task<IActionResult> Upsert(ProductoVM productoVM) 
        {
            if (ModelState.IsValid) 
            { 
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productoVM.Producto.Id == 0) 
                {
                    //Crear 
                    string upload = webRootPath + DS.ImagenRuta;//guarda imagen producto
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);//esa imagen fisica la vamos almacenar en espacio en memoria en directorio 
                    }

                    productoVM.Producto.ImagenUrl = fileName + extension;
                    await _unidadtrabajo.Producto.Agregar(productoVM.Producto);//para que se agregue este producto caso crear 

                }
                //actualizar el producto
                else 
                {
                    //si vamso a eliminar es para que se actualzie la imagen
                     var objProducto = await _unidadtrabajo.Producto.Obtenerprimero(p => p.Id == productoVM.Producto.Id, isTracking:false);
                    if (files.Count > 0) // Si se carga una nueva imagen para el producto existente 
                    {
                        string upload = webRootPath + DS.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension (files[0].FileName);

                        //borrar imagen anterior antes de 
                         var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile) )
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create)) 
                        {
                            files[0].CopyTo (fileStream);
                        }
                        productoVM.Producto.ImagenUrl = fileName + extension;
                    }//caso  de que no se cargue una imagen nueva queda con la imagen anterior
                    else 
                    {
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _unidadtrabajo.Producto.Actualizar(productoVM.Producto);
                }
                TempData[DS.Exitosa] = "Trasaccion exitosa!";// traemos la variable constante de DS para que nos muestre el mensaje 
                await _unidadtrabajo.Guardar(); // guardar el registro
                return View("Index");

            }// si algo falla por alguna razon If not Valid
            productoVM.CategoriaLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Categoria");
            productoVM.MarcaLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Marca");
            productoVM.PadreLista = _unidadtrabajo.Producto.ObtenerTodosDropdownLista("Producto");
            return View(productoVM);
        }



        

        #region API

            [HttpGet]
            public async Task<IActionResult> ObtenerTodos() 
        { 
            var todos = await _unidadtrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos });
        
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id) 
        { 
            var productoDb = await _unidadtrabajo.Producto.Obtener(id);
            if(productoDb == null) 
            {
                return Json(new {Success = false, message ="Error al borrar Producto"});
            }

            //realizaremos la eliminacion de la imagen justo al eliminar el producto, remover imagen 
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorFile = Path.Combine(upload, productoDb.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }

            _unidadtrabajo.Producto.Remover(productoDb);
            await _unidadtrabajo.Guardar();
            return Json(new { Success = true, message = "Producto Eliminado Exitosamente!" });
        }

        [ActionName("ValidarSerie")]
        public async Task<IActionResult> ValidarSerie(string serie,int  id = 0) // este es para que no se repita el numero de serie
        {
                bool valor = false;
                 var lista = await _unidadtrabajo.Producto.ObtenerTodos();

            if (id == 0) 
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim() && b.Id !=id);
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
