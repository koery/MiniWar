namespace MGFramework
{
    public class MGRotateYTo : MGAction
    {
        protected float Diff;
        protected float OrgAngle;
        protected float TarAngle;

        public static MGRotateYTo ActionWithDuration(float duartion, float angle)
        {
            return new MGRotateYTo
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
            OrgAngle = node.RotationAlongY;
            Diff = TarAngle - OrgAngle;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                Target.RotationAlongY = TarAngle;
                return;
            }
            Target.RotationAlongY = t*Diff + OrgAngle;
        }
    }
}