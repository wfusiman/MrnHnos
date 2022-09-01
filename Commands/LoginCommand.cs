using MoreniApp.Model;
using MoreniApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoreniApp.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly IUserRepository _userRepository;

        public LoginCommand( LoginViewModel viewModel, IUserRepository userRepository )
        {
            _viewModel = viewModel;
            _userRepository = userRepository;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.ErrorMessage = string.Empty;
            try
            {
                var valid = await _userRepository.AuthenticateUser(new NetworkCredential(_viewModel.Username, _viewModel.Password));
                if (valid) // si es valido se oculpa la vista login.
                { 
                    Thread.CurrentPrincipal = new GenericPrincipal( new GenericIdentity(_viewModel.Username), null);
                    _viewModel.IsViewVisible = false;
                }
                else // si no se muestra el error
                    _viewModel.ErrorMessage = "Usuario y/o contraseña invalida";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Error validando usuario";
            }
            _viewModel.IsLoading = false;
        }
    }
}
