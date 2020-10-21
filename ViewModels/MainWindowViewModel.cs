using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Decanat :D

        public object[] CompositeCollection { get; }

        private object selectedConpositeValue;
        public object SelectedConpositeValue
        {
            get => selectedConpositeValue;
            set { OnPropertyChanged(ref selectedConpositeValue, value); }
        }

        public ObservableCollection<Group> Groups { get; }

        private Group selectedGroup;
        public Group SelectedGroup
        {
            get => selectedGroup;
            set { OnPropertyChanged(ref selectedGroup, value); }
        }

        #endregion
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

        #region Close Application Command
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Change Tab Index Command
        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => selectedTabIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedTabIndex += Convert.ToInt32(p);
        }
        #endregion

        #region Create Group

        public ICommand CreateGroupCommand { get; }
        private bool CanCreateGroupCommandExecute(object p) => true;
        private void OnCreateGroupCommandExecuted(object p)
        {
            var groupMaxIndex = Groups.Count + 1;
            var newGroup = new Group()
            {
                Name = $"Group {groupMaxIndex}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(newGroup);
        }

        #endregion

        #region Delete Group
        public ICommand DeleteGroupCommand { get; }
        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);
        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;

            int indexGroup = Groups.IndexOf(group);
            Groups.Remove(group);
            
            if (indexGroup <= Groups.Count && Groups.Count != 0)
                SelectedGroup = Groups[Groups.Count - 1];
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new RelayCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateGroupCommand = new RelayCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new RelayCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);

            var dataPoints = new List<DataPoint>((int)(360 / 0.1));
            for (double x = 0d; x <= 360; x+=0.1)
            {
                const double toRad = Math.PI / 180;
                var y = Math.Sin(x * toRad);

                dataPoints.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = dataPoints;

            var studentIndex = 1;

            var student = Enumerable.Range(1, 10).Select(s => new Student
            {
                Name = $"Student {studentIndex}",
                Surname = $"Surname {studentIndex}",
                Patronymic = $"Patronymic {studentIndex++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(g => new Group 
            { 
                Name = $"Group {g}",
                Students = new ObservableCollection<Student>(student)
            });
            Groups = new ObservableCollection<Group>(groups);

            var data_list = new List<object>();

            data_list.Add("hello!");
            data_list.Add(34);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();
        }
    }
}
