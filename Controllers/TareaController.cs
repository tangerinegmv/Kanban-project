using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;

public class TareaController: Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITareaRepository _tareaRepository;
    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository)
    {
        _tareaRepository = tareaRepository;
        _logger = logger;
        _usuarioRepository = usuarioRepository;
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
        Tarea nueva = _tareaRepository.CrearTarea(idTablero, tarea);
        return RedirectToAction("ListarTareasPorTablero", new { idTablero = idTablero });
    }
    [HttpGet]
    public IActionResult ListarTareasPorTablero(int idTablero)
    {
        var tareas = _tareaRepository.ListarTareasPorTablero(idTablero);
        ViewData["IdTablero"] = idTablero;
        return View(tareas);
    }
    [HttpGet]
    public IActionResult ListarTareasPorUsuario(int idUsuario)
    {
        var tareas = _tareaRepository.ListarTareasPorUsuario(idUsuario);
        ViewData["IdUsuario"] = idUsuario;
        return View(tareas);
    }

    [HttpGet]   
    public IActionResult Detalles(int id)
    {
        var tarea = _tareaRepository.Detalles(id);
        return View(tarea);
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        
        var tarea = _tareaRepository.Detalles(id);
        return View(tarea);
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, Tarea tarea)
    {
        var tareaExistente = _tareaRepository.Detalles(id);
        
        if (tareaExistente == null)
        {
            return NotFound(); 
        }

        tarea.IdTablero = tareaExistente.IdTablero;

        _tareaRepository.ModificarTarea(id, tarea);

        return RedirectToAction("ListarTareasPorTablero", new { idTablero = tarea.IdTablero });
    }

    [HttpGet]
    public IActionResult EliminarTarea(int id)
    {
        var tarea = _tareaRepository.Detalles(id);
        return View(tarea);
    }
    [HttpPost]
    public IActionResult EliminarTarea(int id, Tarea tarea)
    {
        var tareaExistente = _tareaRepository.Detalles(id);
        if (tareaExistente == null)
        {
            return NotFound();
        }
        _tareaRepository.EliminarTarea(id);
        return RedirectToAction("ListarTareasPorTablero", new { idTablero = tareaExistente.IdTablero });
    }
    [HttpGet]
    public IActionResult Asignar(int idTarea)
    {
        var usuarios = _usuarioRepository.ListarUsuarios(); 
        ViewBag.IdTarea = idTarea;
        return View(usuarios);
    }

    [HttpPost]
    public IActionResult Asignar(int idTarea, int idUsuario)
    {
        _tareaRepository.AsignarUsuario(idTarea, idUsuario);
        return RedirectToAction("Listar","Tablero"); // Redirigir a la lista de tareas
    }

}
