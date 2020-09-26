using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        private IEnumerable<DataPoint> testDataPoints;
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => testDataPoints;
            set { OnPropertyChanged(ref testDataPoints, value); }
        }
        /// <summary>
        /// App status
        /// </summary>
        private string status = "Ready!";
        /// <summary>
        /// Application status
        /// </summary>
        public string Status
        {
            get => status;
            set { OnPropertyChanged(ref status, value); }
        }

        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            var dataPoints = new List<DataPoint>((int)(360 / 0.1));
            for (double x = 0d; x <= 360; x+=0.1)
            {
                const double toRad = Math.PI / 180;
                var y = Math.Sin(x * toRad);

                dataPoints.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = dataPoints;
        }
    }
}
