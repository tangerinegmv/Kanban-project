namespace Kanban
{
    public enum Rol
        {
            Administrador,
            Operador
        }
    public class Usuario
    {
        private int id;
        private string nombreDeUsuario;
        private string password;
        private Rol rolUsuario;
        public Rol RolUsuario { get => rolUsuario; set => rolUsuario = value; }

        public int Id { get => id; set => id = value; }
        public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public string Password { get => password; set => password = value; }
        
    }
}