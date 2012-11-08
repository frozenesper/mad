using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common
{
    public class Folder : Node, INotifyPropertyChanged
    {
        private readonly ObservableCollection<Node> items;

        /// <summary>
        /// Gets a collection of items contained in the folder.
        /// </summary>
        public ObservableCollection<Node> Items { get { return this.items; } }

        /// <summary>
        /// Gets a collection of subfolders contained in the folder.
        /// </summary>
        public IReadOnlyCollection<Folder> Folders
        { get { return this.Items.OfType<Folder>().Cast<Folder>().ToList(); } }

        /// <summary>
        /// Gets a collection of files contained in the folder.
        /// </summary>
        public IReadOnlyCollection<MediaFile> Files
        { get { return this.Items.OfType<MediaFile>().Cast<MediaFile>().ToList(); } }


        /// <summary>
        /// Initializes a new instance of the Folder class.
        /// </summary>
        public Folder()
        {
            this.items = new ObservableCollection<Node>();
            this.items.CollectionChanged += items_CollectionChanged;
        }

        private void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged("Items");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler!= null)
            {
                var args = new PropertyChangedEventArgs(property);
                handler(this, args);
            }
        }

        #region Equality

        public bool Equals(Folder o)
        {
            return o != null &&
                   base.Equals((Node)o) &&
                   this.Items.SequenceEqual(o.Items);
        }

        public override bool Equals(object o)
        {
            return o is Folder &&
                   this.Equals((Folder)o);
        }

        public override int GetHashCode()
        {
            int hash = 33;
            hash = (hash * 7) + base.GetHashCode();
            hash = (hash * 7) + this.Items.GetHashCode();
            return hash;
        }

        #endregion
    }
}
