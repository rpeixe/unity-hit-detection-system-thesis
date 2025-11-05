using System.Collections.Generic;

namespace HitDetectionSystem
{
    public class HitDetectionControllerActive : HitDetectionController
    {
        private readonly HashSet<HitDetectionController> m_treesToIgnore = new();

        public void ClearTreesToIgnore()
        {
            m_treesToIgnore.Clear();
        }

        private void OnDisable()
        {
            ClearTreesToIgnore();
        }

        private void FixedUpdate()
        {
            m_hitDetectionTreeRoot.DetectHit(m_treesToIgnore);
        }
    }
}
