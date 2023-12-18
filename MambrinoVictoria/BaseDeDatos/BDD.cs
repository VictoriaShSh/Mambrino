using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace MambrinoVictoria.BaseDeDatos
{
    /// <summary>
    /// Clase que se utiliza para la gestion de la base de datos
    /// </summary>
    public class BDD
    {
        string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "registroConexiones.txt");
        MySqlConnection conexion;
        string conn;
        static BDD instancia;

        // Objeto para controlar el acceso al archivo
        static readonly object bloqueo = new object();

        /// <summary>
        /// Obtiene o establece el nombre de usuario, puerto, servidor, contraseña para la conexion a la base de datos
        /// </summary>
        public string Usuario { get; set; }
        public string Puerto { get; set; }
        public string Servidor { get; set; }
        public string Contraseña { get; set; }

        /// <summary>
        /// Constructor privado para evitar instanciacion externa
        /// </summary>
        private BDD()
        {
        }

        /// <summary>
        /// Obtiene la instancia unica de la base de datos
        /// </summary>
        /// <returns>Instancia unica de la base de datos</returns>
        public static BDD InstanciaBDD()
        {
            if (instancia == null)
            {
                instancia = new BDD();
            }
            return instancia;
        }

        /// <summary>
        /// Establece la conexion a la base de datos utilizando los parametros de configuracion
        /// </summary>
        /// <returns>Devuelve true si la conexion se ha realizado corectamente y false si no</returns>
        public bool Conexion()
        {
            conn = $"Server={Servidor};Port={Puerto};Uid={Usuario};Pwd={Contraseña};";

            if (conexion == null)
            {
                try
                {
                    conexion = new MySqlConnection(conn);
                    conexion.Open();

                    // Registro de evento de conexión en el archivo utilizando hilo
                    RegistrarEventoEnHilo("CONEXION", "Conexion establecida");

                    string seleccionarBD = "USE mambrino;";
                    using (MySqlCommand cmdSeleccionarBD = new MySqlCommand(seleccionarBD, conexion))
                    {
                        cmdSeleccionarBD.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al establecer la conexion: " + ex.Message);
                }

                return false;
            }
            return false;
        }

        private void RegistrarEventoEnHilo(string tipo, string descripcion)
        {
            // Crear un hilo que actuara en segundo plano
            Thread hilo = new Thread(() =>
            {
                lock (bloqueo) // Asegura el acceso desde varios hilos
                {
                    try
                    {
                        // Añade un registro en el archivo para registrar el evento
                        string registro = $"{DateTime.Now}: {tipo} - {descripcion}";
                        File.AppendAllText(ruta, registro + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar el evento: " + ex.Message);
                    }
                }
            });

            hilo.Start();
        }


        /// <summary>
        /// Metodo que se utiliza para restablece la base de datos eliminando registros de agunas tablas y restaurando usuario administrador
        /// </summary>
        public void RestablecerBDD()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM patologia", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM hoja_prescripcion", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM notas_enfermeria", conexion))
                {
                    cmd.ExecuteNonQuery();
                }
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM usuarios", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM alertas", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM alta", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM ingresos", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM pacientes", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("UPDATE camas SET estado = 'Disponible'", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand("UPDATE camas SET bloqueada = 'N'", conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                string insertUsuarios = @"INSERT INTO Usuarios (id_usuario, nombre, apellidos, email, id_rol, password, ID_CENTRO) VALUES
                    (1, 'admin', 'admin', 'admin', 1, '1234', 1)";

                using (MySqlCommand cmd = new MySqlCommand(insertUsuarios, conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Base de datos restablecida correctamente \nRECUERDA: El usuario ADMINISTRADOR ha cambiado: \nUsuario: admin\tContraseña: 1234");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al restablecer la base de datos: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se utiliza para modificar el usuario y la contraseña del usuario administrador
        /// </summary>
        public void ModificarUsuarioAdmin(string nuevoUsuario, string nuevaContraseña)
        {
            MySqlTransaction transaction = null;

            try
            {
                transaction = conexion.BeginTransaction();

                string modificarUsuarioAdminQuery = "UPDATE Usuarios SET email = @nuevoUsuario, password = @nuevaContraseña WHERE id_usuario = 1";

                using (MySqlCommand cmdModificarUsuario = new MySqlCommand(modificarUsuarioAdminQuery, conexion))
                {
                    cmdModificarUsuario.Parameters.AddWithValue("@nuevoUsuario", nuevoUsuario);
                    cmdModificarUsuario.Parameters.AddWithValue("@nuevaContraseña", nuevaContraseña);

                    cmdModificarUsuario.ExecuteNonQuery();
                }

                transaction.Commit();

                MessageBox.Show("Usuario administrador modificado correctamente");
            }
            catch (MySqlException ex)
            {
                // En caso de error, realiza un rollback para deshacer los cambios puesto que se perderia el usuario administrador
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                MessageBox.Show("Error al modificar el usuario administrador: " + ex.Message);
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }

        /// <summary>
        /// Metodo que se utiliza para registrar un nuevo usuario
        /// </summary>
        public void AgregarUsuario(string nombre, string apellidos, string email, int id_rol, string contraseña, int id_centro)
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("No se ha establecido una conexión válida.");
                    return;
                }

                string comprobarEmail = "SELECT COUNT(*) FROM Usuarios WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(comprobarEmail, conexion))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int contador = Convert.ToInt32(cmd.ExecuteScalar());

                    if (contador > 0)
                    {
                        MessageBox.Show("El email ya está registrado. No se puede agregar el usuario.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Usuarios (nombre, apellidos, email, id_rol, password, id_centro) VALUES (@nombre, @apellidos, @email, @id_rol, @password, @id_centro)";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@id_rol", id_rol);
                    cmd.Parameters.AddWithValue("@password", contraseña);
                    cmd.Parameters.AddWithValue("@id_centro", id_centro);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        Console.WriteLine("Usuario registrado correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al hacer el registro");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        /// <summary>
        /// Metodo que se utiliza para iniciar sesion
        /// </summary>
        public bool IniciarSesion(string email, string clave)
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("No se ha establecido una conexión válida.");
                    return false;
                }

                string consulta = "SELECT COUNT(*) FROM Usuarios WHERE email = @email AND password = @password";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", clave);

                    int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (rowCount > 0)
                    {
                        // Registro cuando un suario inica sesion
                        RegistrarEventoEnHilo("INICIO SESION ", "El usuario: " + email + " ha iniciado sesion");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Metodo que se utiliza para registrar un nuevo paciente en la base de datos
        /// </summary>
        public void AgregarPaciente(string nif, string numeroSS, string nombre, string apellido1, string apellido2,
            string sexo, DateTime fechaNacimiento, string telefono1, string telefono2, string movil, string estadoCivil,
            string estudios, string fallecido, string nacionalidad, string cAutonoma, string provincia, string poblacion,
            int cp, string direccion, string titular, string regimen, string tis, string medico, string cap, string zona,
            string area, string nacimiento, string cAutonomaNacimiento, string provinciaNacimiento, string poblacionNacimiento)
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("No se ha establecido una conexión válida.");
                    return;
                }

                string insertQuery = "INSERT INTO Pacientes (NIF, NumeroSS, Nombre, Apellido1, Apellido2, Sexo, FechaNacimiento, Telefono1, " +
                    "Telefono2, Movil, EstadoCivil, Estudios, Fallecido, Nacionalidad, CAutonoma, Provincia, Poblacion, CP, Direccion, Titular, Regimen, " +
                    "TIS, Medico, CAP, Zona, Area, Nacimiento, CAutonomaNacimiento, ProvinciaNacimiento, PoblacionNacimiento) " +
                    "VALUES (@nif, @numeroSS, @nombre, @apellido1, @apellido2, @sexo, @fechaNacimiento, @telefono1, @telefono2, " +
                    "@movil, @estadoCivil, @estudios, @fallecido, @nacionalidad, @cAutonoma, @provincia, @poblacion, @cp, @direccion, @titular, @regimen, " +
                    "@tis, @medico, @cap, @zona, @area, @nacimiento, @CAutonomaNacimiento, @ProvinciaNacimiento, @PoblacionNacimiento)";

                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@nif", nif);
                    cmd.Parameters.AddWithValue("@numeroSS", numeroSS);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido1", apellido1);
                    cmd.Parameters.AddWithValue("@apellido2", string.IsNullOrEmpty(apellido2) ? (object)DBNull.Value : apellido2);
                    cmd.Parameters.AddWithValue("@sexo", string.IsNullOrEmpty(sexo) ? (object)DBNull.Value : sexo);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                    cmd.Parameters.AddWithValue("@telefono1", string.IsNullOrEmpty(telefono1) ? (object)DBNull.Value : telefono1);
                    cmd.Parameters.AddWithValue("@telefono2", string.IsNullOrEmpty(telefono2) ? (object)DBNull.Value : telefono2);
                    cmd.Parameters.AddWithValue("@movil", string.IsNullOrEmpty(movil) ? (object)DBNull.Value : movil);
                    cmd.Parameters.AddWithValue("@estadoCivil", string.IsNullOrEmpty(estadoCivil) ? (object)DBNull.Value : estadoCivil);
                    cmd.Parameters.AddWithValue("@estudios", string.IsNullOrEmpty(estudios) ? (object)DBNull.Value : estudios);
                    cmd.Parameters.AddWithValue("@fallecido", fallecido);
                    cmd.Parameters.AddWithValue("@nacionalidad", nacionalidad);
                    cmd.Parameters.AddWithValue("@cAutonoma", cAutonoma);
                    cmd.Parameters.AddWithValue("@provincia", provincia);
                    cmd.Parameters.AddWithValue("@poblacion", poblacion);
                    cmd.Parameters.AddWithValue("@cp", cp);
                    cmd.Parameters.AddWithValue("@direccion", string.IsNullOrEmpty(direccion) ? (object)DBNull.Value : direccion);
                    cmd.Parameters.AddWithValue("@titular", titular);
                    cmd.Parameters.AddWithValue("@regimen", regimen);
                    cmd.Parameters.AddWithValue("@tis", tis);
                    cmd.Parameters.AddWithValue("@medico", medico);
                    cmd.Parameters.AddWithValue("@cap", cap);
                    cmd.Parameters.AddWithValue("@zona", zona);
                    cmd.Parameters.AddWithValue("@area", area);
                    cmd.Parameters.AddWithValue("@nacimiento", nacimiento);
                    cmd.Parameters.AddWithValue("@CAutonomaNacimiento", cAutonomaNacimiento);
                    cmd.Parameters.AddWithValue("@ProvinciaNacimiento", provinciaNacimiento);
                    cmd.Parameters.AddWithValue("@PoblacionNacimiento", poblacionNacimiento);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        Console.WriteLine("Paciente registrado correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al hacer el registro del paciente");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al agregar paciente: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se utiliza para mostrar la informacion del paciente seleccionado
        /// </summary>
        public List<string> MostrarPaciente(int nhc)
        {
            List<string> datosPaciente = new List<string>();

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("No se ha establecido una conexión válida.");
                    return datosPaciente;
                }

                string query = "SELECT * FROM Pacientes WHERE nhc = @nhc";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            datosPaciente.Add(reader["NHC"].ToString());
                            datosPaciente.Add(reader["NIF"].ToString());
                            datosPaciente.Add(reader["NumeroSS"].ToString());
                            datosPaciente.Add(reader["Nombre"].ToString());
                            datosPaciente.Add(reader["Apellido1"].ToString());
                            datosPaciente.Add(reader["Apellido2"].ToString());
                            datosPaciente.Add(reader["Sexo"].ToString());
                            datosPaciente.Add(reader["FechaNacimiento"].ToString());
                            datosPaciente.Add(reader["Telefono1"].ToString());
                            datosPaciente.Add(reader["Telefono2"].ToString());
                            datosPaciente.Add(reader["Movil"].ToString());
                            datosPaciente.Add(reader["EstadoCivil"].ToString());
                            datosPaciente.Add(reader["Estudios"].ToString());
                            datosPaciente.Add(reader["Fallecido"].ToString());
                            datosPaciente.Add(reader["Nacionalidad"].ToString());
                            datosPaciente.Add(reader["CAutonoma"].ToString());
                            datosPaciente.Add(reader["Provincia"].ToString());
                            datosPaciente.Add(reader["Poblacion"].ToString());
                            datosPaciente.Add(reader["CP"].ToString());
                            datosPaciente.Add(reader["Direccion"].ToString());
                            datosPaciente.Add(reader["Titular"].ToString());
                            datosPaciente.Add(reader["Regimen"].ToString());
                            datosPaciente.Add(reader["TIS"].ToString());
                            datosPaciente.Add(reader["Medico"].ToString());
                            datosPaciente.Add(reader["CAP"].ToString());
                            datosPaciente.Add(reader["Zona"].ToString());
                            datosPaciente.Add(reader["Area"].ToString());
                            datosPaciente.Add(reader["Nacimiento"].ToString());
                            datosPaciente.Add(reader["CAutonomaNacimiento"].ToString());
                            datosPaciente.Add(reader["ProvinciaNacimiento"].ToString());
                            datosPaciente.Add(reader["PoblacionNacimiento"].ToString());
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return datosPaciente;
        }

        /// <summary>
        /// Metodo que se utiliza para modificar la informacion de un paciente seleccionado registrado
        /// </summary>
        public void ModificarPaciente(string nif, string telefono1, string telefono2, string movil, string estadoCivil, string estudios, string fallecido,
            string cAutonoma, string provincia, string poblacion, string direccion, string cp)
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("No se ha establecido una conexión válida.");
                    return;
                }

                string updateQuery = "UPDATE pacientes SET Telefono1 = @telefono1, Telefono2 = @telefono2, Movil = @movil, EstadoCivil = @estadoCivil, " +
                    "Estudios = @estudios, Fallecido = @fallecido, CAutonoma = @cAutonoma, Provincia = @provincia, Poblacion = @poblacion, " +
                    "Direccion = @direccion, CP = @cp WHERE nif = @nif";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@telefono1", telefono1);
                    cmd.Parameters.AddWithValue("@telefono2", telefono2);
                    cmd.Parameters.AddWithValue("@movil", movil);
                    cmd.Parameters.AddWithValue("@estadoCivil", estadoCivil);
                    cmd.Parameters.AddWithValue("@estudios", estudios);
                    cmd.Parameters.AddWithValue("@fallecido", fallecido);
                    cmd.Parameters.AddWithValue("@cAutonoma", cAutonoma);
                    cmd.Parameters.AddWithValue("@provincia", provincia);
                    cmd.Parameters.AddWithValue("@poblacion", poblacion);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@cp", cp);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("Registro actualizado correctamente");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún registro para actualizar");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se utiliza para obtener los roles de los usuarios
        /// </summary>
        public List<string> ObtenerRoles()
        {
            List<string> roles = new List<string>();

            try
            {
                string consulta = "SELECT nombre FROM roles";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(reader["nombre"].ToString());
                    }
                }

                return roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los roles: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener los nombres de los usuarios existentes por su perfil
        /// </summary>
        public List<string> ObtenerNombresUsuarios(int id_rol)
        {
            List<string> nombresCompletos = new List<string>();

            try
            {
                string consulta = "SELECT CONCAT(nombre, ' ', apellidos) AS NombreCompleto FROM usuarios WHERE id_rol = @id_rol";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_rol", id_rol);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreCompleto = reader["NombreCompleto"].ToString();
                            nombresCompletos.Add(nombreCompleto);
                        }
                    }
                }

                return nombresCompletos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los nombres de usuarios: " + ex.Message);
                return null;
            }
        }


        public void EliminarUsuario(string nombreCompleto)
        {
            try
            {
                // Divide el nombre completo en nombre y apellidos
                string[] partesNombre = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // El primer elemento es el nombre
                string nombre = partesNombre[0];

                // Los elementos restantes son los apellidos
                string apellidos = partesNombre.Length > 1 ? string.Join(" ", partesNombre.Skip(1)) : "";

                // Realiza la eliminación del usuario
                string eliminarUsuarioQuery = "DELETE FROM usuarios WHERE nombre = @nombre AND apellidos = @apellidos";

                using (MySqlCommand cmdEliminarUsuario = new MySqlCommand(eliminarUsuarioQuery, conexion))
                {
                    cmdEliminarUsuario.Parameters.AddWithValue("@nombre", nombre);
                    cmdEliminarUsuario.Parameters.AddWithValue("@apellidos", apellidos);

                    cmdEliminarUsuario.ExecuteNonQuery();
                }

                MessageBox.Show("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se utiliza para mostrar las comunidades autonomas
        /// </summary>
        public List<string> ObtenerComunidadesA()
        {
            List<string> nombresComunidadesA = new List<string>();

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("No se ha establecido una conexión válida.");
                    return nombresComunidadesA;
                }

                string consulta = "SELECT nombre FROM ComunidadAutonoma";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nombresComunidadesA.Add(reader["nombre"].ToString());
                    }
                }
                return nombresComunidadesA;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las comunidades autonomas: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo que se utiliza para mostrar las provincias segun la comunidad autonoma
        /// </summary>
        public List<string> ObtenerProvinciasPorComunidad(string comunidad)
        {
            List<string> provincias = new List<string>();

            try
            {
                string consulta = "SELECT p.nombre FROM Provincia p INNER JOIN ComunidadAutonoma c ON p.id_cautonoma = c.id_cautonoma WHERE c.nombre = @comunidad";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@comunidad", comunidad);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            provincias.Add(reader["nombre"].ToString());
                        }
                    }
                }

                return provincias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las provincias: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para mostrar los areas de salud
        /// </summary>
        public List<string> ObtenerAreaSalud()
        {
            List<string> areasSalud = new List<string>();

            try
            {
                string consulta = "SELECT nombre FROM areaSalud";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        areasSalud.Add(reader["nombre"].ToString());
                    }
                }

                return areasSalud;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las areas de salud: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener las zonas basicas de salud segun el area de salud seleccionado
        /// </summary>
        public List<string> ObtenerZonaBasicaporArea(string area)
        {
            List<string> zonasBasicas = new List<string>();

            try
            {
                string consulta = "SELECT z.nombre FROM zonaBasicaSalud z INNER JOIN areaSalud a ON z.id_area_salud = a.id_area_salud WHERE a.nombre = @area";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@area", area);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zonasBasicas.Add(reader["nombre"].ToString());
                        }
                    }
                }

                return zonasBasicas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las zonas básicas de salud: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener los centros de atencion primaria
        /// </summary>
        public string ObtenerCAP(string zona)
        {
            string cap = null;

            try
            {
                string consulta = "SELECT cap FROM zonaBasicaSalud WHERE nombre = @zona";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@zona", zona);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cap = reader["cap"].ToString();
                        }
                    }
                }

                return cap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el centro de atencion primaria: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para mostrar los usuarios registrados
        /// </summary>
        public List<string> ObtenerUsuarios()
        {
            List<string> usuarios = new List<string>();

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("No se ha establecido una conexión válida.");
                    return usuarios;
                }

                string consulta = "SELECT nombre, apellidos FROM Usuarios";
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader["nombre"].ToString();
                        string apellidos = reader["apellidos"].ToString();
                        string nombreCompleto = $"{nombre} {apellidos}";
                        usuarios.Add(nombreCompleto);
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los usuarios: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener los nombres de los Hospitales
        /// </summary>
        public List<string> ObtenerNombresCentros()
        {
            List<string> nombresCentros = new List<string>();

            try
            {
                string consulta = "SELECT nombre FROM centros";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreCentro = reader["nombre"].ToString();
                            nombresCentros.Add(nombreCentro);
                        }
                    }
                }

                return nombresCentros;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los nombres de los hospitales: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener el id del centro seleccionado
        /// </summary>
        public int ObtenerIdCentro(string centro)
        {
            int id_centro = 0;

            try
            {
                string consulta = "SELECT id_centro FROM centros WHERE nombre = @nombre";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", centro);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id_centro = Convert.ToInt32(reader["id_centro"]);
                        }
                    }
                }

                return id_centro;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el ID del centro: " + ex.Message);
                return 0;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener las camas segun la planta y el hospital seleccionado
        /// </summary>
        public List<string> ObtenerCamasPorPlantas(int planta, int centro)
        {
            List<string> camas = new List<string>();

            try
            {
                string consulta = "SELECT c.id_planta, c.id_habitacion, c.letra FROM camas c JOIN centros x ON c.id_centro = x.id_centro WHERE c.id_planta = @id_planta AND c.id_centro = @id_centro";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_planta", planta);
                    cmd.Parameters.AddWithValue("@id_centro", centro);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id_planta = reader["id_planta"].ToString();
                            string id_habitacion = reader["id_habitacion"].ToString().PadLeft(2, '0');
                            string letra = reader["letra"].ToString();
                            string nombreCama = $"{id_planta}{id_habitacion}{letra}";
                            camas.Add(nombreCama);
                        }
                    }
                }

                return camas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las camas: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener el id de la cama seleccionada
        /// </summary>
        public int ObtenerIdCama(string refCama, int id_centro)
        {
            int cama = 0;

            try
            {
                int planta = Convert.ToInt32(refCama.Substring(0, 1));
                string habitacion = refCama.Substring(1, 2);
                string letra = refCama.Substring(3, 1);

                string consulta = "SELECT id_cama FROM camas WHERE id_planta = @planta AND id_habitacion = @habitacion AND letra = @letra AND id_centro = @id_centro";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@planta", planta);
                    cmd.Parameters.AddWithValue("@habitacion", habitacion);
                    cmd.Parameters.AddWithValue("@letra", letra);
                    cmd.Parameters.AddWithValue("@id_centro", id_centro);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cama = Convert.ToInt32(reader["id_cama"]);
                        }
                    }
                }

                return cama;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el ID de la cama: " + ex.Message);
                return 0;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener el estado de la cama
        /// </summary>
        public bool ComprobarCamaBloqueada(int idCama)
        {
            bool estaBloqueada = false;

            try
            {
                string consulta = "SELECT bloqueada FROM camas WHERE id_cama = @idCama";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCama", idCama);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estaBloqueada = Convert.ToChar(reader["bloqueada"]) == 'S';
                        }
                    }
                }

                return estaBloqueada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al comprobar el estado de la cama: " + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para cambiar el estado de bloqueo de la cama a S o N
        /// </summary>
        public void BloquearCama(int idCama, bool bloquear)
        {
            try
            {
                string updateQuery = "UPDATE camas SET bloqueada = @bloqueada WHERE id_cama = @idCama";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCama", idCama);
                    cmd.Parameters.AddWithValue("@bloqueada", bloquear ? 'S' : 'N');

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar el bloqueo de la cama: " + ex.Message);
            }
        }


        /// <summary>
        /// Metodo que se utiliza para comprobar si un paciente tiene una cama asignada
        /// </summary>
        public bool PacienteIngresadoEnCama(int nhcPaciente)
        {
            bool pacienteIngresado = false;

            try
            {
                string consulta = "SELECT COUNT(*) FROM ingresos WHERE nhc = @nhc AND id_cama != 0";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhcPaciente);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    pacienteIngresado = count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al comprobar si el paciente tiene una cama asignada: " + ex.Message);
            }

            return pacienteIngresado;
        }

        /// <summary>
        /// Metodo que se utiliza para buscar pacientes pertenecientes a una zona basica
        /// </summary>
        public List<string> BuscarPacientesPorZonaBasica(string Zona)
        {
            List<string> pacientes = new List<string>();

            try
            {
                string consulta = "SELECT nombre, apellido1, apellido2 FROM Pacientes WHERE Zona = @Zona ORDER BY nombre, apellido1, apellido2";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@Zona", Zona);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string apellido1 = reader["apellido1"].ToString();
                            string apellido2 = reader["apellido2"].ToString();
                            string nombreCompleto = $"{nombre} {apellido1} {apellido2}";
                            pacientes.Add(nombreCompleto);
                        }
                    }
                }

                return pacientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar pacientes por zona basica: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para verificar la existencia de un paciente por su NIF
        /// </summary>
        public bool VerificarExistenciaNIF(string nif)
        {
            bool existe = false;

            try
            {
                string consulta = "SELECT COUNT(*) FROM Pacientes WHERE NIF = @nif";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nif", nif);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        existe = true;
                    }
                }

                return existe;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar la existencia del paciente por NIF: " + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para verificar la existencia de un paciente por su número de la seguridad social
        /// </summary>
        public bool VerificarExistenciaNumeroSS(string numeroSS)
        {
            bool existe = false;

            try
            {
                string consulta = "SELECT COUNT(*) FROM Pacientes WHERE NumeroSS = @numeroSS";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@numeroSS", numeroSS);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        existe = true;
                    }
                }

                return existe;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar la existencia del paciente por NumeroSS: " + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para verificar la existencia de un paciente por su TIS
        /// </summary>
        public bool VerificarExistenciaTIS(string tis)
        {
            bool existe = false;

            try
            {
                string consulta = "SELECT COUNT(*) FROM Pacientes WHERE TIS = @tis";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@tis", tis);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        existe = true;
                    }
                }

                return existe;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar la existencia del paciente por TIS: " + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para ingresar un paciente en una cama
        /// </summary>
        public void IngresarPaciente(string estadoHnc, int idAmbito, DateTime fecha, TimeSpan hora, int idCama, int nhc)
        {
            try
            {
                string actualizarCamaQuery = "UPDATE camas SET estado = 'Ocupada' WHERE id_cama = @idCama";

                using (MySqlCommand cmdActualizarCama = new MySqlCommand(actualizarCamaQuery, conexion))
                {
                    cmdActualizarCama.Parameters.AddWithValue("@idCama", idCama);
                    cmdActualizarCama.ExecuteNonQuery();
                }

                string consulta = "INSERT INTO ingresos (estado_hnc, id_ambito, Fecha, Hora, id_cama, nhc) VALUES (@estadoHnc, @idAmbito, @fecha, @hora, @idCama, @nhc)";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@estadoHnc", estadoHnc);
                    cmd.Parameters.AddWithValue("@idAmbito", idAmbito);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@hora", hora);
                    cmd.Parameters.AddWithValue("@idCama", idCama);
                    cmd.Parameters.AddWithValue("@nhc", nhc);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar al paciente en la cama: " + ex.Message);
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener el NIF del paciente a traves de su NHC
        /// </summary>
        public string ObtenerNIFporHNC(int nhc)
        {
            string nif = null;

            try
            {
                string consulta = "SELECT NIF FROM Pacientes WHERE nhc = @nhc";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nif = reader.GetString("NIF");
                        }
                    }
                }

                return nif;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el NIF del paciente: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener el NHC del paciente a traves de su NIF
        /// </summary>
        public int ObtenerNHCporNIF(string nif)
        {
            int nhc = 0;

            try
            {
                string consulta = "SELECT NHC FROM Pacientes WHERE nif = @nif";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nif", nif);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nhc = reader.GetInt32("NHC");
                        }
                    }
                }

                return nhc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el NHC del paciente: " + ex.Message);
                return 0;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener el NHC del paciente ingresado en una cama especifica
        /// </summary>
        public int ObtenerNHCporIdCama(int idCama)
        {
            int nhc = 0;

            try
            {
                string consulta = "SELECT p.NHC FROM Pacientes p JOIN ingresos i ON p.nhc = i.nhc WHERE i.id_cama = @idCama";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCama", idCama);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nhc = reader.GetInt32("NHC");
                        }
                    }
                }

                return nhc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el NHC del paciente: " + ex.Message);
                return 0;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener el NIF del paciente por su nombre y apellidos
        /// </summary>
        public string ObtenerNIFPaciente(string nombreCompleto)
        {
            string nif = null;

            try
            {
                string consulta = "SELECT NIF FROM Pacientes WHERE CONCAT(Nombre, ' ', Apellido1, ' ', Apellido2) = @nombreCompleto";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nif = reader.GetString("NIF");
                        }
                    }
                }

                return nif;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el NIF del paciente: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza verificar si un pacinte ya se encuentra ingresado en una cama
        /// </summary>
        public bool VerificarPacienteIngresado(int idCama)
        {
            string consulta = "SELECT estado FROM camas WHERE id_cama = @idCama";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCama", idCama);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result.ToString().Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error verificando paciente ingresado " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo que se utiliza para obtener el sexo del paciente seleccionado
        /// </summary>
        public bool ObtenerSexoPaciente(int nhc)
        {
            try
            {
                string consulta = "SELECT sexo FROM pacientes WHERE NHC = @nhc";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);

                    string sexo = cmd.ExecuteScalar()?.ToString();
                    return sexo == "Hombre";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el sexo del paciente: " + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para averiguar si un paciente tiene o no alguna alerta
        /// </summary>
        public bool ObtenerAlertaPaciente(int nhc)
        {
            try
            {
                string consulta = "SELECT COUNT(*) FROM alertas WHERE NHC = @nhc";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la alerta del paciente: " + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para crear una alerta para un paciente
        /// </summary>
        public void CrearAlerta(string descripcion, string observaciones, DateTime fecha, int nhc)
        {
            try
            {
                string consulta = "INSERT INTO alertas (descripcion, observaciones, fecha, nhc) " +
                                  "VALUES (@descripcion, @observaciones, @fecha, @nhc)";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@observaciones", observaciones);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@nhc", nhc);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la alerta para el paciente: " + ex.Message);
            }
        }


        /// <summary>
        /// Metodo que se utiliza para mostrar las alertas de un paciente
        /// </summary>
        public List<string> MostrarAlerta(int nhc)
        {
            List<string> alertas = new List<string>();

            try
            {
                string consulta = "SELECT descripcion, observaciones FROM alertas WHERE NHC = @nhc";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string descripcion = reader["descripcion"].ToString();
                            string observaciones = reader["observaciones"].ToString();

                            string alertaInfo = $"Descripción: {descripcion}, Observaciones: {observaciones}";
                            alertas.Add(alertaInfo);
                        }
                    }
                }

                return alertas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las alertas del paciente: " + ex.Message);
                return new List<string>();
            }
        }


        /// <summary>
        /// Metodo que se utiliza para actualizar una alerta
        /// </summary>
        public void ActualizarAlerta(string nuevaDescripcion, string nuevasObservaciones, DateTime nuevaFecha, int nhc)
        {
            try
            {
                string consulta = "UPDATE alertas SET descripcion = @nuevaDescripcion, observaciones = @nuevasObservaciones, fecha = @nuevaFecha WHERE nhc = @nhc";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nuevaDescripcion", nuevaDescripcion);
                    cmd.Parameters.AddWithValue("@nuevasObservaciones", nuevasObservaciones);
                    cmd.Parameters.AddWithValue("@nuevaFecha", nuevaFecha);
                    cmd.Parameters.AddWithValue("@nhc", nhc);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la alerta: " + ex.Message);
            }
        }


        /// <summary>
        /// Metodo que se utiliza para mostrar la información de un paciente seleccionado
        /// </summary>
        public string MostrarInfoPacienteSeleccionado(int nhc)
        {
            try
            {
                string paciente = null;

                string consulta = "SELECT nombre, apellido1, apellido2 FROM Pacientes WHERE NHC = @nhc;";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string apellido1 = reader["apellido1"].ToString();
                            string apellido2 = reader["apellido2"].ToString();
                            string nombreCompleto = $"{apellido1} {apellido2} {nombre}";
                            paciente = nombreCompleto;
                        }
                    }
                }

                return paciente;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar la informacion del paciente: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener la edad de un paciente seleccionado
        /// </summary>
        public int ObtenerEdadPaciente(int nhc)
        {
            try
            {
                int edad = 0;

                string consulta = "SELECT fechaNacimiento FROM Pacientes WHERE NHC = @nhc;";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                            edad = CalcularEdad(fechaNacimiento);
                        }
                    }
                }

                return edad;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la edad del paciente: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Método auxiliar para calcular la edad a partir de la fecha de nacimiento
        /// </summary>
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                int edad = ahora.Year - fechaNacimiento.Year;

                if (ahora.Month < fechaNacimiento.Month || (ahora.Month == fechaNacimiento.Month && ahora.Day < fechaNacimiento.Day))
                {
                    edad--;
                }

                return edad;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular la edad: " + ex.Message);
                return 0;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener la descripcion de una alerta
        /// </summary>
        public string MostrarInfoAlertaPaciente(int nhc)
        {
            try
            {
                string infoAlerta = null;

                string consulta = "SELECT descripcion FROM Alertas WHERE NHC = @nhc;";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nhc", nhc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string descripcion = reader["descripcion"].ToString();
                            infoAlerta = descripcion;
                        }
                    }
                }
                return infoAlerta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la informacion de la alerta: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Metodo que se utiliza para obtener los sintomas y el dianostico de un paciente
        /// </summary>
        public List<string> ObtenerSintomaDiagnosticoPorPaciente(int nhcPaciente)
        {
            List<string> sintomaDiagnostico = new List<string>();

            try
            {
                string consultarSintomaDiagnosticoQuery = @"SELECT sintomas, diagnostico FROM patologia WHERE nhc = @nhcPaciente ORDER BY id_patologia DESC LIMIT 1";

                using (MySqlCommand cmdConsultarSintomaDiagnostico = new MySqlCommand(consultarSintomaDiagnosticoQuery, conexion))
                {
                    cmdConsultarSintomaDiagnostico.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    using (MySqlDataReader reader = cmdConsultarSintomaDiagnostico.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sintomaDiagnostico.Add(reader["sintomas"].ToString());
                            sintomaDiagnostico.Add(reader["diagnostico"].ToString());
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener  sintomas y diagnostico: " + ex.Message);
            }

            return sintomaDiagnostico;
        }

        /// <summary>
        /// Metodo que se utiliza para dar el alta a un paciente
        /// </summary>
        public void DarAltaPaciente(int idCama, DateTime fecha, TimeSpan hora, string motivo, string tipo, int nhc)
        {
            try
            {
                string actualizarCamaQuery = "UPDATE camas SET estado = 'Disponible' WHERE id_cama = @idCama";

                using (MySqlCommand cmdActualizarCama = new MySqlCommand(actualizarCamaQuery, conexion))
                {
                    cmdActualizarCama.Parameters.AddWithValue("@idCama", idCama);
                    cmdActualizarCama.ExecuteNonQuery();
                }

                string actualizarIngresos = "UPDATE ingresos SET id_cama = NULL WHERE nhc = @nhc";

                using (MySqlCommand cmdIngresos = new MySqlCommand(actualizarIngresos, conexion))
                {
                    cmdIngresos.Parameters.AddWithValue("@nhc", nhc);
                    cmdIngresos.ExecuteNonQuery();
                }

                string insertarAltaQuery = "INSERT INTO alta (fecha, hora, motivo, tipo, nhc) VALUES (@fecha, @hora, @motivo, @tipo, @nhc)";

                using (MySqlCommand cmdInsertarAlta = new MySqlCommand(insertarAltaQuery, conexion))
                {
                    cmdInsertarAlta.Parameters.AddWithValue("@fecha", fecha);
                    cmdInsertarAlta.Parameters.AddWithValue("@hora", hora);
                    cmdInsertarAlta.Parameters.AddWithValue("@motivo", motivo);
                    cmdInsertarAlta.Parameters.AddWithValue("@tipo", tipo);
                    cmdInsertarAlta.Parameters.AddWithValue("@nhc", nhc);

                    cmdInsertarAlta.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dar de alta al paciente: " + ex.Message);
            }
        }


        /// <summary>
        /// Metodo que se utiliza para registrar las notas de enfermeria de un pacinete
        /// </summary>
        public void RegistrarNotasEnfermeria(int nhcPaciente, DateTime fechaToma, TimeSpan horaToma, decimal temperatura, int tensionArterialSistolica,
            int tensionArterialDiastolica, int frecuenciaCardiaca, int frecuenciaRespiratoria, decimal peso, decimal talla,
            decimal indiceMasaCorporal, decimal glucemiaCapilar, string ingestaAlimentos, string tipoDeposicion,
            string nauseas, string prurito, string observaciones, int balanceHidricoEntradaAlimentos,
            int balanceHidricoEntradaLiquidos, int balanceHidricoFluidoterapia, int balanceHidricoHemoderivados,
            int balanceHidricoOtrosEntrada, int balanceHidricoDiuresis, int balanceHidricoVomitos, int balanceHidricoHeces,
            int balanceHidricoSondaNasogastrica, int balanceHidricoDrenajes, int balanceHidricoOtrasPerdidas, int totalBalanceHidrico)
        {
            try
            {
                string insertarNotasEnfermeriaQuery = @"INSERT INTO notas_enfermeria (fecha_toma, hora_toma,
                temperatura, tension_arterial_sistolica, tension_arterial_diastolica, frecuencia_cardiaca,
                frecuencia_respiratoria, peso, talla, indice_masa_corporal, glucemia_capilar, ingesta_alimentos,
                tipo_deposicion, nauseas, prurito, observaciones, balance_hidrico_entrada_alimentos,
                balance_hidrico_entrada_liquidos, balance_hidrico_fluidoterapia, balance_hidrico_hemoderivados,
                balance_hidrico_otros_entrada, balance_hidrico_diuresis, balance_hidrico_vomitos, balance_hidrico_heces,
                balance_hidrico_sonda_nasogastrica, balance_hidrico_drenajes, balance_hidrico_otras_perdidas,
                total_balance_hidrico, nhc) VALUES (@fechaToma, @horaToma, @temperatura, @tensionArterialSistolica, 
                @tensionArterialDiastolica, @frecuenciaCardiaca, @frecuenciaRespiratoria, @peso, @talla, @indiceMasaCorporal, 
                @glucemiaCapilar, @ingestaAlimentos, @tipoDeposicion, @nauseas, @prurito, @observaciones, @balanceHidricoEntradaAlimentos,
                @balanceHidricoEntradaLiquidos, @balanceHidricoFluidoterapia, @balanceHidricoHemoderivados,
                @balanceHidricoOtrosEntrada, @balanceHidricoDiuresis, @balanceHidricoVomitos, @balanceHidricoHeces,
                @balanceHidricoSondaNasogastrica, @balanceHidricoDrenajes, @balanceHidricoOtrasPerdidas,
                @totalBalanceHidrico, @nhc)";

                using (MySqlCommand cmdInsertarNotasEnfermeria = new MySqlCommand(insertarNotasEnfermeriaQuery, conexion))
                {
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@fechaToma", fechaToma);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@horaToma", horaToma);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@temperatura", temperatura);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@tensionArterialSistolica", tensionArterialSistolica);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@tensionArterialDiastolica", tensionArterialDiastolica);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@frecuenciaCardiaca", frecuenciaCardiaca);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@frecuenciaRespiratoria", frecuenciaRespiratoria);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@peso", peso);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@talla", talla);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@indiceMasaCorporal", indiceMasaCorporal);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@glucemiaCapilar", glucemiaCapilar);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@ingestaAlimentos", ingestaAlimentos);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@tipoDeposicion", tipoDeposicion);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@nauseas", nauseas);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@prurito", prurito);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@observaciones", observaciones);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoEntradaAlimentos", balanceHidricoEntradaAlimentos);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoEntradaLiquidos", balanceHidricoEntradaLiquidos);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoFluidoterapia", balanceHidricoFluidoterapia);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoHemoderivados", balanceHidricoHemoderivados);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoOtrosEntrada", balanceHidricoOtrosEntrada);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoDiuresis", balanceHidricoDiuresis);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoVomitos", balanceHidricoVomitos);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoHeces", balanceHidricoHeces);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoSondaNasogastrica", balanceHidricoSondaNasogastrica);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoDrenajes", balanceHidricoDrenajes);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@balanceHidricoOtrasPerdidas", balanceHidricoOtrasPerdidas);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@totalBalanceHidrico", totalBalanceHidrico);
                    cmdInsertarNotasEnfermeria.Parameters.AddWithValue("@nhc", nhcPaciente);

                    cmdInsertarNotasEnfermeria.ExecuteNonQuery();
                }

                MessageBox.Show("Notas de enfermeria registradas correctamente");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al registrar las notas de enfermeria: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se utiliza para registrar la hoja de prescripcion de un pacinete
        /// </summary>
        public void RegistrarHojaPrescripcion(int nhcPaciente, string especialidad, string principioActivo,
            string dosis, string viaAdministracion, string frecuencia, DateTime fechaFinTratamiento)
        {
            try
            {
                string insertarHojaPrescripcionQuery = @"INSERT INTO hoja_prescripcion (especialidad, principio_activo, 
                    dosis, via_administracion, frecuencia, fecha_fin_tratamiento, nhc) VALUES (@especialidad, @principioActivo, 
                    @dosis, @viaAdministracion, @frecuencia, @fechaFinTratamiento, @nhcPaciente)";

                using (MySqlCommand cmdInsertarHojaPrescripcion = new MySqlCommand(insertarHojaPrescripcionQuery, conexion))
                {
                    cmdInsertarHojaPrescripcion.Parameters.AddWithValue("@especialidad", especialidad);
                    cmdInsertarHojaPrescripcion.Parameters.AddWithValue("@principioActivo", principioActivo);
                    cmdInsertarHojaPrescripcion.Parameters.AddWithValue("@dosis", dosis);
                    cmdInsertarHojaPrescripcion.Parameters.AddWithValue("@viaAdministracion", viaAdministracion);
                    cmdInsertarHojaPrescripcion.Parameters.AddWithValue("@frecuencia", frecuencia);
                    cmdInsertarHojaPrescripcion.Parameters.AddWithValue("@fechaFinTratamiento", fechaFinTratamiento);
                    cmdInsertarHojaPrescripcion.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    cmdInsertarHojaPrescripcion.ExecuteNonQuery();
                }

                MessageBox.Show("Hoja de prescripcion registrada correctamente");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al registrar la hoja de prescripción: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se utiliza para obtener la lista de hojas de prescripcion de un pacinete
        /// </summary>
        public List<List<string>> ObtenerHojasPrescripcionPorPaciente(int nhcPaciente)
        {
            List<List<string>> listaHojasPrescripcion = new List<List<string>>();

            try
            {
                string consultarHojasPrescripcionQuery = @"SELECT id_hoja, especialidad, principio_activo, dosis, 
                    via_administracion, frecuencia, fecha_fin_tratamiento FROM hoja_prescripcion WHERE nhc = @nhcPaciente";

                using (MySqlCommand cmdConsultarHojasPrescripcion = new MySqlCommand(consultarHojasPrescripcionQuery, conexion))
                {
                    cmdConsultarHojasPrescripcion.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    using (MySqlDataReader reader = cmdConsultarHojasPrescripcion.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> hojaPrescripcion = new List<string>
                    {
                        reader["id_hoja"].ToString(),
                        reader["especialidad"].ToString(),
                        reader["principio_activo"].ToString(),
                        reader["dosis"].ToString(),
                        reader["via_administracion"].ToString(),
                        reader["frecuencia"].ToString(),
                        ((DateTime)reader["fecha_fin_tratamiento"]).ToString("dd-MM-yyyy")
                    };

                            listaHojasPrescripcion.Add(hojaPrescripcion);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener las hojas de prescripcion: " + ex.Message);
            }

            return listaHojasPrescripcion;
        }

        /// <summary>
        /// Metodo que se utiliza para registrar la patologia de un pacinete
        /// </summary>
        public void RegistrarPatologia(int nhcPaciente, DateTime fechaSintomas, DateTime fechaDiagnostico, string sintomas,
        string diagnostico, string especialidad, string codificacion)
        {
            try
            {
                string insertarPatologiaQuery = @"INSERT INTO patologia (fecha_inicio, fecha_diagnostico, sintomas, diagnostico, 
                    especialidad, codificacion, nhc) VALUES ( @fechaInicio, @fechaDiagnostico, @sintomas, @diagnostico, 
                    @especialidad, @codificacion, @nhcPaciente)";

                using (MySqlCommand cmdInsertarPatologia = new MySqlCommand(insertarPatologiaQuery, conexion))
                {
                    cmdInsertarPatologia.Parameters.AddWithValue("@fechaInicio", fechaSintomas);
                    cmdInsertarPatologia.Parameters.AddWithValue("@fechaDiagnostico", fechaDiagnostico);
                    cmdInsertarPatologia.Parameters.AddWithValue("@sintomas", sintomas);
                    cmdInsertarPatologia.Parameters.AddWithValue("@diagnostico", diagnostico);
                    cmdInsertarPatologia.Parameters.AddWithValue("@especialidad", especialidad);
                    cmdInsertarPatologia.Parameters.AddWithValue("@codificacion", codificacion);
                    cmdInsertarPatologia.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    cmdInsertarPatologia.ExecuteNonQuery();
                }
                MessageBox.Show("Informacion de patologia registrada correctamente");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al registrar la informacion de patologia: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se utiliza para obtener la lista de patologias de un pacinete
        /// </summary>
        public List<List<string>> ObtenerPatologiasPorPaciente(int nhcPaciente)
        {
            List<List<string>> listaPatologias = new List<List<string>>();

            try
            {
                string consultarPatologiasQuery = @"SELECT id_patologia, fecha_inicio, fecha_diagnostico, sintomas, diagnostico, 
                    especialidad, codificacion FROM patologia WHERE nhc = @nhcPaciente";

                using (MySqlCommand cmdConsultarPatologias = new MySqlCommand(consultarPatologiasQuery, conexion))
                {
                    cmdConsultarPatologias.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    using (MySqlDataReader reader = cmdConsultarPatologias.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> patologia = new List<string>
                    {
                        reader["id_patologia"].ToString(),
                        ((DateTime)reader["fecha_inicio"]).ToString("dd-MM-yyyy"),
                        ((DateTime)reader["fecha_diagnostico"]).ToString("dd-MM-yyyy"),
                        reader["sintomas"].ToString(),
                        reader["diagnostico"].ToString(),
                        reader["especialidad"].ToString(),
                        reader["codificacion"].ToString()
                    };

                            listaPatologias.Add(patologia);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener la informacion de patologias: " + ex.Message);
            }

            return listaPatologias;
        }

        /// <summary>
        /// Metodo que se utiliza para obtener la lista de ingresos de un pacinete
        /// </summary>
        public List<List<string>> ObtenerIngresosPorPaciente(int nhcPaciente)
        {
            List<List<string>> listaIngresos = new List<List<string>>();

            try
            {
                string consultarIngresosQuery = @"SELECT id_alta, estado_hnc, id_ambito, Fecha, Hora, id_cama, nhc FROM ingresos WHERE nhc = @nhcPaciente";

                using (MySqlCommand cmdConsultarIngresos = new MySqlCommand(consultarIngresosQuery, conexion))
                {
                    cmdConsultarIngresos.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    using (MySqlDataReader reader = cmdConsultarIngresos.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> ingreso = new List<string>
                    {
                        reader["id_alta"].ToString(),
                        reader["estado_hnc"].ToString(),
                        reader["id_ambito"].ToString(),
                        ((DateTime)reader["Fecha"]).ToString("dd-MM-yyyy"),
                        ((TimeSpan)reader["Hora"]).ToString(@"hh\:mm"),
                        reader["id_cama"].ToString(),
                        reader["nhc"].ToString()
                    };

                            listaIngresos.Add(ingreso);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener la información de ingresos: " + ex.Message);
            }

            return listaIngresos;
        }

        /// <summary>
        /// Metodo que se utiliza para obtener la lista de altas de un pacinete
        /// </summary>
        public List<List<string>> ObtenerAltasPorPaciente(int nhcPaciente)
        {
            List<List<string>> listaAltas = new List<List<string>>();

            try
            {
                string consultarAltasQuery = @"SELECT id_alta, fecha, hora, motivo, tipo, nhc FROM alta WHERE nhc = @nhcPaciente";

                using (MySqlCommand cmdConsultarAltas = new MySqlCommand(consultarAltasQuery, conexion))
                {
                    cmdConsultarAltas.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    using (MySqlDataReader reader = cmdConsultarAltas.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> alta = new List<string>
                    {
                        reader["id_alta"].ToString(),
                        ((DateTime)reader["fecha"]).ToString("dd-MM-yyyy"),
                        ((TimeSpan)reader["hora"]).ToString(@"hh\:mm"),
                        reader["motivo"].ToString(),
                        reader["tipo"].ToString(),
                        reader["nhc"].ToString()
                    };

                            listaAltas.Add(alta);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener la información de altas: " + ex.Message);
            }

            return listaAltas;
        }

        /// <summary>
        /// Metodo que se utiliza para obtener la lista de notas de enfermeria ingresos de un pacinete
        /// </summary>
        public List<List<string>> ObtenerNotasEnfermeriaPorPaciente(int nhcPaciente)
        {
            List<List<string>> listaNotasEnfermeria = new List<List<string>>();

            try
            {
                string consultarNotasEnfermeriaQuery = @"SELECT id_notas, fecha_toma, hora_toma, temperatura, tension_arterial_sistolica,
            tension_arterial_diastolica, frecuencia_cardiaca, frecuencia_respiratoria, peso, talla, indice_masa_corporal,
            glucemia_capilar, ingesta_alimentos, tipo_deposicion, nauseas, prurito, observaciones FROM notas_enfermeria WHERE nhc = @nhcPaciente";

                using (MySqlCommand cmdConsultarNotasEnfermeria = new MySqlCommand(consultarNotasEnfermeriaQuery, conexion))
                {
                    cmdConsultarNotasEnfermeria.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    using (MySqlDataReader reader = cmdConsultarNotasEnfermeria.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> notaEnfermeria = new List<string>
                    {
                        reader["id_notas"].ToString(),
                        ((DateTime)reader["fecha_toma"]).ToString("dd-MM-yyyy"),
                        ((TimeSpan)reader["hora_toma"]).ToString(@"hh\:mm"),
                        reader["temperatura"].ToString(),
                        reader["tension_arterial_sistolica"].ToString(),
                        reader["tension_arterial_diastolica"].ToString(),
                        reader["frecuencia_cardiaca"].ToString(),
                        reader["frecuencia_respiratoria"].ToString(),
                        reader["peso"].ToString(),
                        reader["talla"].ToString(),
                        reader["indice_masa_corporal"].ToString(),
                        reader["glucemia_capilar"].ToString(),
                        reader["ingesta_alimentos"].ToString(),
                        reader["tipo_deposicion"].ToString(),
                        reader["nauseas"].ToString(),
                        reader["prurito"].ToString(),
                        reader["observaciones"].ToString(),
                    };

                            listaNotasEnfermeria.Add(notaEnfermeria);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener la información de notas de enfermería: " + ex.Message);
            }

            return listaNotasEnfermeria;
        }

        /// <summary>
        /// Metodo que se utiliza para obtener la lista de balances hidricos de un pacinete
        /// </summary>
        public List<List<string>> ObtenerBalancesHidricosPorPaciente(int nhcPaciente)
        {
            List<List<string>> listaNotasEnfermeria = new List<List<string>>();

            try
            {
                string consultarNotasEnfermeriaQuery = @"SELECT id_notas, balance_hidrico_entrada_alimentos,
            balance_hidrico_entrada_liquidos, balance_hidrico_fluidoterapia, balance_hidrico_hemoderivados,
            balance_hidrico_otros_entrada, balance_hidrico_diuresis, balance_hidrico_vomitos, balance_hidrico_heces,
            balance_hidrico_sonda_nasogastrica, balance_hidrico_drenajes, balance_hidrico_otras_perdidas,
            total_balance_hidrico FROM notas_enfermeria WHERE nhc = @nhcPaciente";

                using (MySqlCommand cmdConsultarNotasEnfermeria = new MySqlCommand(consultarNotasEnfermeriaQuery, conexion))
                {
                    cmdConsultarNotasEnfermeria.Parameters.AddWithValue("@nhcPaciente", nhcPaciente);

                    using (MySqlDataReader reader = cmdConsultarNotasEnfermeria.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> notaEnfermeria = new List<string>
                    {
                        reader["id_notas"].ToString(),
                        reader["balance_hidrico_entrada_alimentos"].ToString(),
                        reader["balance_hidrico_entrada_liquidos"].ToString(),
                        reader["balance_hidrico_fluidoterapia"].ToString(),
                        reader["balance_hidrico_hemoderivados"].ToString(),
                        reader["balance_hidrico_otros_entrada"].ToString(),
                        reader["balance_hidrico_diuresis"].ToString(),
                        reader["balance_hidrico_vomitos"].ToString(),
                        reader["balance_hidrico_heces"].ToString(),
                        reader["balance_hidrico_sonda_nasogastrica"].ToString(),
                        reader["balance_hidrico_drenajes"].ToString(),
                        reader["balance_hidrico_otras_perdidas"].ToString(),
                        reader["total_balance_hidrico"].ToString(),
                    };

                            listaNotasEnfermeria.Add(notaEnfermeria);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener la información de notas de enfermería: " + ex.Message);
            }

            return listaNotasEnfermeria;
        }

        /// <summary>
        /// Metodo que se utiliza para comprobar si un usuario es administrador
        /// </summary>
        public bool EsAdministrador(string usuario, string clave)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                MessageBox.Show("No se ha establecido una conexión válida.");
                return false;
            }

            try
            {
                string consulta = "SELECT id_usuario FROM Usuarios WHERE email = @usuario AND password = @clave";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int idUsuario = Convert.ToInt32(result);

                        return idUsuario == 1;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al verificar el usuario administrador: " + ex.Message);
                return false;
            }
        }
    }
}
