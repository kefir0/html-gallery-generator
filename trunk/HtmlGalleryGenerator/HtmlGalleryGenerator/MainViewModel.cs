using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HtmlGalleryGenerator.Annotations;

namespace HtmlGalleryGenerator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region public properties and indexers

        public string FileListText
        {
            get { return _fileListText; }
            set
            {
                if (value == _fileListText) return;
                _fileListText = value;
                OnPropertyChanged("FileListText");
            }
        }

        public DelegateCommand LoadFilesFromFolderCommand
        {
            get
            {
                return _loadFilesFromFolderCommand ??
                       (_loadFilesFromFolderCommand = new DelegateCommand(LoadFilesFromFolder));
            }
        }

        public string ServerImagePath
        {
            get { return _serverImagePath; }
            set
            {
                if (value == _serverImagePath) return;
                _serverImagePath = value;
                OnPropertyChanged("ServerImagePath");
            }
        }

        #endregion

        #region public methods

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region protected methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region private methods

        private void LoadFilesFromFolder()
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK && Directory.Exists(dlg.SelectedPath))
            {
                FileListText = string.Join(Environment.NewLine, Directory.GetFiles(dlg.SelectedPath).Select(Path.GetFileName).ToArray());
            }
        }

        #endregion

        #region private fields

        private DelegateCommand _loadFilesFromFolderCommand;
        private string _fileListText;
        private string _serverImagePath = "images/";

        #endregion
    }
}