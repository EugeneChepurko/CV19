using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CV19WPFTest
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double scaleX = 1;
        public double ScaleX
        {
            get => scaleX;
            set { OnPropertyChanged(ref scaleX, value); }
        }

        private double scaleY = 1;
        public double ScaleY
        {
            get => scaleY;
            set { OnPropertyChanged(ref scaleY, value); }
        }
        
        private int tickness = 1;
        public int Tickness
        {
            get => tickness;
            set { OnPropertyChanged(ref tickness, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        

        private void Window_MouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            ScaleX += 0.5;
            ScaleY += 0.5;
            //Tickness = 2;
        }
    }
}
