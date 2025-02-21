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

    [HttpGet]   
    public IActionResult Detalles(int id)
    {
        var tarea = tareaRepository.Detalles(id);
        return View(tarea);
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        
        var tarea = tareaRepository.Detalles(id);
        return View(tarea);
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, Tarea tarea)
    {
        var tareaExistente = tareaRepository.Detalles(id);
        
        if (tareaExistente == null)
        {
            return NotFound(); 
        }

        tarea.IdTablero = tareaExistente.IdTablero;

        tareaRepository.ModificarTarea(id, tarea);

        return RedirectToAction("ListarTareasPorTablero", new { idTablero = tarea.IdTablero });
    }

    [HttpGet]
    public IActionResult EliminarTarea(int id)
    {
        var tarea = tareaRepository.Detalles(id);
        return View(tarea);
    }
    [HttpPost]
    public IActionResult EliminarTarea(int id, Tarea tarea)
    {
        var tareaExistente = tareaRepository.Detalles(id);
        if (tareaExistente == null)
        {
            return NotFound();
        }
        tareaRepository.EliminarTarea(id);
        return RedirectToAction("ListarTareasPorTablero", new { idTablero = tareaExistente.IdTablero });
    }
}
