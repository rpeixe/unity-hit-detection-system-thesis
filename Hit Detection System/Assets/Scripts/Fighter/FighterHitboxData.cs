using UnityEngine;
using HitDetectionSystem;

[CreateAssetMenu]
public class FighterHitboxData : HitDetectionData
{
    [SerializeField] private FighterHitboxProperties[] m_fighterHitboxProperties;

    public FighterHitboxProperties[] FighterHitboxProperties => m_fighterHitboxProperties;
}
