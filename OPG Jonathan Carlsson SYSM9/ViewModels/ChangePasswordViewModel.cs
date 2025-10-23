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
    public class ChangePasswordViewModel : BaseViewModel
    {
        //props
        private readonly NavigationManager _navigationManager;

        public ICommand CancelCommand { get; }

        //constructor
        public ChangePasswordViewModel()
        {
            //_navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            CancelCommand = new RelayCommand(execute => ExecuteCancel());
        }
        private void ExecuteCancel()
        {
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<ForgotPasswordWindow>();
        }
    }
}
