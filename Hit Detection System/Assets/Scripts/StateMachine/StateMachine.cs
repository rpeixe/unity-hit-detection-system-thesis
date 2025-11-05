using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<TStateKey> : MonoBehaviour where TStateKey : Enum
{
    protected Dictionary<TStateKey, State<TStateKey>> States = new();

    public State<TStateKey> PreviousState { get; protected set; }
    public State<TStateKey> CurrentState { get; protected set; }

    protected virtual void Start()
    {
        SetupStateMachine();
    }

    protected virtual void Update()
    {
        TStateKey nextStateKey = CurrentState.UpdateState();

        if (!nextStateKey.Equals(default(TStateKey)))
        {
            TransitionToState(nextStateKey);
        }
    }

    protected virtual void FixedUpdate()
    {
        TStateKey nextStateKey = CurrentState.FixedUpdateState();

        if (!nextStateKey.Equals(default(TStateKey)))
        {
            TransitionToState(nextStateKey);
        }
    }

    protected virtual void TransitionToState(TStateKey nextState)
    {
        CurrentState.ExitState();
        PreviousState = CurrentState;
        CurrentState = States[nextState];
        CurrentState.EnterState();
    }

    protected abstract void SetupStateMachine();
}
