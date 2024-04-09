using PatientManagement.Commands;
using PatientManagement.CustomComponents;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Managers;
using PatientManagement.Views.ConfirmationWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class VisitListViewModel : INotifyPropertyChanged
    {
        private PatientStore _patientStore;
        public ICommand OpenVisitViewCommand { get; }
        public ICommand DeleteVisitCommand { get; }

        public VisitListViewModel(Stores.PatientStore patientStore)
        {
            _patientStore = patientStore;
            _patientStore.PatientSelectionChanged += OnPatientSelectionChanged;
            //_patientStore.VisitCreated += OnVisitCreated;
            _patientVisits = new ObservableCollection<Visit>(VisitManager.GetPatientVisitsFromDb(SelectedPatient?.Id) ?? Enumerable.Empty<Visit>());
            OpenVisitViewCommand = new RelayCommand(OpenVisitView);
            DeleteVisitCommand = new RelayCommand(DeleteVisit);
        }

        private void DeleteVisit(object obj)
        {
            try
            {
                if (obj is Visit visit)
                {
                    var confirmationWindow = new DeleteConfirmationWindow("Deleting Visit " +
                        "\nDate: " + visit.Date + "" +
                        "\nDetail:" + visit.OptionalDetail);
                    confirmationWindow.ShowDialog();
                    confirmationWindow.Activate();

                    if (confirmationWindow.Confirmed)
                    {
                        VisitManager.DeleteVisit(visit);
                        PatientVisits.Remove(visit);
                    }
                }
            }
            catch (Exception ex)
            {
                Message.MessageText = ex.Message;
            }
        }

        private void OpenVisitView(object visitId)
        {
            _patientStore.CurrentVisitId = (string)visitId;
            VisitViewModel visitViewModel = new VisitViewModel(_patientStore);
            _patientStore.ChangeViewModel(visitViewModel);

        }

        //private void OnVisitCreated(VisitPage obj)
        //{
        //    if (obj is VisitPage visit)
        //    {
        //        PatientVisits.Add(visit);
        //    }

        //}

        private void OnPatientSelectionChanged(Patient? patient)
        {
            IEnumerable<Visit> visits = VisitManager.GetPatientVisitsFromDb(patient?.Id) ?? Enumerable.Empty<Visit>();
            //foreach (VisitPage vis in visits)
            //{
            //    Debug.WriteLine("patient Visits:" + vis.Date + " detail:" + vis.OptionalDetail);
            //}
            SelectedPatient = patient;
            PatientVisits = new ObservableCollection<Visit>(visits);
            //OnPropertyChanged(nameof(PatientVisits));
        }


        private Message _message = new Message();

        public Message Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        private Patient? _selectedPatient;
        public Patient? SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        private ObservableCollection<Visit> _patientVisits;
        public ObservableCollection<Visit> PatientVisits
        {
            get { return _patientVisits; }
            set
            {
                _patientVisits = value;
                OnPropertyChanged(nameof(PatientVisits));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
