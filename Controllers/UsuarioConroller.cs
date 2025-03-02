using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    //private _usuarioRepository usuarioRepository = new UsuarioRepository();
    private readonly IUsuarioRepository _usuarioRepository;


    public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }
   

    public IActionResult Index()
    {
        return View(_usuarioRepository.ListarUsuarios());
    }
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        _usuarioRepository.CrearUsuario(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        return View(_usuarioRepository.Detalles(id));
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, Usuario usuario)
    {
        _usuarioRepository.ModificarUsuario(id, usuario);
        return RedirectToAction("Index");
    }
    
    [HttpGet]   
    public IActionResult EliminarUsuario(int id)
    {
        return View(_usuarioRepository.Detalles(id));
    }
    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        _usuarioRepository.EliminarUsuario(usuario.Id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult CambiarPassword(int id)
    {
        return View(_usuarioRepository.Detalles(id));
    }
    [HttpPost]
    public IActionResult CambiarPassword(int id, Usuario usuario)
    {
        _usuarioRepository.CambiarPassword(id, usuario);
        return RedirectToAction("Index");
    }
}