using PatientManagement.Models.DataEntites;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PatientManagement.ViewModels.Managers
{
    class HistoryViewModel : ViewModelBase
    {
        public ICommand AddHistoryCommand { get; }
        public ICommand RemoveHistoryCommand { get; }
        public ICommand EditHistoryCommand { get; }
        public ObservableCollection<History> Histories { get; set; }


    }
}
