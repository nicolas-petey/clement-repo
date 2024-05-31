using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestCodecare.Helpers_NePasModifier;

namespace TestCodecare
{
    public class MainWindowViewModel : ViewModel
    {
        private List<int> _points;
        public List<int> Points
        {
            get => _points;
            set
            {
                SetValueAndRaiseEventIfPropertyChanged(() => Points, ref _points, value);
            }
        }

        private int _point;
        public int Point
        {
            get => _point;
            set
            {
                SetValueAndRaiseEventIfPropertyChanged(() => Point, ref _point, value);
            }
        }

        private string _libellé;
        public string Libellé
        {
            get => _libellé;
            set
            {
                SetValueAndRaiseEventIfPropertyChanged(() => Libellé, ref _libellé, value);
            }
        }







        public ICommand CloseCommand { get; set; }

        public MainWindowViewModel()
        {
            Points = new List<int> { 1, 
                10, 53,
                30 };

            CloseCommand = CommandHelper.Create(() => 
                System.Windows.Application.Current.Shutdown());
            Libellé = "Compteur";
        }
    }
}
