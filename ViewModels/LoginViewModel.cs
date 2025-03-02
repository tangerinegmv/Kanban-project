using Kanban;
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    private string nombreDeUsuario;
    private string password;
    private string errorMessage;
    private bool isAuthenticated;

    [Required]
    [StringLength(100, ErrorMessage = "El nombre de usuario no debe exceder los 100 caracteres.")]
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }

    [Required]
    [StringLength(100, ErrorMessage = "La contraseña no debe superar los 100 caracteres")]
    public string Password { get => password; set => password = value; }
    public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
    public bool IsAuthenticated { get => isAuthenticated; set => isAuthenticated = value; }
}
//falta isautenticated del proyecto de fede