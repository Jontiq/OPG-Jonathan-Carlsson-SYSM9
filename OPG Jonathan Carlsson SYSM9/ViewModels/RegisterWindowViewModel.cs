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
    public class RegisterWindowViewModel : BaseViewModel
    {
        //props
        //private readonly just because we don't want it to be able to change outside of class, and only to be "created" in the constructor
        private readonly UserManager _userManager;
        private readonly NavigationManager _navigationManager;
        public ICommand CancelCommand { get; }

        //constructor
        public RegisterWindowViewModel()
        {
            _userManager = (UserManager)Application.Current.Resources["UserManager"];
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];
            //LoginCommand = new RelayCommand(execute => ExecuteLogin(), canExecute => CanExecuteLogin());
            CancelCommand = new RelayCommand(execute => ExecuteCancel());

        }

        //methods
        //Cancels the registration, opens the LoginWindow and closes the RegisterWindow without saving anything.
        private void ExecuteCancel()
        {
            _navigationManager.ShowWindow<LoginWindow>();
            _navigationManager.CloseWindow<RegisterWindow>();
        }

    }
}
