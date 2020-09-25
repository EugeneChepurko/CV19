using CV19.Infrastructure.Commands;
using CV19.Views.Base;
using System.Windows;
using System.Windows.Input;

namespace CV19.Views
{
    internal class MainWindowViewModel : ViewModel
    {
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
        }
    }
}
