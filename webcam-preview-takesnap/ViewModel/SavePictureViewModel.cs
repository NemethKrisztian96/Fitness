namespace TakeSnapsWithWebcamUsingWpfMvvm.ViewModel
{
    #region Namespace

    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media;
    using Fitness.Common.Helpers;
    using Fitness.Logic;
    using Fitness.Model;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;

    using TakeSnapsWithWebcamUsingWpfMvvm.Video;

    #endregion

    /// <summary>
    /// Represents view model for main window.
    /// </summary>
    public class SavePictureViewModel : ViewModelBase
    {
        private string ClientPicturesFolder = "../../../ClientPictures/"; //Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../ClientPictures"));

        /// <summary>
        /// Video preview width.
        /// </summary>
        private int videoPreviewWidth;

        /// <summary>
        /// Video preview height.
        /// </summary>
        private int videoPreviewHeight;

        /// <summary>
        /// Selected video device.
        /// </summary>
        private MediaInformation selectedVideoDevice;

        /// <summary>
        /// Snapshot taken.
        /// </summary>
        private ImageSource snapshotTaken;

        /// <summary>
        /// Byte array of snapshot image.
        /// </summary>
        private Bitmap snapshotBitmap;

        /// <summary>
        /// List of media information device available.
        /// </summary>
        private IEnumerable<MediaInformation> mediaDeviceList;

        /// <summary>
        /// Instance of relay command for loaded event.
        /// </summary>
        private RelayCommand loadedCommand;

        /// <summary>
        /// Instance of relay command for snapshot command.
        /// </summary>
        private RelayCommand snapshotCommand;

        private RelayCommand savePictureCommand;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public SavePictureViewModel()
        {
        }

        /// <summary>
        /// Gets or sets video preview width.
        /// </summary>
        public int VideoPreviewWidth
        {
            get
            {
                return this.videoPreviewWidth;
            }

            set
            {
                this.videoPreviewWidth = value;
                this.RaisePropertyChanged(() => this.VideoPreviewWidth);
            }
        }

        /// <summary>
        /// Gets or sets video preview height.
        /// </summary>
        public int VideoPreviewHeight
        {
            get
            {
                return this.videoPreviewHeight;
            }

            set
            {
                this.videoPreviewHeight = value;
                this.RaisePropertyChanged(() => this.VideoPreviewHeight);
            }
        }

        /// <summary>
        /// Gets or sets selected media video device.
        /// </summary>
        public MediaInformation SelectedVideoDevice
        {
            get
            {
                return this.selectedVideoDevice;
            }

            set
            {
                this.selectedVideoDevice = value;
                this.RaisePropertyChanged(() => this.SelectedVideoDevice);
            }
        }

        /// <summary>
        /// Gets or sets image source.
        /// </summary>
        public ImageSource SnapshotTaken
        {
            get
            {
                return this.snapshotTaken;
            }

            set
            {
                this.snapshotTaken = value;
                this.RaisePropertyChanged(() => this.SnapshotTaken);
            }
        }

        /// <summary>
        /// Gets or sets snapshot bitmap.
        /// </summary>
        public Bitmap SnapshotBitmap
        {
            get
            {
                return this.snapshotBitmap;
            }

            set
            {
                this.snapshotBitmap = value;
                this.RaisePropertyChanged(() => this.SnapshotBitmap);
            }
        }

        /// <summary>
        /// Gets or sets media device list.
        /// </summary>
        public IEnumerable<MediaInformation> MediaDeviceList
        {
            get
            {
                return this.mediaDeviceList;
            }

            set
            {
                this.mediaDeviceList = value;
                this.RaisePropertyChanged(() => this.MediaDeviceList);
            }
        }

        /// <summary>
        /// Gets instance of relay command of type windows loaded.
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                return this.loadedCommand ?? (this.loadedCommand = new RelayCommand(this.OnWindowLoaded));
            }
        }

        /// <summary>
        /// Gets instance of relay command for snapshot command.
        /// </summary>
        public RelayCommand SnapshotCommand
        {
            get
            {
                return this.snapshotCommand ?? (this.snapshotCommand = new RelayCommand(this.OnSnapshot));
            }
        }

        public RelayCommand SavePictureCommand
        {
            get
            {
                return this.savePictureCommand ?? (this.savePictureCommand = new RelayCommand(this.SavePicture,this.SavePictureCanExecute));
            }
        }

        /// <summary>
        /// Event handler for windows loaded event.
        /// </summary>
        private void OnWindowLoaded()
        {
            this.MediaDeviceList = WebcamDevice.GetVideoDevices;
            this.VideoPreviewWidth = 320;
            this.VideoPreviewHeight = 240;
            this.SelectedVideoDevice = null;
        }

        /// <summary>
        /// Event handler for on take snapshot command click.
        /// </summary>
        private void OnSnapshot()
        {
            this.SnapshotTaken = ConvertToImageSource(this.SnapshotBitmap);
        }

        /// <summary>
        /// The convert to image source.
        /// </summary>
        /// <param name="bitmap"> The bitmap. </param>
        /// <returns> The <see cref="object"/>. </returns>
        public static ImageSource ConvertToImageSource(Bitmap bitmap)
        {
            var imageSourceConverter = new ImageSourceConverter();
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                var snapshotBytes = memoryStream.ToArray();
                return (ImageSource)imageSourceConverter.ConvertFrom(snapshotBytes); ;
            }
        }

        public void SavePicture()
        {
            string filename = this.ClientPicturesFolder + Data.Fitness.LastModified.Id.ToString() + ".jpeg";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            this.SnapshotBitmap.Save(filename, ImageFormat.Jpeg);
            Data.Fitness.LastModified.ImagePath = filename;
            Data.Fitness.SaveAllChanges();

            PopupMessage.OkButtonPopupMessage("Success", "Picture saved successfully");
        }

        public bool SavePictureCanExecute()
        {
            if (this.SnapshotTaken != null)
            {
                return true;
            }
            return false;
        }
    }
}