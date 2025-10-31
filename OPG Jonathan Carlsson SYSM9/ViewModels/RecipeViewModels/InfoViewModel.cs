using MVVM_KlonaMIg.MVVM;
using OPG_Jonathan_Carlsson_SYSM9.Managers;
using OPG_Jonathan_Carlsson_SYSM9.Views;
using OPG_Jonathan_Carlsson_SYSM9.Views.RecipeWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Jonathan_Carlsson_SYSM9.ViewModels.RecipeViewModels
{
    public class InfoViewModel : BaseViewModel
    {
        private readonly NavigationManager _navigationManager;
        public ICommand GoBackCommand { get; }

        public InfoViewModel()
        {
            _navigationManager = (NavigationManager)Application.Current.Resources["NavigationManager"];

            GoBackCommand = new RelayCommand(execute => ExecuteGoBack());
        }


        private void ExecuteGoBack()
        {
            _navigationManager.CreateAndShowWindow<RecipeListWindow>();
            _navigationManager.CloseWindow<InfoWindow>();
        }

    }
}
