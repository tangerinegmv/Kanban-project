using Microsoft.Data.Sqlite;
using Kanban;
using Kanban.ViewModels;
public class UsuarioRepository: IUsuarioRepository
{
   // private const string cadenaConexion = @"Data Source=Kanban.db";
    private readonly string _connectionString;

    public UsuarioRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    

    public Usuario CrearUsuario(CrearUsuarioViewModel usuario)
    {
        Usuario nuevoUsuario = null;
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            var query = "INSERT INTO Usuario (nombre_de_usuario, password, rolusuario) VALUES (@nombre_de_usuario, @password, @rolusuario);";
            connection.Open();
            using var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
            command.Parameters.Add(new SqliteParameter("@password", usuario.Password));
            command.Parameters.Add(new SqliteParameter("@rolusuario", usuario.RolUsuario));
            command.ExecuteNonQuery();

            command.CommandText = "SELECT MAX(id) FROM Tarea";
            int idGenerado = Convert.ToInt32(command.ExecuteScalar());

            nuevoUsuario = new Usuario();
            nuevoUsuario.Id = idGenerado;
            nuevoUsuario.NombreDeUsuario = usuario.NombreDeUsuario;
            nuevoUsuario.Password = usuario.Password;
            nuevoUsuario.RolUsuario = (Rol)usuario.RolUsuario;
            connection.Close();
            //NO ANDA

        }
        if (nuevoUsuario == null)
        {
            throw new Exception("No se pudo crear el usuario.");
        }
        return nuevoUsuario;
    }

    public List<ListarUsuariosViewModel> ListarUsuarios()
    {
        List<ListarUsuariosViewModel> listaUsuario = new List<ListarUsuariosViewModel>();
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = @"SELECT * FROM Usuario;";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var usuario = new ListarUsuariosViewModel();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.RolUsuario = (Kanban.Rol)Convert.ToInt32(reader["rolusuario"]);
                    listaUsuario.Add(usuario);
                }
            }
            connection.Close();

        }
        if (listaUsuario == null)
        {
            throw new Exception("No se pudo listar los usuarios.");
        }
        return listaUsuario;
    }

    public Usuario Detalles(int id)
        {
            var usuario = new Usuario();
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
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
                    usuario.Password = reader["password"].ToString();
                    }
                }
                connection.Close();

            }
            if (usuario == null)
            {
                throw new Exception("No se pudo encontrar el usuario.");
            }
            return usuario;
        }
    public void ModificarUsuario(int id, ModificarUsuarioViewModel usuario)
        {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        var query = @"UPDATE Usuario SET 
                              nombre_de_usuario = COALESCE(@nombre_de_usuario, nombre_de_usuario), 
                              rolusuario = COALESCE(@RolUsuario, rolusuario) WHERE id = @id;";
        connection.Open();
        var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        command.Parameters.AddWithValue("@nombre_de_usuario",
            string.IsNullOrEmpty(usuario.NombreDeUsuario) ? DBNull.Value : usuario.NombreDeUsuario);
        command.Parameters.AddWithValue("@RolUsuario",
            usuario.RolUsuario.HasValue ? (object)usuario.RolUsuario : DBNull.Value);

        int filasAfectadas = command.ExecuteNonQuery();

        if (filasAfectadas == 0) // si no hay filas afectadas es porque no se modificó
        {
            throw new Exception("No se pudo modificar el usuario.");
        }
        connection.Close();

    } 
    public void EliminarUsuario(int id)
    {
        if (!Verifica(id))
        {
            throw new InvalidOperationException("El usuario está asociado a tableros o tareas y no puede ser eliminado.");
            
        }
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            var query = "DELETE FROM Usuario WHERE id = @id;";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            int filasAfectadas = command.ExecuteNonQuery();
            if (filasAfectadas == 0)
            {
                throw new KeyNotFoundException($"No se encontro el usuario con ID: {id}.");
            }
            connection.Close();
        }
    }

    public void CambiarPassword(int id, Usuario usuario)
    {
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            var query = "UPDATE Usuario SET password = @password WHERE id = @id;";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.Parameters.Add(new SqliteParameter("@password", usuario.Password ?? throw new ArgumentNullException(nameof(usuario.Password))));
            int filasAfectadas = command.ExecuteNonQuery();

            if (filasAfectadas == 0) // si no hay filas afectadas es porque no se modificó
            {
                throw new Exception("No se pudo modificar el usuario.");
            }
            connection.Close();
        }
    }

    private bool Verifica(int id)
    {
        int cantidad = 0;
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                string query = @"SELECT COUNT(*) FROM Usuario u
                                LEFT JOIN Tablero t ON u.id = id_usuario_propietario
                                LEFT JOIN Tarea x ON u.id = x.id_usuario_asignado
                                WHERE u.id = @id AND (t.id IS NOT NULL OR x.id IS NOT NULL);";
                connection.Open();
                var command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                cantidad = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        return cantidad == 0;
    }

    public void AsignarUsuario(int idUsuario, int idTarea)
    {
    using SqliteConnection connection = new SqliteConnection(_connectionString);
    var query = "UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id = @idTarea;";
    connection.Open();
    var command = new SqliteCommand(query, connection);
    command.Parameters.Add(new SqliteParameter("@idUsuario", idUsuario));
    command.Parameters.Add(new SqliteParameter("@idTarea", idTarea));
    int filasAfectadas = command.ExecuteNonQuery();

        if (filasAfectadas == 0) // si no hay filas afectadas es porque no se modificó
        {
            throw new Exception("No se pudo modificar el usuario.");
        }
    connection.Close();
    }

    public Usuario GetUsuarioNombre(string nombre)
  {
    Usuario usuarioBuscado = null;

    string query = @"SELECT * FROM 
                     Usuario 
                     WHERE nombre_de_usuario = @NombreDeUsuario;";

    using (SqliteConnection connection = new SqliteConnection(_connectionString))
    {
      connection.Open();

      SqliteCommand command = new SqliteCommand(query, connection);

      command.Parameters.AddWithValue("@NombreDeUsuario", nombre);
      using (SqliteDataReader reader = command.ExecuteReader())
      {
        if (reader.Read())
        {
          usuarioBuscado = new Usuario
          {
            Id = reader.GetInt32(0),
            NombreDeUsuario = reader.GetString(1),
            Password = reader.GetString(2),
            RolUsuario = (Rol)reader.GetInt32(3)
          };
        }
      }

      connection.Close();
    }

    if (usuarioBuscado == null)
    {
      throw new KeyNotFoundException($"No se encontro el usuario con nombre: {nombre}.");
    }

    return usuarioBuscado;
  }
  
}