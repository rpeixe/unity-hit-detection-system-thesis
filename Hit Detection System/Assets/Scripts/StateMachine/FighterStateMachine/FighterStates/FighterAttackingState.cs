using UnityEngine;

public class FighterAttackingState : FighterState
{
    private FighterAnimations m_animation;
    private bool m_isAerial;

    public FighterAttackingState(FighterStates key, FighterStateMachine stateMachine, FighterAnimations animation, bool isAerial) : base(key, stateMachine)
    {
        m_animation = animation;
        m_isAerial = isAerial;
    }

    public override void EnterState()
    {
        StateMachine.AnimationController.PlayAnimation(m_animation);
    }

    public override void ExitState()
    {
    }

    public override FighterStates UpdateState()
    {
        return FighterStates.None;
    }

    public override FighterStates FixedUpdateState()
    {
        FighterStates transition = FighterStates.None;

        if (m_isAerial)
        {
            transition = HandleAerialAttack();
        }
        else
        {
            transition = HandleGroundedAttack();
        }

        return transition;
    }

    protected virtual FighterStates HandleAerialAttack()
    {
        if (!StateMachine.PhysicsController.IsMovingUp && StateMachine.PhysicsController.IsGrounded())
        {
            return FighterStates.Grounded;
        }

        return FighterStates.None;
    }

    protected virtual FighterStates HandleGroundedAttack()
    {
        FighterStates previousStateKey = ((FighterState)StateMachine.PreviousState).StateKey;

        if (StateMachine.AnimationController.AnimationFinished)
        {
            return previousStateKey;
        }

        return FighterStates.None;
    }

    public override void HandleButtonPressed(ControllerButtons button)
    {
    }

    public override void HandleButtonReleased(ControllerButtons button)
    {
    }
}
