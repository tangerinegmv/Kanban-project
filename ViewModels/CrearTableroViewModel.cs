using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kanban.ViewModels
{
    public class CrearTableroViewModel
    {
        [Required(ErrorMessage ="Campo obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Campo obligatorio.")]

        public string Descripcion { get; set; }

        public int? IdUsuarioPropietario { get; set; }

        
    }
}