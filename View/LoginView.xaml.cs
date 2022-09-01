
using MoreniApp.Helpers;
using System;
using System.Windows;
using System.Windows.Input;

namespace MoreniApp.View
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            tb_user.Focus();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void FocusControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            FocusControl.checkMoveNext(e);
        }

    }
}
