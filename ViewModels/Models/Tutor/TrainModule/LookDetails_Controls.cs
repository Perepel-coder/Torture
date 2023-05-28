using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace ViewModels.Models.Tutor.TrainModule
{
    public class LookDetails_Controls : ReactiveObject
    {
        public ATask Task { get; set; } = null!;
        public string? HtmlCode { get; set; }
        private Visibility baseMath;
        private Visibility methodProgramm;
        private Visibility selectKey;
        public Visibility BaseMath
        {
            get => baseMath;
            set => this.RaiseAndSetIfChanged(ref baseMath, value);
        }
        public Visibility MethodProgramm
        {
            get => methodProgramm;
            set => this.RaiseAndSetIfChanged(ref methodProgramm, value);
        }
        public Visibility SelectKey
        {
            get => selectKey;
            set => this.RaiseAndSetIfChanged(ref selectKey, value);
        }
        public LookDetails_Controls()
        {
            BaseMath = Visibility.Collapsed;
            SelectKey = Visibility.Collapsed;
            MethodProgramm = Visibility.Collapsed;
        }
        public BaseMath_S? BaseMathTask { get; set; }
        public MethodProgramm_S? MethodProgrammTask { get; set; }
        public SelectKey_S? SelectKeyTask { get; set; }
    }
}
