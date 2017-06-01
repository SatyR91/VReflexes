using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    private float timerStart;
    private float timerStop;
    public bool hasStarted;

    /// <summary>
    /// Duration in milliseconds
    /// </summary>
    public float duration {
        get {
            return (timerStop - timerStart)*1000;
        }
    }

    /// <summary>
    /// Begin the timer
    /// </summary>
    public void StartTimer() {
        timerStart = Time.time;
        hasStarted = true;
    }

    /// <summary>
    /// End the timer
    /// </summary>
    public void StopTimer()
    {
        timerStop = Time.time;
        hasStarted = false;
    }

    public float currentDuration()
    {
        return (Time.time - timerStart) * 1000;
    }

}
