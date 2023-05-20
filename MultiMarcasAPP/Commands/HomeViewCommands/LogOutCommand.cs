using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.HomeViewCommands
{
    public class LogOutCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if(parameter != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                ((Window)parameter).Close();
            }
        }
    }
}
