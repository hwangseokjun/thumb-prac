using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MainViewModel : ViewModelBase
    {

        private ObservableCollection<Box> _boxes;
        public ObservableCollection<Box> Boxes
        {
            get => _boxes;
            set => SetProperty(ref _boxes, value);
        }

        public MainViewModel()
        {
            Boxes = new ObservableCollection<Box>();
            Boxes.Add(new Box { X = 100, Y = 100, Width = 100, Height = 100 });
        }
    }
}
