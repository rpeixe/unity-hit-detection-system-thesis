using System.Linq;
using UnityEngine;
using HitDetectionSystem;

public abstract class FighterState : State<FighterStates>
{
    protected Vector2 LeftStickPosition;

    public new FighterStateMachine StateMachine => (FighterStateMachine)base.StateMachine;

    protected FighterState(FighterStates key, FighterStateMachine stateMachine) : base(key, stateMachine)
    {
    }

    public abstract void HandleButtonPressed(ControllerButtons button);
    public abstract void HandleButtonReleased(ControllerButtons button);
    public virtual void HandleLeftStickUpdated(Vector2 direction)
    {
        LeftStickPosition = direction;
    }

    public virtual FighterStates HandleHitDealt(HitDetectionEventData eventData)
    {
        return FighterStates.None;
    }

    public virtual FighterStates HandleHitTaken(HitDetectionEventData eventData)
    {
        //LogProperties(eventData);

        FighterHitboxData hitboxData = (FighterHitboxData)eventData.Source.Data;
        FighterHurtboxContext hurtboxContext = (FighterHurtboxContext)eventData.Target;
        FighterHurtboxData hurtboxData = (FighterHurtboxData)hurtboxContext.Data;

        bool overheadAttack = hitboxData.FighterHitboxProperties.Contains(FighterHitboxProperties.Overhead);
        bool lowAttack = hitboxData.FighterHitboxProperties.Contains(FighterHitboxProperties.Low);
        bool airAttack = hitboxData.FighterHitboxProperties.Contains(FighterHitboxProperties.Air);
        bool criticalAttack = hitboxData.FighterHitboxProperties.Contains(FighterHitboxProperties.Critical);

        bool defenderBlocking = hurtboxData.FighterHurtboxProperties.Contains(FighterHurtboxProperties.Block);
        bool defenderAttacking = hurtboxData.FighterHurtboxProperties.Contains(FighterHurtboxProperties.Attack);
        bool defenderAntiAiring = hurtboxData.FighterHurtboxProperties.Contains(FighterHurtboxProperties.AntiAir);

        if (airAttack && defenderAntiAiring)
        {
            ScreenPrinter.Instance.Print("Ignorou ataque aéreo!", StateMachine.PlayerID);

            HitRegisterer.Instance.RegisterHit(StateMachine.PlayerID, new HitDecision
            {
                Ignored = true,
            });
            return FighterStates.None;
        }

        if (defenderBlocking)
        {
            if ((hurtboxContext.IsHoldingDown && !overheadAttack) || (!hurtboxContext.IsHoldingDown && !lowAttack))
            {
                ScreenPrinter.Instance.Print("Bloqueou ataque!", StateMachine.PlayerID);

                HitRegisterer.Instance.RegisterHit(StateMachine.PlayerID, new HitDecision
                {
                    Blocked = true,
                });
                return FighterStates.None;
            }
        }

        string message = "Atingido por ataque!";
        HitDecision hitDecision = new HitDecision();

        if (defenderAttacking)
        {
            message += "\nContra ataque!";

            hitDecision.Counter = true;
        }

        if (criticalAttack)
        {
            message += "\nPonto crítico!";

            hitDecision.Critical = true;
        }

        ScreenPrinter.Instance.Print(message, StateMachine.PlayerID);
        HitRegisterer.Instance.RegisterHit(StateMachine.PlayerID, hitDecision);

        return FighterStates.None;
    }

    private void LogProperties(HitDetectionEventData eventData)
    {
        Debug.Log($"Attack properties:");
        FighterHitboxData hitboxData = (FighterHitboxData)eventData.Source.Data;

        foreach (var property in hitboxData.FighterHitboxProperties)
        {
            Debug.Log(property.ToString());
        }

        Debug.Log($"Defense properties:");
        FighterHurtboxContext hurtboxContext = (FighterHurtboxContext)eventData.Target;
        FighterHurtboxData hurtboxData = (FighterHurtboxData)hurtboxContext.Data;

        foreach (var property in hurtboxData.FighterHurtboxProperties)
        {
            if (property == FighterHurtboxProperties.Block)
            {
                Debug.Log($"{(hurtboxContext.IsHoldingDown ? "Low" : "High")} Block");
            }
            else
            {
                Debug.Log(property.ToString());
            }
        }
    }
}
