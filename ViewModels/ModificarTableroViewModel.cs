using Kanban;
using System.ComponentModel.DataAnnotations;

public class ModificarTableroViewModel
{
    private string nombre;
    private string descripcion;

    [StringLength(100, ErrorMessage = "El nombre de tablero no debe exceder los 100 caracteres.")]
    public string Nombre { get => nombre; set => nombre = value; }
    
    [StringLength(255, ErrorMessage = "La descripcion no debe exceder los 255 caracteres.")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
}