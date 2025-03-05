namespace Kanban.ViewModels;
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    private string nombreDeUsuario;
    private string password;
    private string errorMessage;
    private bool isAuthenticated;

    [Required(ErrorMessage ="El usuario es obligatorio.")]
    
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }

    [Required(ErrorMessage ="La contraseña es obligatoria.")]
   
    public string Password { get => password; set => password = value; }
    public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
    public bool IsAuthenticated { get => isAuthenticated; set => isAuthenticated = value; }
}
//falta isautenticated del proyecto de fede