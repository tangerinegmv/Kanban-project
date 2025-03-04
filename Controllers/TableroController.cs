using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;
using Kanban.ViewModels;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    //private TableroRepository tableroRepository = new TableroRepository();
    private readonly ITableroRepository _tableroRepository;
    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository)
    {
        _tableroRepository = tableroRepository;
        _logger = logger;
    }
   

    // public IActionResult Index()
    // {
    //     return View(_tableroRepository.ListarTableros());
    // }

   //[HttpGet]
    public IActionResult Listar()
    {
        int idUsuario = ObtenerUsuarioLogueado(); // Método para obtener el ID del usuario logueado

        List<ListarTablerosViewModel> tablerosPropios = _tableroRepository.ListarTablerosPorUsuario(idUsuario);
        List<ListarTablerosViewModel> tablerosAsignados = _tableroRepository.ObtenerPorUsuarioAsignado(idUsuario);

        // Unir ambas listas sin duplicados
        var tableros = tablerosPropios
            .Concat(tablerosAsignados)
            .GroupBy(t => t.Id)
            .Select(g => g.First())
            .ToList();
        return View(tableros);
    }

    public IActionResult ListarTablerosPorUsuario(int idUsuario)
    {
        return View(_tableroRepository.ListarTablerosPorUsuario(idUsuario));
    }

    private int ObtenerUsuarioLogueado()
    {
        return (int)HttpContext.Session.GetInt32("Id");   
  }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        try
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != null)
            {
                return View(new CrearTableroViewModel());
            }
            else
            {
                return RedirectToAction("Listar", "Tablero");
            }
            
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo cargar el formulario de creación de tablero";
            return RedirectToAction("Listar");
        }
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tablero)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                 _logger.LogError("Datos inválidos para la creación del tablero.");
                ViewBag.ErrorMessage = "Datos inválidos.";
                return View(tablero);
            }
            int? idUsuario = ObtenerUsuarioLogueado();
            if (idUsuario == null)
            {
                _logger.LogWarning("Intento de crear un tablero sin usuario logueado.");
                TempData["ErrorMessage"] = "Debes iniciar sesión para crear un tablero.";
                return RedirectToAction("Index", "Login");
            }
            var tableroNuevo = new Tablero();
            tableroNuevo.IdUsuarioPropietario = idUsuario.Value;
            tableroNuevo.Nombre = tablero.Nombre;
            tableroNuevo.Descripcion = tablero.Descripcion;
            Tablero nuevo = _tableroRepository.CrearTablero(tableroNuevo);
            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo crear el tablero";
            return RedirectToAction("Listar");
        }
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        try
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != null)
            {
                Tablero tablero = _tableroRepository.ObtenerTablero(id);
                ModificarTableroViewModel modificarTablero = new ModificarTableroViewModel();
                
                modificarTablero.Nombre = tablero.Nombre;
                modificarTablero.Descripcion = tablero.Descripcion;
                return View(modificarTablero);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo cargar el formulario de modificación de tablero";
            return RedirectToAction("Listar");
        }
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, ModificarTableroViewModel tablero)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Datos inválidos para la modificación del tablero.");
                ViewBag.ErrorMessage = "Datos inválidos.";
                return View(tablero);
            }
            
            Tablero modificado = new Tablero();
            modificado.Id = id;
            modificado.Nombre = tablero.Nombre;
            modificado.Descripcion = tablero.Descripcion;
            _tableroRepository.ModificarTablero(id, modificado);
            return RedirectToAction("Listar");
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo modificar el tablero";
            return RedirectToAction("Listar");
            
        }
    }

    [HttpGet]
    public IActionResult EliminarTablero(int id)
    {
        return View(_tableroRepository.ObtenerTablero(id));
    }
    [HttpPost]
    public IActionResult EliminarTablero(Tablero tablero)
    {
        _tableroRepository.EliminarTablero(tablero.Id);
        return RedirectToAction("Index");
    }
    
}
