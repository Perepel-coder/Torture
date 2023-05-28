using Services.Crypto;
using Services.RequestDB.InterfaceDB;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models;

namespace ViewModels.Researcher
{
    public class CryptoComparison_VM: BaseViewModel
    {
        public override string Name { get; set; }
        public CryptoComparison_Controls Controls { get; set; }
        private readonly CryptoStopwatchService stopwatchService;
        public CryptoComparison_VM(
            ICryptoAlgService cryptoAlgService,
            CryptoComparison_Controls controls, 
            CryptoStopwatchService stopwatchService)
        {
            Name = typeof(CryptoComparison_VM).Name;
            Controls = controls;
            Controls.Data_size = 500;
            Controls.Cycle_count = 50;
            Controls.Key_size = 10;
            Controls.Orientation = "Зашифровать";
            this.stopwatchService = stopwatchService;
            Controls.ListOfAlg = new(cryptoAlgService.GetAlgs());
            Controls.ListOfAlgMode = new(cryptoAlgService.GetModes());
            Controls.ListOfAlg.First().Flag = true;
            Controls.ListOfAlgMode.First().Flag = true;
        }

        private RelayCommand? comparisonBlockAlg;
        public ICommand ComparisonBlockAlg
        {
            get
            {
                return comparisonBlockAlg ??= new RelayCommand(chart =>
                {
                    Controls.VisibilityProgressBar = Visibility.Visible;
                    Controls.IsEnabledButtonTransform = false;
                    Thread thread = new(() =>
                    {
                        var algs = Controls.ListOfAlg.Where(el => el.Flag == true);
                        var mode = Controls.ListOfAlgMode.Single(el => el.Flag);

                        stopwatchService.Registration(
                            algs,
                            mode,
                            Controls.Orientation,
                            Controls.Data_size,
                            Controls.Key_size,
                            Controls.Cycle_count);
                        var chartSeries = stopwatchService.GetResult();
                        Chart newChart = (Chart)chart;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            newChart.Series.Clear();
                            foreach (var series in chartSeries)
                            {
                                LineSeries lineSeries = new();
                                lineSeries.Title = series.Item2;
                                lineSeries.ItemsSource = series.Item1;
                                lineSeries.IndependentValueBinding = new Binding
                                {
                                    Path = new PropertyPath("X")
                                };
                                lineSeries.DependentValueBinding = new Binding
                                {
                                    Path = new PropertyPath("Y")
                                };
                                newChart.Series.Add(lineSeries);
                            }
                        });
                        Controls.VisibilityProgressBar = Visibility.Collapsed;
                        Controls.IsEnabledButtonTransform = true;
                    });
                    thread.Start();
                });
            }
        }
    }
}
