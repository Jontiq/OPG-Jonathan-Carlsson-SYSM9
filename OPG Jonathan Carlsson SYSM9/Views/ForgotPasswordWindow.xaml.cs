using OPG_Jonathan_Carlsson_SYSM9.ViewModels;
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

namespace OPG_Jonathan_Carlsson_SYSM9.Views
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
            ForgotPasswordWindowViewModel viewModel = new ForgotPasswordWindowViewModel();
            DataContext = viewModel;
        }
        private void WindowClosed(object sender, EventArgs e)
        {
            if (DataContext is ForgotPasswordWindowViewModel vm)
            {
                vm.CancelCommand.Execute(null);
            }
        }
    }
}
