using MoreniApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;

namespace MoreniApp.Repositories
{
    public class ArmadorRepository : RepositoryBase, IArmadorRepository
    {
        async Task<IEnumerable> IArmadorRepository.GetAll()
        {
            string query = "SELECT * FROM ARMADORES;";
            try
            {
                using (var conn = GetConnection())
                using (var command = new OleDbCommand( query,conn )) 
                {
                    List<Armador> list = new List<Armador>();
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Armador arm = new Armador
                        {
                            Id = reader.GetInt32(0),
                            Razon_social = reader[1].ToString(),
                            Nombre_fantasia = reader[2].ToString(),
                            Cuit = reader[3].ToString(),
                            Direccion = reader[4].ToString(),
                            Ciudad = reader[5].ToString(),
                            Email = reader[6].ToString(),
                            Telefono = reader[7].ToString()
                        };
                        list.Add(arm);
                    }
                    return list;
                }
            }
            catch( Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        async Task<Armador> IArmadorRepository.GetById(int id)
        {
            string query = "SELECT * FROM ARMADORES WHERE ID = @i;";
            try
            {
                using var conn = GetConnection();
                using var command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@i", id);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    Armador arm = new Armador
                    {
                        Id = reader.GetInt32(0),
                        Razon_social = reader[1].ToString(),
                        Nombre_fantasia = reader[2].ToString(),
                        Cuit = reader[3].ToString(),
                        Direccion = reader[4].ToString(),
                        Ciudad = reader[5].ToString(),
                        Email = reader[6].ToString(),
                        Telefono = reader[7].ToString()
                    };
                    return arm;
                }
                return null;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        async Task<Armador> IArmadorRepository.GetByRazonSocial(string rs)
        {
            string query = "SELECT * FROM ARMADORES WHERE RAZON_SOCIAL = @rs;";
            try
            {
                using var conn = GetConnection();
                using var command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@rs", rs);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    Armador arm = new Armador
                    {
                        Id = reader.GetInt32(0),
                        Razon_social = reader[1].ToString(),
                        Nombre_fantasia = reader[2].ToString(),
                        Cuit = reader[3].ToString(),
                        Direccion = reader[4].ToString(),
                        Ciudad = reader[5].ToString(),
                        Email = reader[6].ToString(),
                        Telefono = reader[7].ToString()
                    };
                    return arm;
                }
                return null;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        async Task<bool> IArmadorRepository.AddArmador(Armador arm)
        {
            string sql = "INSERT INTO ARMADORES (razon_social,nombre_fantasia,cuit,direccion,ciudad,email,telefono,estado) " +
                        "VALUES ('"
                         + arm.Razon_social + "','"
                         + arm.Nombre_fantasia + "','"
                         + arm.Cuit + "','"
                         + arm.Direccion + "','"
                         + arm.Ciudad + "','"
                         + arm.Email + "','"
                         + arm.Telefono + "','"
                         + arm.Estado + "'"
                         + ")";
            try
            {
                using var conn = GetConnection();
                using var command = new OleDbCommand(sql, conn);
                await command.ExecuteNonQueryAsync(); // EJECUTA INSERT ARMADOR
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        async Task<bool> IArmadorRepository.UpdateArmador(int id, Armador arm)
        {
            if (id == -1)
                return false;
            string sql = "UPDATE ARMADORES SET " +
            "razon_social = '" + arm.Razon_social +
            "', nombre_fantasia = '" + arm.Nombre_fantasia +
            "', cuit = '" + arm.Cuit +
            "', direccion = '" + arm.Direccion +
            "', ciudad = '" + arm.Ciudad +
            "', email = '" + arm.Email +
            "', telefono = '" + arm.Telefono +
            "'  WHERE id = " + id;
            try
            {
                using var conn = GetConnection();
                using var cmd = new OleDbCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public string GetError()
        {
            return Error;
        }
    }
}
