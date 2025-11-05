using UnityEngine;

public class FighterGroundedState : FighterState
{
    private bool m_jumpPressed;
    private FighterStates m_attackStateQueued;

    public FighterGroundedState(FighterStates key, FighterStateMachine stateMachine) : base(key, stateMachine)
    {
    }

    public override void EnterState()
    {
        m_jumpPressed = false;
        m_attackStateQueued = FighterStates.None;
        StateMachine.AnimationController.PlayAnimation(FighterAnimations.Idle);
    }

    public override void ExitState()
    {
    }

    public override FighterStates UpdateState()
    {
        if (m_jumpPressed)
        {
            StateMachine.PhysicsController.Jump();
            return FighterStates.AttackingJumpIn;
        }

        if (m_attackStateQueued != FighterStates.None)
        {
            return FighterStates.None;
        }

        if (LeftStickPosition.y < -0.5)
        {
            return FighterStates.Crouching;
        }

        StateMachine.PhysicsController.Walk(LeftStickPosition.x);

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
        if (button == ControllerButtons.Jump)
        {
            m_jumpPressed = true;
        }
        else if (button == ControllerButtons.Attack)
        {
            m_attackStateQueued = FighterStates.AttackingOverhead;
        }
    }

    public override void HandleButtonReleased(ControllerButtons button)
    {
    }
}
