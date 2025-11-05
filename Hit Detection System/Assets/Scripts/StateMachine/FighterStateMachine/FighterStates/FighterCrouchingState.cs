using UnityEngine;

public class FighterCrouchingState : FighterState
{
    private FighterStates m_attackStateQueued;

    public FighterCrouchingState(FighterStates key, FighterStateMachine stateMachine) : base(key, stateMachine)
    {
    }

    public override void EnterState()
    {
        StateMachine.PhysicsController.Stop();
        m_attackStateQueued = FighterStates.None;

        StateMachine.AnimationController.PlayAnimation(FighterAnimations.Crouching);
    }

    public override void ExitState()
    {
    }

    public override FighterStates UpdateState()
    {
        if (LeftStickPosition.y > -0.5)
        {
            return FighterStates.Grounded;
        }

        return FighterStates.None;
    }

    public override FighterStates FixedUpdateState()
    {
        if (m_attackStateQueued != FighterStates.None)
        {
            return m_attackStateQueued;
        }

        return FighterStates.None;
    }

    public override void HandleButtonPressed(ControllerButtons button)
    {
        if (button == ControllerButtons.Attack)
        {
            m_attackStateQueued = FighterStates.AttackingLow;
        }
    }

    public override void HandleButtonReleased(ControllerButtons button)
    {
    }
}
