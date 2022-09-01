using MoreniApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreniApp.ViewModel
{
    public class UsuarioViewModel : ViewModelBase
    {
        private readonly Usuario _usuario;
        public int Id => _usuario.Id;

        public string Nombre => _usuario.Nombre;

        public string Rol => _usuario.Rol;

        public string Estado => _usuario.Estado;

        public UsuarioViewModel( Usuario usr )
        {
            _usuario = usr;
        }
    }
}
