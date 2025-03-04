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
        var tableros = tablerosPropios.Union(tablerosAsignados).ToList();

        return View(tableros);
    }

    private int ObtenerUsuarioLogueado()
    {
        return (int)HttpContext.Session.GetInt32("Id");   
  }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new Tablero());
    }
    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero)
    {
        Tablero nuevo = _tableroRepository.CrearTablero(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        return View(_tableroRepository.ObtenerTablero(id));
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, Tablero tablero)
    {
        _tableroRepository.ModificarTablero(id, tablero);
        return RedirectToAction("Index");
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
