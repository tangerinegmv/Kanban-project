using Kanban;
using System.ComponentModel.DataAnnotations;

public class ListarTablerosViewModel
{
    private int id;
    private string nombreUsuarioPropietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public string NombreUsuarioPropietario { get => nombreUsuarioPropietario; set => nombreUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
}