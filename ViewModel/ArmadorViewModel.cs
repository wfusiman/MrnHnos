using MoreniApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreniApp.ViewModel
{
    public class ArmadorViewModel
    {
        private readonly Armador _armador;

        public int Id => _armador.Id;
        public string Razon_social => _armador.Razon_social;
        public string Nombre_fantasia => _armador.Nombre_fantasia;
        public string Cuit => _armador.Cuit;
        public string Direccion => _armador.Direccion;
        public string Ciudad => _armador.Ciudad;
        public string Email => _armador.Email;
        public string Telefono => _armador.Telefono;
        public string Estado => _armador.Estado;

        public ArmadorViewModel( Armador arm )
        {
            _armador = arm;
        }
    }
}
