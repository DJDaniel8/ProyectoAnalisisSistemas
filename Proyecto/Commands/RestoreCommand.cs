using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands
{
    public class RestoreCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {
                SystemCommands.RestoreWindow((Window)parameter);
            }
        }
    }
}
