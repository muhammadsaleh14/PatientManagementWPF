using System;

namespace PatientManagement.Stores
{
    public partial class PatientStore
    {
        public event Action<string>? ErrorDiagosisHeadingViewModel;

        internal void DisplayErrorDiagnosisHeading(string error)
        {
            ErrorDiagosisHeadingViewModel?.Invoke(error);
        }

    }
}
