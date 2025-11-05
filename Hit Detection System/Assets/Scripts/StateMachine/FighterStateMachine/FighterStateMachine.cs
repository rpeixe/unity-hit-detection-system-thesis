using UnityEngine;
using HitDetectionSystem;

public class FighterStateMachine : StateMachine<FighterStates>
{
    [SerializeField] private GameObject m_hitboxes;
    [SerializeField] private GameObject m_hurtboxes;
    [SerializeField] private int m_playerID;

    private FighterInputController m_inputController;
    private FighterPhysicsController m_physicsController;
    private FighterAnimationController m_animationController;

    private bool m_isFrozen;

    public int PlayerID => m_playerID;

    public FighterInputController InputController => m_inputController;
    public FighterPhysicsController PhysicsController => m_physicsController;
    public FighterAnimationController AnimationController => m_animationController;

    public new FighterState CurrentState
    {
        get => (FighterState)base.CurrentState;
        set => base.CurrentState = value;
    }

    public object TransitionContext { get; set; }


    protected override void SetupStateMachine()
    {
        m_inputController = GetComponent<FighterInputController>();
        m_physicsController = GetComponent<FighterPhysicsController>();
        m_animationController = GetComponent<FighterAnimationController>();

        States.Add(FighterStates.Grounded, new FighterGroundedState(FighterStates.Grounded, this));
        States.Add(FighterStates.Crouching, new FighterCrouchingState(FighterStates.Crouching, this));

        States.Add(FighterStates.AttackingOverhead, new FighterAttackingState(FighterStates.AttackingOverhead, this, FighterAnimations.Overhead, false));
        States.Add(FighterStates.AttackingLow, new FighterAttackingState(FighterStates.AttackingLow, this, FighterAnimations.Low, false));
        States.Add(FighterStates.AttackingJumpIn, new FighterAttackingState(FighterStates.AttackingJumpIn, this, FighterAnimations.JumpIn, true));

        CurrentState = (FighterState)States[FighterStates.Grounded];

        m_isFrozen = false;

        RegisterInputEvents();
        InitializeHitboxes();
        RegisterHitboxEvents();
        RegisterHurtboxEvents();
    }

    private void RegisterInputEvents()
    {
        m_inputController.OnButtonPressed += HandleButtonPressed;
        m_inputController.OnButtonReleased += HandleButtonReleased;
        m_inputController.OnLeftStickUpdated += HandleLeftStickUpdated;
    }

    private void RegisterHitboxEvents()
    {
        var hitboxes = m_hitboxes.GetComponentsInChildren<HitDetectionController>(includeInactive: true);

        foreach (var hitbox in hitboxes)
        {
            hitbox.OnHit += HandleHitDealt;
        }
    }

    private void InitializeHitboxes()
    {
    }

    private void RegisterHurtboxEvents()
    {
        if (m_hurtboxes.TryGetComponent(out HitDetectionController hurtbox))
        {
            hurtbox.OnHit += HandleHitTaken;
        }
    }

    private void HandleButtonPressed(ControllerButtons button)
    {
        CurrentState.HandleButtonPressed(button);
    }

    private void HandleButtonReleased(ControllerButtons button)
    {
        CurrentState.HandleButtonReleased(button);
    }

    private void HandleLeftStickUpdated(Vector2 value)
    {
        CurrentState.HandleLeftStickUpdated(value);
    }

    private void HandleHitDealt(HitDetectionEventData eventData)
    {
        FighterStates transition = CurrentState.HandleHitDealt(eventData);

        if (transition != FighterStates.None)
        {
            TransitionToState(transition);
        }
    }

    private void HandleHitTaken(HitDetectionEventData eventData)
    {
        FighterStates transition = CurrentState.HandleHitTaken(eventData);

        if (transition != FighterStates.None)
        {
            TransitionToState(transition);
        }
    }

    protected override void Update()
    {
        if (m_isFrozen) return;

        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (m_isFrozen) return;

        base.FixedUpdate();
    }
}
