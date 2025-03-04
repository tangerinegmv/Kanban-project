using Kanban;
using Kanban.ViewModels;
public interface IUsuarioRepository
{
    public Usuario CrearUsuario(CrearUsuarioViewModel usuario);
    public List<ListarUsuariosViewModel> ListarUsuarios();
    public Usuario Detalles(int id);
    public void ModificarUsuario(int id, ModificarUsuarioViewModel usuario);
    public void EliminarUsuario(int id);
    public void CambiarPassword(int id, Usuario usuario);
    public Usuario GetUsuarioNombre(string nombre);
}