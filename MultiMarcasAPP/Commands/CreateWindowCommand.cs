using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands
{
    public class CreateWindowCommand : CommandBase
    {
        Action _CreateWindow;

        public CreateWindowCommand(Action createWindow)
        {
            _CreateWindow = createWindow;
        }

        public override void Execute(object? parameter)
        {
            _CreateWindow();
        }
    }
}
