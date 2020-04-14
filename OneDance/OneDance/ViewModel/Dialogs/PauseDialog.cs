using MaterialDesignThemes.Wpf;
using OneDance.Core.Enums;
using OneDance.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace OneDance.ViewModel.Dialogs
{
    public class PauseDialog
    {
        public PauseDialog(Action<object> endAction)
        {
            EndAction = endAction;
        }

        private Action<object> EndAction
        {
            get;
            set;
        }
        
        private UserControl m_pauseDialog;

        private UserControl View
        {
            get { return m_pauseDialog ?? (m_pauseDialog = new PauseDialogView()); }
        }

        private DialogHost dialogInstance;

        public async void Execute()
        {
            if (dialogInstance != null)
                await dialogInstance.ShowDialog(View, ClosingEventHandler);
           
            else
                await DialogHost.Show(View, "PauseDialog", ExtendedOpenedEventHandler, ClosingEventHandler);
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            dialogInstance  = eventargs.Source as DialogHost;
        }

        public void Close()
        {
            if (dialogInstance == null)
                return;

            if (dialogInstance.IsOpen)
                dialogInstance.IsOpen = false;
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            EndAction.Invoke(null);
        }
    }
}
