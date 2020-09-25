using CV19.Views.Base;

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
    }
}
