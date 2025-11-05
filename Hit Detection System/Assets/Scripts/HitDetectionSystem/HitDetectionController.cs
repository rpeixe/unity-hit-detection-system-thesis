using System;
using UnityEngine;

namespace HitDetectionSystem
{
    public class HitDetectionController : MonoBehaviour
    {
        public event Action<HitDetectionEventData> OnHit;

        [SerializeField] protected HitDetectionNode m_hitDetectionTreeRoot;

        private void Awake()
        {
            ulong initialID = 0;
            int initialDepth = 0;
            HitDetectionController controller = this;

            m_hitDetectionTreeRoot.SetupHitDetection(initialID, initialDepth, controller, null);
        }

        public void Hit(HitDetectionContext sourceContext, HitDetectionContext targetContext)
        {
            OnHit?.Invoke(new HitDetectionEventData(sourceContext, targetContext));
        }
    }
}
