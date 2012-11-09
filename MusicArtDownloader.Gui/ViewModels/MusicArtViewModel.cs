using MusicArtDownloader.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MusicArtDownloader.Gui.ViewModels
{
    public class MusicArtViewModel : ViewModel
    {
        private DispatcherTimer timer;
        private Stopwatch stopWatch;
        private string root;
        private ICommand browseCommand;
        private ICommand loadFoldersCommand;
        private CancellationTokenSource cancellationTokenSource;

        #region Properties

        public ICommand BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                {
                    browseCommand = new Command(() => this.BrowseForRoot());
                }
                return browseCommand;
            }
        }

        public ICommand LoadFoldersCommand
        {
            get
            {
                if (loadFoldersCommand == null)
                {
                    loadFoldersCommand = new AsyncCommand(() => this.SearchForFolders());
                }
                return loadFoldersCommand;
            }
        }

        public string Root
        {
            get { return this.root; }
            set
            {
                this.root = value;
                this.OnPropertyChanged("Root");
            }
        }

        public TimeSpan? Elapsed
        {
            get { return this.stopWatch == null ? new TimeSpan() : this.stopWatch.Elapsed; }
        }

        #endregion

        private void BrowseForRoot()
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Choose the root folder of you music collection";
            dialog.UseDescriptionForTitle = true;
            dialog.ShowNewFolderButton = true;

            var result = dialog.ShowDialog();
            if (result == true)
            {
                this.Root = dialog.SelectedPath;
            }
        }

        private async Task SearchForFolders()
        {
            var token = this.cancellationTokenSource.Token;
            using (var ctx = new MediaContext(this.Root))
            {
                var folder = await ctx.FindAllSubFoldersAsync();
            }
        }

        void Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public MusicArtViewModel()
        {
            SetupTimer();
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        private void SetupTimer()
        {
            this.timer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher.CurrentDispatcher);
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.timer.Tick += Tick;
        }
    }
}
