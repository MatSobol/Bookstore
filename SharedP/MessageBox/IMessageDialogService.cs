using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedP.Program.Shared.MessageBox
{
    public interface IMessageDialogService
    {
        void ShowMessage(string message);
    }
}
