using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //private TableroRepository tableroRepository = new TableroRepository();
    private readonly ITableroRepository _tableroRepository;
    public TableroController(ILogger<HomeController> logger, ITableroRepository tableroRepository)
    {
        _tableroRepository = tableroRepository;
        _logger = logger;
    }
   

    public IActionResult Index()
    {
        return View(_tableroRepository.ListarTableros());
    }

   [HttpGet]
    public IActionResult ListarTablerosPorUsuario(int idUsuario)
    {
        var tableros = _tableroRepository.ListarTablerosPorUsuario(idUsuario);
         if (tableros == null)
            {
                return NotFound();
            }
            return View(tableros);
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
