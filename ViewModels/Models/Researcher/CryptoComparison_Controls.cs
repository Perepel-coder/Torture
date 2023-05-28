using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace ViewModels.Models
{
    public class CryptoComparison_Controls: ReactiveObject
    {
        public ObservableCollection<CryptoAlg_S> ListOfAlg { get; set; } = new();
        public ObservableCollection<Mode_S> ListOfAlgMode { get; set; } = new();

        private Visibility visibilityProgressBar = Visibility.Collapsed;
        public Visibility VisibilityProgressBar
        {
            get => visibilityProgressBar;
            set => this.RaiseAndSetIfChanged(ref visibilityProgressBar, value);
        }


        private int data_size;
        public int Data_size
        {
            get => data_size;
            set
            {
                this.RaiseAndSetIfChanged(ref data_size, value);
                if (value < this.Cycle_count)
                {
                    this.Cycle_count = value;
                }
            }
        }


        private int key_size;
        public int Key_size
        {
            get => key_size;
            set => this.RaiseAndSetIfChanged(ref key_size, value);
        }


        private int cycle_count;
        public int Cycle_count
        {
            get => cycle_count;
            set
            {
                if (value <= this.Data_size)
                {
                    this.RaiseAndSetIfChanged(ref cycle_count, value);
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref cycle_count, this.Data_size);
                }
            }
        }


        private string orientation = string.Empty;
        public string Orientation
        {
            get => orientation;
            set => this.RaiseAndSetIfChanged(ref orientation, value);
        }


        private bool isEnabledButtonTransform = true;
        public bool IsEnabledButtonTransform
        {
            get => isEnabledButtonTransform;
            set => this.RaiseAndSetIfChanged(ref isEnabledButtonTransform, value);
        }
    }
}
