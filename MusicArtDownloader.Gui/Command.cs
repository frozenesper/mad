using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicArtDownloader.Gui
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>from: http://enumeratethis.com/2012/06/14/asynchronous-commands-in-metro-wpf-silverlight/ </remarks>
    public class Command : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;
        private bool isExecuting;

        public Command(Action execute) : this(execute, () => true) { }

        public Command(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            // if the command is not executing, execute the users' can execute logic
            return !isExecuting && canExecute();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            // tell the button that we're now executing...
            isExecuting = true;
            OnCanExecuteChanged();
            try
            {
                // execute user code
                execute();
            }
            finally
            {
                // tell the button we're done
                isExecuting = false;
                OnCanExecuteChanged();
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null) CanExecuteChanged(this, new EventArgs());
        }
    }
}
