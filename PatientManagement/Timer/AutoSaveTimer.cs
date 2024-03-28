using PatientManagement.ViewModels.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;

public class AutoSaveTimer : ViewModelBase
{

    public DispatcherTimer timer;
    private bool _isSaving;

    public bool IsSaving
    {
        get { return _isSaving; }
        set
        {
            if (_isSaving != value)
            {
                {
                    _isSaving = value;
                    OnPropertyChanged(nameof(IsSaving));
                }
            }

        }
    }


    private readonly Action<string, string?> saveAction; // Updated Action delegate
    private Dictionary<string, string?> textToSave = new Dictionary<string, string?>();

    public AutoSaveTimer(Action<string, string?> saveAction) // Updated constructor
    {
        this.saveAction = saveAction;
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(2); // Adjust as needed
        timer.Tick += Timer_Tick;
        _isSaving = false;

    }



    public virtual void UpdateText(string propertyName, string text) // Updated method signature
    {
        RestartTimer();
        IsSaving = true;
        textToSave[propertyName] = text;
        Debug.WriteLine("Restarting timer");

    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        // Save text when the timer ticks
        foreach (var key in textToSave.Keys)
        {
            if (textToSave[key] != null)
            {
                saveAction.Invoke(key, textToSave[key]);
                textToSave[key] = null;
            }
        }
        IsSaving = false;
        timer.Stop();
        Debug.WriteLine("running timer tick");
    }

    private void RestartTimer()
    {
        timer.Stop();
        timer.Start();
    }
}
