using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private TableroRepository tableroRepository = new TableroRepository();

    public TableroController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(tableroRepository.ListarTableros());
    }

   [HttpGet]
    public IActionResult ListarTablerosPorUsuario(int idUsuario)
    {
        var tableros = tableroRepository.ListarTablerosPorUsuario(idUsuario);
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
        tableroRepository.CrearTablero(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        return View(tableroRepository.ObtenerTablero(id));
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, Tablero tablero)
    {
        tableroRepository.ModificarTablero(id, tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EliminarTablero(int id)
    {
        return View(tableroRepository.ObtenerTablero(id));
    }
    [HttpPost]
    public IActionResult EliminarTablero(Tablero tablero)
    {
        tableroRepository.EliminarTablero(tablero.Id);
        return RedirectToAction("Index");
    }
    
}
