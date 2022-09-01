using FontAwesome.Sharp;
using MoreniApp.Model;
using MoreniApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace MoreniApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private CuentaUsuario _currentUserAccount;
        private IUserRepository UserRepository;

        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        public CuentaUsuario CurrentUserAccount
        {
            get => _currentUserAccount;
            set { _currentUserAccount = value; OnPropertyChanged(nameof(CurrentUserAccount)); }
        }

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); }
        }
        public string Caption
        {
            get => _caption;
            set { _caption = value; OnPropertyChanged(nameof(Caption)); }
        }
        public IconChar Icon
        {
            get => _icon;
            set { _icon = value; OnPropertyChanged(nameof(Icon)); }
        }

        async private void LoadCurrentUserData()
        {
            var user = await UserRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Usuario {user.Username}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Usuario invalido, necesita loguearse";
            }
        }

        // Commands

        public ICommand ShowHomeViewCommand { get; set; } 
        public ICommand ShowUsuarioViewCommand { get; set; }
        public ICommand ShowArmadoresViewCommand { get; set; }
        public MainViewModel()
        {
            UserRepository = new UsuarioRepository();
            CurrentUserAccount = new CuentaUsuario();
            // Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowUsuarioViewCommand = new ViewModelCommand(ExecuteShowUsuarioViewCommand);
            ShowArmadoresViewCommand = new ViewModelCommand(ExecuteShowArmadoresViewCommand);

            // Default view
            ExecuteShowHomeViewCommand(null);
            LoadCurrentUserData();
        }

        private void ExecuteShowArmadoresViewCommand(object obj)
        {
            CurrentChildView = new ArmadoresListViewModel();
            Caption = "Armadores";
            Icon = IconChar.Anchor;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Principal";
            Icon = IconChar.Home;
        }

        private void ExecuteShowUsuarioViewCommand(object obj)
        {
            CurrentChildView = new UsuariosListViewModel();
            Caption = "Usuarios";
            Icon = IconChar.Users;
        }
    }
}
