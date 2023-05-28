using ReactiveUI;
using Services.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        private Visibility visibilityProgressBar;
        private bool isEnabledButtonTransform;

        public bool IsEnabledButtonTransform
        {
            get => isEnabledButtonTransform;
            set => this.RaiseAndSetIfChanged(ref isEnabledButtonTransform, value);
        }
        public Visibility VisiProgressBar
        {
            get => visibilityProgressBar;
            set => this.RaiseAndSetIfChanged(ref visibilityProgressBar, value);
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

        public void Visi_TableImageText(Visibility dg, Visibility image, Visibility text)
        {
            VisiDataGrid = dg;
            VisiImage = image;
            VisiTextBlock = text;
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
            visibilityProgressBar = Visibility.Collapsed;
            visiProjectText = Visibility.Collapsed;
            isEnabledButtonTransform = true;;
        }
    }
}
