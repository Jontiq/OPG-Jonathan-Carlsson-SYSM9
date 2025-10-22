using OPG_Jonathan_Carlsson_SYSM9.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPG_Jonathan_Carlsson_SYSM9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginWindowViewModel viewModel = new LoginWindowViewModel();
            DataContext = viewModel;
        }

        //Is being used by textbox "pwd" in LoginWindow.xaml, helps us set the password input through an event.
        private void Pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginWindowViewModel viewModel)
                viewModel.PasswordInput = Pwd.Password;
        }
    }
}