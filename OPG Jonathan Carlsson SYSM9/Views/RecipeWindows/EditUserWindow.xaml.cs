using OPG_Jonathan_Carlsson_SYSM9.ViewModels;
using OPG_Jonathan_Carlsson_SYSM9.ViewModels.RecipeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OPG_Jonathan_Carlsson_SYSM9.Views.RecipeWindows
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow()
        {
            InitializeComponent();
            EditUserViewModel viewModel = new EditUserViewModel();
            DataContext = viewModel;
        }

        //Is being used by textbox "Pwd" in RegisterWindow.xaml, helps us set the password input through an event.
        private void Pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditUserViewModel viewModel)
            {
                viewModel.PasswordInput = Pwd.Password;
                viewModel.CheckPasswordRules();
            }

        }
        //Is being used by textbox "PwdConfirm" in RegisterWindow.xaml, helps us set the PasswordConfirm input through an event.
        private void PwdConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditUserViewModel viewModel)
            {
                viewModel.ConfirmPwInput = PwdConfirm.Password;
                viewModel.CheckPasswordRules();
            }
        }
    }
}