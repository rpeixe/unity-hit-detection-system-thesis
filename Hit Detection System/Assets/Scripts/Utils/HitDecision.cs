using UnityEngine;

public struct HitDecision
{
    public bool Blocked { get; set; }
    public bool Critical { get; set; }
    public bool Counter { get; set; }
    public bool Ignored { get; set; }
}
