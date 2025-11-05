using HitDetectionSystem;

public class FighterHurtboxContext : HitDetectionContext
{
    private FighterInputController m_inputController;

    private void Start()
    {
        m_inputController = GetComponentInParent<FighterInputController>(includeInactive: true);
    }

    public bool IsHoldingDown => m_inputController && m_inputController.LeftStickPosition.y <= -0.1;
}
