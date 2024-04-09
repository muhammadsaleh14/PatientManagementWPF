using System.ComponentModel;
using System.Threading.Tasks;

namespace PatientManagement.CustomComponents
{
    public class Message : INotifyPropertyChanged
    {
        private string _messageText = string.Empty;

        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                OnPropertyChanged(nameof(MessageText));
                Task.Delay(2000).ContinueWith(t =>
                {
                    _messageText = string.Empty;
                    OnPropertyChanged(nameof(MessageText));
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
