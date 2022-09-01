using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MoreniApp.Model
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateUser(NetworkCredential credential);
        Task<bool> AddUser(Usuario usuario);
        Task<bool> EditUser(Usuario usuario);
        Task<Usuario> GetById(int id);
        Task<CuentaUsuario> GetByUsername(string usr);
        Task<IEnumerable> GetAll();
    }
}
