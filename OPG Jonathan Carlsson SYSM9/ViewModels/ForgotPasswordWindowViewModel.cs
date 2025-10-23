using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels
{
    public class ForgotPasswordWindowViewModel : BaseViewModel
    {

        //props
        private readonly NavigationManager _navigationManager;
        //construcor
        public ForgotPasswordWindowViewModel()
        {
            CancelCommand = new RelayCommand(execute => ExecuteCancel());
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
        }

        public ICommand CancelCommand { get; }
        //methods
        private void ExecuteCancel()
        {
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<ForgotPasswordWindow>();
        }
    }
}
