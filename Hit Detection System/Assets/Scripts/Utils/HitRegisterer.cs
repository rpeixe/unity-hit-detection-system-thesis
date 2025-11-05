using UnityEngine;

public class HitRegisterer : MonoBehaviour
{
    public static HitRegisterer Instance { get; private set; }

    public int NumberOfHitsOnPlayer1 { get; private set; }
    public int NumberOfHitsOnPlayer2 { get; private set; }

    public HitDecision LastHitDecisionOnPlayer1 { get; private set; }
    public HitDecision LastHitDecisionOnPlayer2 { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterHit(int player, HitDecision hitDecision)
    {
        if (player == 1)
        {
            NumberOfHitsOnPlayer1++;
            LastHitDecisionOnPlayer1 = hitDecision;
        }
        else if (player == 2)
        {
            NumberOfHitsOnPlayer2++;
            LastHitDecisionOnPlayer2 = hitDecision;
        }
        else
        {
            Debug.LogError("Invalid player number.");
        }
    }
}
