using Kanban;

public interface IUsuarioRepository
{
    public void CrearUsuario(Usuario usuario);
    public List<Usuario> ListarUsuarios();
    public Usuario Detalles(int id);
    public void ModificarUsuario(int id, Usuario usuario);
    public void EliminarUsuario(int id);
    public void CambiarPassword(int id, Usuario usuario);
}