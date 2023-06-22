using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Fields
    //Timer duration

    float totalSeconds = 0;

    //Timer execution
    float elapedSeconds = 0;
    bool running = false;

    //Support for Finished property
    bool started = false;
    #endregion

    /// <summary>
    /// Runs the timer
    /// Because a timmer of 0 duration doesn't really make sense,
    /// the timer only runs if the total seconds is larger than 0
    /// this also makes sure the consumer of the class has actually set the duration
    /// higher than 0
    /// </summary>

    public void Run()
    {
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapedSeconds = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapedSeconds += Time.deltaTime;
            if (elapedSeconds >= totalSeconds)
            {
                running = false;
            }
        }
    }

    public float Duarion { set { if (!running) { totalSeconds = value; } } }

    public bool Finished
    {
        get { return started && !running; }
    }

    public bool Running
    {
        get { return running; }
    }
}
