using PatientManagement.Stores;
using System;
using System.ComponentModel;

namespace PatientManagement.AutoSaves
{
    public class AutoSaveVisit : AutoSaveTimer
    {
        private PatientStore _patientStore;
        public AutoSaveVisit(PatientStore patientStore, Action<string, string?> saveAction) : base(saveAction)
        {
            _patientStore = patientStore;
            base.PropertyChanged += AutoSaveTimerChanged;

        }

        public override void UpdateText(string propertyName, string text) // Updated method signature
        {
            base.UpdateText(propertyName, text);

        }

        private void AutoSaveTimerChanged(object? sender, PropertyChangedEventArgs e)
        {

            if (sender is AutoSaveTimer autoSaveTimer && e.PropertyName == nameof(autoSaveTimer.IsSaving))
            {
                _patientStore.ChangeCanCloseCounter(!autoSaveTimer.IsSaving);
            }
        }

    }
}
