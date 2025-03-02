using Kanban;
using System.ComponentModel.DataAnnotations;

public class ListarUsuariosViewModel
{
    private string usuario;
    private Rol rolUsuario;

    public string Usuario { get => usuario; set => usuario = value; }
    public Rol RolUsuario { get => rolUsuario; set => rolUsuario = value; }
}