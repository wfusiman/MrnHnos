using MoreniApp.Model;
using MoreniApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MoreniApp.ViewModel
{
    public class ArmadoresListViewModel: ViewModelBase
    {
        // Fields
        private ObservableCollection<ArmadorViewModel> _armadores;

        public IList<ArmadorViewModel> Armadores => _armadores;

        public ICollectionView ArmadoresListView { get; set; }

        public ArmadorViewModel ArmadorSeleccionado { get; set; }

        private readonly IArmadorRepository ArmadorRepository;

        private string _filtro = string.Empty;
        public string Filtro
        {
            get { return _filtro; }
            set
            {
                _filtro = value;
                OnPropertyChanged(nameof(Filtro));
                ArmadoresListView.Refresh();
            }
        }

        //-> Commands
        public ICommand ListArmadoresCommand { get; }
        public ICommand ViewArmadorCommand { get; }
        public ICommand NewArmadorCommand { get; }

        //Constructor
        public ArmadoresListViewModel()
        {
            ArmadorRepository = new ArmadorRepository();
            ListArmadoresCommand = new ViewModelCommand(ExecuteListArmadorCommand);
            ViewArmadorCommand = new ViewModelCommand(ExecuteViewArmadorCommand, CanExecuteViewArmadorCommand);
            NewArmadorCommand = new ViewModelCommand(ExecuteNewArmadorCommand, CanExecuteNewArmadorCommand);

            ExecuteListArmadorCommand(null);
        }
        private void ExecuteNewArmadorCommand(object obj)
        {
            MessageBox.Show("Nuevo armador");
        }

        private bool CanExecuteNewArmadorCommand(object obj)
        {
            return true;
        }

        private void ExecuteViewArmadorCommand(object obj)
        {
            MessageBox.Show("Armador seleccionado  " + ArmadorSeleccionado.Id);
        }

        private bool CanExecuteViewArmadorCommand(object obj)
        {
            return (ArmadorSeleccionado != null);
        }

        async private void ExecuteListArmadorCommand(object obj)
        {
            _armadores = new ObservableCollection<ArmadorViewModel>();
            var armadores = await ArmadorRepository.GetAll();
            if (armadores == null)
                MessageBox.Show("Error recuperado lista de armadores: " + ArmadorRepository.GetError());
            else
            {
                ArmadoresListView = CollectionViewSource.GetDefaultView(Armadores);
                ArmadoresListView.Filter = FiltrarArmadores;
                foreach (Armador arm in armadores)
                {
                    ArmadorViewModel avm = new ArmadorViewModel(arm);
                    _armadores.Add(avm);
                }
                
            }
        }

        private bool FiltrarArmadores(object obj)
        {
            if (obj is ArmadorViewModel avm)
            {
                return avm.Razon_social.Contains(Filtro, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
