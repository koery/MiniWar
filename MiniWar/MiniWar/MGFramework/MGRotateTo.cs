namespace MGFramework
{
    public class MGRotateTo : MGAction
    {
        protected float Diff;
        protected float OrgAngle;
        protected float TarAngle;

        public static MGRotateTo ActionWithDuration(float duartion, float angle)
        {
            return new MGRotateTo
                       {
                           TarAngle = angle,
                           _duration = duartion
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgAngle = node.Rotation;
            Diff = TarAngle - OrgAngle;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                Target.Rotation = TarAngle;
                return;
            }
            Target.Rotation = t*Diff + OrgAngle;
        }
    }
}