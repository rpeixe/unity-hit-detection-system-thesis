using System.Collections.Generic;

namespace HitDetectionSystem
{
    public class HitDetectionZone : HitDetectionNode
    {
        private readonly List<HitDetectionZone> m_overlaps = new();

        private void OnDisable()
        {
            ClearOverlaps();
        }

        public override HashSet<HitDetectionController> DetectHit(HashSet<HitDetectionController> treesToIgnore)
        {
            if (gameObject.activeInHierarchy == false)
            {
                return treesToIgnore;
            }

            foreach (HitDetectionZone overlap in m_overlaps)
            {
                if (overlap.Controller != Controller && !treesToIgnore.Contains(overlap.Controller))
                {
                    Controller.Hit(Context, overlap.Context);
                    overlap.Controller.Hit(Context, overlap.Context);
                    treesToIgnore.Add(overlap.Controller);
                }
            }

            return treesToIgnore;
        }

        public void RegisterOverlap(HitDetectionZone newOverlap)
        {
            int index = m_overlaps.BinarySearch(newOverlap, Comparer<HitDetectionZone>.Create((a, b) => a.ID.CompareTo(b.ID)));
            if (index < 0) index = ~index;
            m_overlaps.Insert(index, newOverlap);
        }

        public void ClearOverlaps()
        {
            m_overlaps.Clear();
        }
    }
}
