using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;

public class AutoSaveTimer
{
    private readonly DispatcherTimer timer;
    private readonly Action<string, string?> saveAction; // Updated Action delegate
    private Dictionary<string, string?> textToSave = new Dictionary<string, string?>();

    public AutoSaveTimer(Action<string, string?> saveAction) // Updated constructor
    {
        this.saveAction = saveAction;
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(2); // Adjust as needed
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    public void UpdateText(string propertyName, string text) // Updated method signature
    {
        textToSave[propertyName] = text;
        RestartTimer();
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
    }

    private void RestartTimer()
    {
        timer.Stop();
        timer.Start();
    }
}
