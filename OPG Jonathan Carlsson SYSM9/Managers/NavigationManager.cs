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
        //"T" is for generic type, meaning that the type is decided when using the method. "T" must inherit from "Window", and if it does, create "new" "T".
        
        //Creates a new window of the "T" window.
        public void CreateWindow<T>() where T : Window, new()
        {
            T window = new T();
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

        //Hides "T" if it inherit from Window
        public void HideWindow<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is T)
                {
                    window.Hide();
                    break;
                }
            }
        }

        //Shows "T" if it inherit from Window and already exists (AKA have been created)
        public void ShowWindow<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is T)
                {
                    window.Show();
                    break;
                }
            }
        }
    }
}
