using MoreniApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace MoreniApp.Repositories
{
    public class UsuarioRepository : RepositoryBase, IUserRepository
    {
        async Task<bool> IUserRepository.AuthenticateUser(NetworkCredential credential)
        {
            string query = "SELECT nombre,contrasena FROM USUARIOS WHERE nombre = @usrname AND contrasena = @passwd;";
            try
            {
                using var conn = GetConnection();
                using var command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@usrname", credential.UserName);
                command.Parameters.AddWithValue("@passwd", credential.Password);
                var reader = await command.ExecuteReaderAsync();
                return reader.HasRows;
            }
            catch(Exception ex)
            {
                Error = ex.Message;
                MessageBox.Show("Error autenticando usuario: " + Error);
                return false;
            }
        }
        async Task<Usuario> IUserRepository.GetById(int id)
        {
            string query = "SELECT id, nombre, rol, estado FROM usuarios WHERE id = @i;";
            try
            {
                using( var conn = GetConnection())
                using (var command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@i", id);
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        Usuario usr = new Usuario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Rol = reader.GetString(2),
                            Estado = reader.GetString(3)
                        };
                        return usr;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
               Error = ex.Message;
               return null;
            }
        }

        async Task<IEnumerable> IUserRepository.GetAll()
        {
            string query = "SELECT ID,NOMBRE,ROL,ESTADO FROM USUARIOS";
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using( var conn = GetConnection())
                using (var command = new OleDbCommand(query, conn))
                {
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Usuario usr = new Usuario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Rol = reader.GetString(2),
                            Estado = reader.GetString(3)
                        };
                        usuarios.Add(usr);
                    }
                    return usuarios;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return usuarios;
            }
        }

        async Task<CuentaUsuario> IUserRepository.GetByUsername(string username )
        {
            string query = "SELECT nombre FROM usuarios WHERE nombre = @usr;";
            try
            {
                using (var conn = GetConnection())
                using (var command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@usr", username);
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        CuentaUsuario cusr = new CuentaUsuario
                        {
                            Username = reader.GetString(0),
                            DisplayName = reader.GetString(0),
                            ProfilePicture = null
                        };
                        return cusr;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        Task<bool> IUserRepository.AddUser(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserRepository.EditUser(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
