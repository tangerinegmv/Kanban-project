using Microsoft.Data.Sqlite;
using Kanban;
public class UsuarioRepository
{
    private const string cadenaConexion = @"Data Source=Kanban.db";
    public void CrearUsuario(Usuario usuario)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "INSERT INTO Usuario (nombre_de_usuario, password, rolusuario) VALUES (@nombre_de_usuario, @password, @rolusuario);";
            connection.Open();
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.Add(new SqliteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SqliteParameter("@password", usuario.Password));
                command.Parameters.Add(new SqliteParameter("@rolusuario", usuario.RolUsuario));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public List<Usuario> ListarUsuarios()
    {
        List<Usuario> listaUsuario = new List<Usuario>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Usuario;";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.RolUsuario = (Kanban.Rol)Convert.ToInt32(reader["rolusuario"]);
                    listaUsuario.Add(usuario);
                }
            }
            connection.Close();

        }
        return listaUsuario;
    }

    public Usuario Detalles(int id)
        {
            var usuario = new Usuario();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                string query = "SELECT * FROM Usuario WHERE id = @id;";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.RolUsuario = (Kanban.Rol)Convert.ToInt32(reader["rolusuario"]);
                        
                    }
                }
                connection.Close();

            }
            return usuario;
        }
    public void ModificarUsuario(int id, Usuario usuario)
        {
            using ( SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                var query = "UPDATE Usuario SET nombre_de_usuario = @nombre_de_usuario, rolusuario = @rolusuario WHERE id = @id;";
                connection.Open();
                var command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.Parameters.Add(new SqliteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SqliteParameter("@rolusuario", usuario.RolUsuario));
                command.ExecuteNonQuery();
                connection.Close();
            }
        } 
    public void EliminarUsuario(int id)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "DELETE FROM Usuario WHERE id = @id;";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
  
}