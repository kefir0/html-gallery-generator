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
        #region private static methods

        private static void CopyFileToDir(string file, string dir)
        {
            File.Copy(file, Path.Combine(dir, file), true);
        }

        #endregion

        #region public properties and indexers

        public string FileListText
        {
            get { return _fileListText; }
            set
            {
                if (value == _fileListText) return;
                _fileListText = value;
                OnPropertyChanged("FileListText");
                GoCommand.OnCanExecuteChanged();
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

        public string GalleryTitle
        {
            get { return _galleryTitle; }
            set
            {
                if (value == _galleryTitle) return;
                _galleryTitle = value;
                OnPropertyChanged("GalleryTitle");
            }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value == _pageSize) return;
                if (value <= 0) throw new ArgumentException();
                _pageSize = value;
                OnPropertyChanged("PageSize");
            }
        }

        public DelegateCommand GoCommand
        {
            get { return _goCommand ?? (_goCommand = new DelegateCommand(Go, CanGo)); }
        }

        public string OutputFileName
        {
            get { return _outputFileName; }
            set
            {
                if (value == _outputFileName) return;
                _outputFileName = value;
                OnPropertyChanged("OutputFileName");
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

        private bool CanGo()
        {
            return !string.IsNullOrEmpty(FileListText);
        }

        private void Go()
        {
            var dlg = new FolderBrowserDialog {Description = "Выбери целевую папку (куда HTML файлы галереи записать)"};
            if (dlg.ShowDialog() == DialogResult.OK && Directory.Exists(dlg.SelectedPath))
            {
                try
                {
                    GalleryProcessor.CreateGallery(
                        FileListText.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries),
                        GalleryTitle,
                        ServerImagePath, OutputFileName, PageSize, dlg.SelectedPath);

                    // Copy CSS and logo
                    CopyFileToDir("trialru.css", dlg.SelectedPath);
                    CopyFileToDir("logo.png", dlg.SelectedPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Это провал!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadFilesFromFolder()
        {
            var dlg = new FolderBrowserDialog {Description = "Папка с картинками:"};
            if (dlg.ShowDialog() == DialogResult.OK && Directory.Exists(dlg.SelectedPath))
            {
                FileListText = string.Join(Environment.NewLine,
                    Directory.GetFiles(dlg.SelectedPath).Select(Path.GetFileName).ToArray());
            }
        }

        #endregion

        #region private fields

        private DelegateCommand _loadFilesFromFolderCommand;
        private string _fileListText = string.Empty;
        private string _serverImagePath = "images/";
        private string _galleryTitle = "Vyborg Outdoor Camp 2013";
        private int _pageSize = 20;
        private DelegateCommand _goCommand;
        private string _outputFileName = "gallery";

        #endregion
    }
}