using UnityEngine;

namespace HitDetectionSystem
{
    public class HitDetectionContext : MonoBehaviour
    {
        [SerializeField] private HitDetectionData m_data;

        public HitDetectionData Data => m_data;
    }
}
