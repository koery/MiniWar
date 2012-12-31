namespace MGFramework
{
    public class MGRotateXTo : MGAction
    {
        protected float Diff;
        protected float OrgAngle;
        protected float TarAngle;

        public static MGRotateXTo ActionWithDuration(float duartion, float angle)
        {
            return new MGRotateXTo
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
            OrgAngle = node.RotationAlongX;
            Diff = TarAngle - OrgAngle;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                Target.RotationAlongX = TarAngle;
                return;
            }
            Target.RotationAlongX = t*Diff + OrgAngle;
        }
    }
}