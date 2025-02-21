using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;

public class TareaController: Controller
{
    private readonly ILogger<HomeController> _logger;
    private TareaRepository tareaRepository = new TareaRepository();
    public TareaController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult CrearTarea(int idTablero)
    {
        ViewData["IdTablero"] = idTablero;
        return View(new Tarea());
    }
    [HttpPost]
    public IActionResult CrearTarea(int idTablero, Tarea tarea)
    {
        //ViewData["idTablero"] = idTablero;
        Tarea nueva = tareaRepository.CrearTarea(idTablero, tarea);
        return RedirectToAction("ListarTareasPorTablero", new { idTablero = idTablero });
    }
    [HttpGet]
    public IActionResult ListarTareasPorTablero(int idTablero)
    {
        var tareas = tareaRepository.ListarTareasPorTablero(idTablero);
        ViewData["IdTablero"] = idTablero;
        return View(tareas);
    }
}