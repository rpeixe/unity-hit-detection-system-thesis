using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HitDetectionSystem
{
    [RequireComponent(typeof(HitDetectionZone))]
    [RequireComponent(typeof(Collider2D))]
    public class HitDetection2DCollider : MonoBehaviour
    {
        private HitDetectionZone m_hitDetectionZone;
        private Collider2D m_collider;

        public HitDetectionZone HitDetectionZone => m_hitDetectionZone;

        private void Awake()
        {
            m_hitDetectionZone = GetComponent<HitDetectionZone>();
            m_collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            StartCoroutine(nameof(CheckOverlapsNextFrame));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            HandleCollision(collision);
        }

        private IEnumerator CheckOverlapsNextFrame()
        {
            yield return null;
            List<Collider2D> results = new();
            int count = m_collider.Overlap(results);

            for (int i = 0; i < count; i++)
            {
                Collider2D otherCollider = results[i];

                HandleCollision(otherCollider);
            }
        }

        private void HandleCollision(Collider2D collision)
        {
            if (collision.TryGetComponent(out HitDetectionZone otherHitDetectionZone))
            {
                HitDetectionZone.RegisterOverlap(otherHitDetectionZone);
            }
        }
    }
}
