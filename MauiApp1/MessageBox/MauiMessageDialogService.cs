using SharedP.Program.Shared.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Client.MessageBox
{
    class MauiMessageDialogService : IMessageDialogService
    {
        public void ShowMessage(string message)
        {
            Shell.Current.DisplayAlert("Message", message, "OK");
        }
    }
}
