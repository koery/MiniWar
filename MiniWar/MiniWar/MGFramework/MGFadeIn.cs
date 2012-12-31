namespace MGFramework
{
    public class MGFadeIn : MGAction
    {
        protected float Diff;
        protected float OrgOpacity;
        protected float TarOpacity;

        public static MGFadeIn ActionWithDuration(float duartion)
        {
            return new MGFadeIn
                       {
                           TarOpacity = 255,
                           _duration = duartion
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgOpacity = 0;
            Diff = TarOpacity - OrgOpacity;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                Target.Opacity = ((byte) TarOpacity);
                return;
            }
            Target.Opacity = ((byte) (t*Diff + OrgOpacity));
        }
    }
}