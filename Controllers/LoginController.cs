using SQLitePCL;
using Kanban;
using Kanban.ViewModels;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;

public class LoginController:Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        this._logger = logger;
        this._usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
            {
                var model = new LoginViewModel
                {
                    IsAuthenticated = HttpContext.Session.GetString("IsAuthenticated") == "true"
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Tablero");
            }
            
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Error al cargar la página";
            return View("Index");
        }
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        try
        {

            if (string.IsNullOrEmpty(model.NombreDeUsuario) || string.IsNullOrEmpty(model.Password))
            {
                model.ErrorMessage = "Por favor debe ingresar su usuario y contraseña.";
                return View("Index", model);
            }

            Usuario usuario = _usuarioRepository.GetUsuarioNombre(model.NombreDeUsuario);
            if (usuario == null || usuario.Password != model.Password)
            {
                _logger.LogInformation("El usuario " + model.NombreDeUsuario + " introdujo mal los datos.");
                model.ErrorMessage = "Usuario o contraseña incorrectos.";
                return View("Index", model);
            }
            HttpContext.Session.SetString("IsAuthenticated", "true");
            HttpContext.Session.SetString("Usuario", usuario.NombreDeUsuario);
            HttpContext.Session.SetString("Rol", usuario.RolUsuario.ToString());
            HttpContext.Session.SetInt32("Id", usuario.Id);
            _logger.LogInformation("El usuario " + usuario.NombreDeUsuario + " ha iniciado sesión.");
            return RedirectToAction("Index", "Tablero");
        
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Intento de acceso invalido - Usuario: " + model.NombreDeUsuario + " | Clave ingresada: " + model.Password);
            _logger.LogError(ex.ToString());
            model.ErrorMessage = "Credenciales invalidas.";
            return View("Index", model);
            
        }
    }
            
           
}