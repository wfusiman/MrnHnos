using MoreniApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoreniApp.View
{
    /// <summary>
    /// Lógica de interacción para LoadingView.xaml
    /// </summary>
    public partial class LoadingView : Window
    {
        BackgroundWorker backgroundWorker1;
        private bool statusConnection = false;
        public LoadingView()
        {
            InitializeComponent();
            InitBackgroundWorker();

            btn_aceptar.Visibility = Visibility.Collapsed;

            statusConnection = (DB.GetConnection() != null);

            backgroundWorker1.RunWorkerAsync();
        }

        private void InitBackgroundWorker()
        {
            this.tbMensaje2.Text = "Conectando a base de datos";
            backgroundWorker1 = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.loginWindow = new LoginView();
            if (statusConnection)
            {
                tbMensaje2.Text = "Conexion exitosa";
                var loginView = new LoginView();
                loginView.Show();
                loginView.IsVisibleChanged += (s, ev) =>
                {
                    if (loginView.IsVisible == false && loginView.IsLoaded)
                    {
                        var mainView = new MainView();
                        mainView.Show();
                        loginView.Close();
                    }
                };
                //loginWindow.Show();
                //loginWindow.Activate();
                Hide();
                Close();
            }
            else
            {
                btn_aceptar.Visibility = Visibility.Visible;
                pbCargando.IsIndeterminate = false;
                pbCargando.Maximum = 100;
                tbMensaje2.Text = "ERROR iniciando aplicacion";
                tbMensaje2.Text += ": " + DB.GetError();

            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                System.Threading.Thread.Sleep(30);
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            pbCargando.Value = e.ProgressPercentage;
            tbMensaje2.Text = "conectando a base de datos " + e.ProgressPercentage.ToString() + "%";
        }

        private void Btn_aceptar_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Close();
            Environment.Exit(0);
        }
    }
}
