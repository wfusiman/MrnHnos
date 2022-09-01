using MoreniApp.Model;
using MoreniApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MoreniApp.ViewModel
{
    public class ArmadorEditViewModel: ViewModelBase
    {
        // Fields 
        private int _Id;
        private string _razon_social;
        private string _nombre_fantasia;
        private string _cuit;
        private string _direccion;
        private string _ciudad;
        private string _email;
        private string _telefono;
        private string _estado;

        // Properties
        public int Id 
        { 
            get => _Id; 
            set { _Id = value; OnPropertyChanged(nameof(Id)); } 
        }
        public string Razon_social 
        { 
            get => _razon_social; 
            set { _razon_social = value; OnPropertyChanged(nameof(Razon_social)); } 
        }
        public string Nombre_fantasia 
        { 
            get => _nombre_fantasia;
            set { _nombre_fantasia = value; OnPropertyChanged(nameof(Nombre_fantasia)); }
        }

        public string Cuit
        {
            get => _cuit;
            set { _cuit = value; OnPropertyChanged(nameof(Cuit)); }
        }
        public string Direccion 
        { 
            get => _direccion;
            set { _direccion = value; OnPropertyChanged(nameof(Direccion)); }
        }
        public string Ciudad
        {
            get => _ciudad;
            set { _ciudad = value; OnPropertyChanged(nameof(Ciudad)); }
        }
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        public string Telefono
        {
            get => _telefono;
            set { _telefono = value; OnPropertyChanged(nameof(Telefono)); }
        }
        public string Estado
        {
            get => _estado;
            set { _estado = value; OnPropertyChanged(nameof(Estado)); }
        }

        public string mode; // mode = NUEVO, VISTA O EDICION.
        private IArmadorRepository ArmadorRepository;
        // Commnads
        public ICommand SaveArmador { get; }
        public ICommand UpdateArmador { get; }
        public ICommand EditArmador { get; }
        public ArmadorEditViewModel( int idArmador )
        {
            ArmadorRepository = new ArmadorRepository();
            if (idArmador == -1) // modo NUEVO
                NuevoArmador();
            else
                VerArmador(idArmador);
        }

        private void NuevoArmador()
        {
            mode = "NUEVO";
            Id = -1;
            Razon_social = "";
            Nombre_fantasia = "";
            Cuit = "";
            Direccion = "";
            Ciudad = "";
            Email = "";
            Telefono = "";
            Estado = "";
        }

        async private void VerArmador( int idArmador )
        {
            mode = "VER";
            var arm = await ArmadorRepository.GetById(idArmador);
            if (arm != null)  // se encontro el armador
            {
                Id = arm.Id;
                Razon_social = arm.Razon_social;
                Nombre_fantasia = arm.Nombre_fantasia;
                Cuit = arm.Cuit;
                Direccion = arm.Direccion;
                Ciudad = arm.Ciudad;
                Email = arm.Email;
                Telefono = arm.Telefono;
                Estado = arm.Estado;
            }
        }
        
    }
}
