using UnityEngine;
using HitDetectionSystem;

[CreateAssetMenu]
public class FighterHurtboxData : HitDetectionData
{
    [SerializeField] private FighterHurtboxProperties[] m_fighterHurtboxProperties;

    public FighterHurtboxProperties[] FighterHurtboxProperties => m_fighterHurtboxProperties;
}
