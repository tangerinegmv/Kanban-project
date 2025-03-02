using System.ComponentModel.DataAnnotations;
using Kanban;
public class CrearTableroViewModel
{
  private int idUsuarioPropietario;
  private string nombre;
  private string descripcion;
  [Required]
  public int IdUsuarioPropietario { get => this.idUsuarioPropietario; set => this.idUsuarioPropietario = value; }
  
  [Required]
  [StringLength(100, ErrorMessage = "El nombre de tablero no debe exceder los 100 caracteres.")]
  public string Nombre { get => nombre; set => nombre = value; }
  [StringLength(255, ErrorMessage = "La descripcion no debe exceder los 255 caracteres.")]
  public string Descripcion { get => descripcion; set => descripcion = value; }

}