using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MusicArtDownloader.Gui.ViewModels
{
    public class MusicArtViewModel : ViewModel
    {
        private DispatcherTimer timer;
        private Stopwatch stopWatch;
        private string root;

        #region Properties

        public TimeSpan? Elapsed
        {
            get { return this.stopWatch == null ? new TimeSpan() : this.stopWatch.Elapsed; }
        }

        #endregion

        void Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public MusicArtViewModel()
        {
            SetupTimer();
        }

        private void SetupTimer()
        {
            this.timer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher.CurrentDispatcher);
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.timer.Tick += Tick;
        }
    }
}
