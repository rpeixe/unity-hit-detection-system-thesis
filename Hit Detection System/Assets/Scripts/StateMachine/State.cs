using System;

public abstract class State<TStateKey> where TStateKey : Enum
{
    protected StateMachine<TStateKey> StateMachine;

    public TStateKey StateKey { get; private set; }

    public State(TStateKey key, StateMachine<TStateKey> stateMachine)
    {
        StateKey = key;
        StateMachine = stateMachine;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract TStateKey UpdateState();
    public abstract TStateKey FixedUpdateState();
}
