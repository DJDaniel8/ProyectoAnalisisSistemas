using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands
{
    public class SelectRowCommand : CommandBase
    {
        Action _CreateWindow;

        public SelectRowCommand(Action createWindow)
        {
            _CreateWindow = createWindow;
        }

        public override void Execute(object? parameter)
        {
            _CreateWindow();
        }
    }
}
