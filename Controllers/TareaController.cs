using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;
using Kanban.ViewModels;

public class TareaController: Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITareaRepository _tareaRepository;
    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository,ITableroRepository tableroRepository)
    {
        _tareaRepository = tareaRepository;
        _logger = logger;
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
    }
   
    [HttpGet]
    public IActionResult CrearTarea(int idTablero)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
                return RedirectToAction("Index", "Login");
            
            var tablero = _tableroRepository.ObtenerTablero(idTablero);
            var tareas = _tareaRepository.ListarTareasPorTablero(idTablero);


            ViewData["idTablero"] = idTablero;
            ViewData["NombreTablero"] = tablero.Nombre;
            ViewData["NombrePropietario"] = _usuarioRepository.Detalles(tablero.IdUsuarioPropietario).NombreDeUsuario;
            ViewData["Tareas"] = tareas;
            return View(new CrearTareaViewModel());
        }
        catch (Exception ex )
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"]="No se pudo cargar la vista de Crear Tarea";
            return RedirectToAction("ListarTareasPorTablero", new { idTablero = idTablero });
    
            
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(int idTablero, CrearTareaViewModel tarea)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Datos inválidos para la creación de la tarea.");
                ViewBag.ErrorMessage = "Datos inválidos.";
                 ViewData["idTablero"] = idTablero;
                return View(tarea);
            }
            var tareaNueva = new Tarea();
            tareaNueva.Nombre = tarea.Nombre;
            tareaNueva.Descripcion = tarea.Descripcion;
            tareaNueva.Color = tarea.Color;
            tareaNueva.Estado = tarea.Estado;
            tareaNueva.IdUsuarioAsignado = null;
            tareaNueva.IdTablero = idTablero;
            _tareaRepository.CrearTarea(idTablero, tareaNueva);
            return RedirectToAction("ListarTareasPorTablero", new { idTablero = idTablero });
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo crear la tarea";
            return RedirectToAction("ListarTareasPorTablero", new { idTablero = idTablero });
        }
        
    }
    [HttpGet]
    public IActionResult ListarTareasPorTablero(int idTablero)
    {
        var tablero = _tableroRepository.ObtenerTablero(idTablero);
        var tareas = _tareaRepository.ListarTareasPorTablero(idTablero);

        var tareasViewModel = tareas.Select(t => new ListarTareasViewModel
    {
        Id = t.Id,
        Nombre = t.Nombre,
        Descripcion = t.Descripcion,
        Color = t.Color,
        Estado = t.Estado,
        IdUsuarioAsignado = t.IdUsuarioAsignado,
        NombreUsuarioAsignado = t.IdUsuarioAsignado.HasValue ? _usuarioRepository.Detalles(t.IdUsuarioAsignado.Value).NombreDeUsuario : null
    }).ToList();
        ViewData["IdTablero"] = idTablero;
        ViewData["NombreTablero"] = tablero.Nombre;
        ViewData["IdUsuarioPropietario"] = tablero.IdUsuarioPropietario;
        return View(tareasViewModel);
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
        
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
                return RedirectToAction("Index", "Login");

            var tarea = _tareaRepository.Detalles(id);
            int idUsuarioLogueado = (int)HttpContext.Session.GetInt32("Id");
            if (tarea.IdUsuarioAsignado != idUsuarioLogueado)
            {
                TempData["ErrorMessage"] = "No tienes permiso para modificar esta tarea.";
                return RedirectToAction("ListarTareasPorTablero", new { idTablero = tarea.IdTablero });
            }

            ModificarTareaViewModel modificarTarea = new ModificarTareaViewModel();
           
            modificarTarea.Estado = tarea.Estado;

            return View(modificarTarea);
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo cargar la vista de modificación de tarea";
            return RedirectToAction("Listar","Tablero");
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, ModificarTareaViewModel tarea)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Datos inválidos para la modificación de la tarea.");
                ViewBag.ErrorMessage = "Datos inválidos.";
                return View(tarea);
            }

            var tareaExistente = _tareaRepository.Detalles(id);
            if (tareaExistente == null)
            {
                return NotFound();
            }
            
            int idUsuarioLogueado = (int)HttpContext.Session.GetInt32("Id");

            if (tareaExistente.IdUsuarioAsignado != idUsuarioLogueado)
            {
                TempData["ErrorMessage"] = "No tienes permiso para modificar esta tarea.";
                return RedirectToAction("ListarTareasPorTablero", new { idTablero = tareaExistente.IdTablero });
            }

            Tarea tareaModificada = new Tarea();
            tareaModificada.Estado = tarea.Estado;

            _tareaRepository.ModificarTarea(id, tareaModificada);
            ViewData["idTablero"] = tareaExistente.IdTablero;
            return RedirectToAction("ListarTareasPorTablero", new { idTablero = tareaExistente.IdTablero });
        
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo modificar la tarea";
            return RedirectToAction("Listar","Tablero");
        }
        
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
