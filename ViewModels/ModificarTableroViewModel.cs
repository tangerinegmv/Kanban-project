namespace Kanban.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ModificarTableroViewModel
{
    private string nombre;
    private string descripcion;

    [Required(ErrorMessage = "El nombre del tablero es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre de tablero no debe exceder los 100 caracteres.")]
    public string Nombre { get => nombre; set => nombre = value; }
    
    [Required(ErrorMessage = "La descripción del tablero es obligatoria.")]
    [StringLength(255, ErrorMessage = "La descripcion no debe exceder los 255 caracteres.")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
}