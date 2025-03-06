using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kanban.ViewModels
{
    public class ModificarTareaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la tarea es obligatorio.")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Color { get; set; }

        [Required(ErrorMessage = "El estado de la tarea es obligatorio.")]
        public EstadoTarea Estado { get; set; }

        public int? IdUsuarioAsignado { get; set; }

        
    }
}