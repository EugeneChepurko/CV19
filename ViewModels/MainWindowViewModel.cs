﻿using CV19.Infrastructure.Commands;
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
        /// <summary>
        /// Selected number index
        /// </summary>
        private int selectedTabIndex;
        /// <summary>
        /// Selected number index
        /// </summary>
        public int SelectedTabIndex
        {
            get => selectedTabIndex;
            set { OnPropertyChanged(ref selectedTabIndex, value); }
        }


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

        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => selectedTabIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedTabIndex += Convert.ToInt32(p);
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new RelayCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

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
