using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is provided by the previous project. 
 */
public static class StateController
{
    private static State CurrentState = State.Reset;
    public static Text Label;
    private static List<StateListener> listners = new List<StateListener>();
    public static event Action SimulationStarted;
    public static event Action SimulationStopped;
    public enum State
    {
        Running, Stopped, Reset, PickUp, Placed
    }
    public static void Next()
    {
        switch (CurrentState)
        {
            case State.Reset:
                StartSim();
                break;
            case State.Running:
                StopSim();
                break;
            case State.Stopped:
                ResetSim();
                break;
            default:
                break;
        }
    }
    public static void PickUp()
    {
        CurrentState = State.PickUp;
        OnStateUpdate();
    }
    public static void Place()
    {
        CurrentState = State.Placed;
        OnStateUpdate();
        CurrentState = State.Reset;//silent transition. Pick and place subState of Reset.
    }
    public static void StartSim()
    {
        if (SimulationStarted != null)
        {
            SimulationStarted();
        }
        CurrentState = State.Running;
        OnStateUpdate();
    }
    public static void StopSim()
    {
        if (SimulationStopped != null)
        {
            SimulationStopped();
        }
        CurrentState = State.Stopped;
        OnStateUpdate();
    }
    public static void ResetSim()
    {
        CurrentState = State.Reset;
        OnStateUpdate();
    }
    public static void AddListner(StateListener listener)
    {
        listners.Add(listener);
    }
    public static void RemoveListner(StateListener listener)
    {
        listners.Remove(listener);
    }
    private static void OnStateUpdate()
    {
        foreach (StateListener listner in listners)
        {
            if (listner.ListenerState == CurrentState)
                listner.OnStateUpdate();
        }
    }

    public static bool isSimulationStarted()
    {
        return CurrentState.Equals(State.Running);
    }

    public static bool isStopped()
    {
        return CurrentState.Equals(State.Stopped);
    }
}