using MoreniApp.Model;
using MoreniApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MoreniApp.ViewModel
{
    public class UsuariosListViewModel: ViewModelBase
    {
        // Fields
        private ObservableCollection<UsuarioViewModel> _usuarios;

        public IEnumerable<UsuarioViewModel> Usuarios => _usuarios;


        private IUserRepository UserRepository;

        public UsuariosListViewModel()
        {
            UserRepository = new UsuarioRepository();
            LoadListUsuarios();
        }

        async void LoadListUsuarios()
        {
            _usuarios = new ObservableCollection<UsuarioViewModel>();
            var usuarios = await UserRepository.GetAll();
            foreach(Usuario usr in usuarios)
            {
                _usuarios.Add(new UsuarioViewModel(usr));
            }
        }


    }
}
