using ReactiveUI;
using Services.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using trans = Services.Crypto.TransformData;

namespace ViewModels.Models
{
    public class CypherData_Controls : ReactiveObject
    {
        public enum DataType { TEXT, CFT, TABLE, IMAGE };
        public CypherTools Tools { get; }
        public ObservableCollection<CryptoAlg_S> ListOfAlg { get; set; }
        public ObservableCollection<Mode_S> ListOfAlgMode { get; set; }
        public IEnumerable<int>? InDataCode { get; set; }
        public DataType CurrentType { get; set; }
        private object? inputData;

        public object? InData
        {
            get => inputData;
            set
            {
                this.RaiseAndSetIfChanged(ref inputData, value);
                if(inputData != null && inputData.GetType() == typeof(string))
                {
                    InDataCode = trans.ToInt((string)inputData);
                }
            }
        }

        private Visibility progressBar;
        private bool isEnabledButtonTransform;

        public bool IsEnabledButtonTransform
        {
            get => isEnabledButtonTransform;
            set => this.RaiseAndSetIfChanged(ref isEnabledButtonTransform, value);
        }
        public Visibility ProgressBar
        {
            get => progressBar;
            set => this.RaiseAndSetIfChanged(ref progressBar, value);
        }

        private string statusBar;
        private Visibility visiDataGrid;
        private Visibility visiImage;
        private Visibility visiTextBlock;
        private Visibility visiProjectText;

        public string StatusBar
        {
            get => statusBar;
            set => this.RaiseAndSetIfChanged(ref statusBar, value);
        }
        public Visibility VisiDataGrid
        {
            get => visiDataGrid;
            set => this.RaiseAndSetIfChanged(ref visiDataGrid, value);
        }
        public Visibility VisiImage
        {
            get => visiImage;
            set => this.RaiseAndSetIfChanged(ref visiImage, value);
        }
        public Visibility VisiTextBlock
        {
            get => visiTextBlock;
            set => this.RaiseAndSetIfChanged(ref visiTextBlock, value);
        }
        public Visibility VisiProj_Text
        {
            get => visiProjectText;
            set => this.RaiseAndSetIfChanged(ref visiProjectText, value);
        }

        public void Visi_Table()
        {
            Collaps_All();
            VisiDataGrid = Visibility.Visible;
        }
        public void Visi_Text()
        {
            Collaps_All();
            VisiTextBlock = Visibility.Visible;
        }
        public void Visi_Image()
        {
            Collaps_All();
            VisiImage = Visibility.Visible;
        }
        public void Visi_ProjTXT()
        {
            Collaps_All();
            VisiProj_Text = Visibility.Visible;
        }

        public void ProgressBar_ON()
        {
            ProgressBar = Visibility.Visible;
        }
        public void ProgressBar_OFF()
        {
            ProgressBar = Visibility.Collapsed;
        }

        public void Collaps_All()
        {
            VisiDataGrid = Visibility.Collapsed;
            VisiImage = Visibility.Collapsed;
            VisiTextBlock = Visibility.Collapsed;
            VisiProj_Text = Visibility.Collapsed;
        }

        public CypherData_Controls(CypherTools tools)
        {
            Tools = tools;
            ListOfAlg = new();
            ListOfAlgMode = new();
            inputData = new();
            statusBar = string.Empty;
            visiDataGrid = Visibility.Collapsed;
            visiImage = Visibility.Collapsed;
            visiTextBlock = Visibility.Collapsed;
            progressBar = Visibility.Collapsed;
            visiProjectText = Visibility.Collapsed;
            isEnabledButtonTransform = true;
        }
    }
}
