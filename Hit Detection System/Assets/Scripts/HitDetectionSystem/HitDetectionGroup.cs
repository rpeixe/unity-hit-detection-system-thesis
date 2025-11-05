using System;
using System.Collections.Generic;

namespace HitDetectionSystem
{
    public class HitDetectionGroup : HitDetectionNode
    {
        public List<HitDetectionNode> Nodes;

        public override HashSet<HitDetectionController> DetectHit(HashSet<HitDetectionController> treesToIgnore)
        {
            if (gameObject.activeInHierarchy == false)
            {
                return treesToIgnore;
            }

            foreach (HitDetectionNode node in Nodes)
            {
                HashSet<HitDetectionController> result = node.DetectHit(treesToIgnore);
            
                if (result != null)
                {
                    treesToIgnore.UnionWith(result);
                }
            }

            return treesToIgnore;
        }

        public override void SetupHitDetection(ulong id, int depth, HitDetectionController controller, HitDetectionContext context)
        {
            if (depth > 9)
            {
                throw new Exception("Hit detection trees can't be more than 10 nodes tall.");
            }

            if (Nodes.Count > 99)
            {
                throw new Exception("Hit detection trees can't be more than 100 nodes wide.");
            }

            base.SetupHitDetection(id, depth, controller, context);

            for (int i = 0; i < Nodes.Count; i++)
            {
                int childDepth = depth + 1;
                ulong childID = GetChildId(i, id, childDepth);

                Nodes[i].SetupHitDetection(childID, childDepth, controller, Context);
            }
        }

        private static ulong GetChildId(int childId, ulong parentId, int depth)
        {
            if (depth < 1 || depth > 9)
                throw new ArgumentOutOfRangeException(nameof(depth), "Depth must be between 1 and 9 (root = 0 is implicit).");
            if (childId < 0 || childId > 99)
                throw new ArgumentOutOfRangeException(nameof(childId), "Child ID must be between 0 and 99.");

            string parentStr = parentId.ToString("D20");
            char[] chars = parentStr.ToCharArray();

            int pos = 2 + (depth - 1) * 2;

            string childStr = childId.ToString("D2");
            chars[pos] = childStr[0];
            chars[pos + 1] = childStr[1];

            if (chars[0] > '1')
                throw new InvalidOperationException("Resulting ID would exceed ulong max range (leading digit > 1).");

            return ulong.Parse(new string(chars));
        }
    }
}
