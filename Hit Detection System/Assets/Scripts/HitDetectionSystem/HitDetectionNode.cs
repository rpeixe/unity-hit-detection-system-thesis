using System.Collections.Generic;
using UnityEngine;

namespace HitDetectionSystem
{
    public abstract class HitDetectionNode : MonoBehaviour
    {
        private ulong m_id;
        private HitDetectionController m_controller;
        private HitDetectionContext m_context;

        public ulong ID => m_id;
        public HitDetectionController Controller => m_controller;
        public HitDetectionContext Context => m_context;

        public abstract HashSet<HitDetectionController> DetectHit(HashSet<HitDetectionController> treesToIgnore);

        public virtual void SetupHitDetection(ulong id, int depth, HitDetectionController controller, HitDetectionContext context)
        {
            m_controller = controller;
            m_id = id;

            if (TryGetComponent(out HitDetectionContext existingContext))
            {
                m_context = existingContext;
            }
            else
            {
                m_context = context;
            }

            if (m_context == null)
            {
                throw new System.Exception($"HitDetectionNode {name} or one of its parents must have a context.");
            }
        }
    }
}
