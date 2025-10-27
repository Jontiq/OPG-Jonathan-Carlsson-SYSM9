using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPG_Jonathan_Carlsson_SYSM9.Managers
{
    //this class is to be used for a "simpler" and cleaner way of navigating between windows
    public class NavigationManager
    {
        //"T" is for "generic type", meaning that the type is decided when using the method. "T" must inherit from "Window", and if it does, create "new" "T".
        //"A cup is generic. You can specify it further and say a coffee cup or a tea cup. In other words, Cup<T> can be a Cup<Coffee> or a Cup<Tea>"

        //Creates and shows new window
        public void CreateAndShowWindow<T>() where T : Window, new()
        {
            //Creates window
            T window = new T();
            window.Show();
        }

        //Closes "T" if it inherit from Window
        public void CloseWindow<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is T)
                {
                    window.Close();
                    break;
                }
            }
        }
        //Closes all windows except T
        public void CloseAllExcept<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is not T)
                    window.Close();
            }
        }
    }
}
