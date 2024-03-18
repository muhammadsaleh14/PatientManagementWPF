using System;

namespace PatientManagement.Interfaces
{
    public interface ICloseWindow
    {
        Action CloseWindow { get; set; }
        bool CanCloseWindow();
    }
}
