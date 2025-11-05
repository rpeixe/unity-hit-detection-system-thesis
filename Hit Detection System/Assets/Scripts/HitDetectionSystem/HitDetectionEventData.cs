namespace HitDetectionSystem
{
    public readonly struct HitDetectionEventData
    {
        public HitDetectionContext Source { get; }
        public HitDetectionContext Target { get; }

        public HitDetectionEventData(HitDetectionContext source, HitDetectionContext target)
        {
            Source = source;
            Target = target;
        }
    }
}
