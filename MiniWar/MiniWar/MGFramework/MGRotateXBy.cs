namespace MGFramework
{
    public class MGRotateXBy : MGRotateXTo
    {
        public new static MGRotateXBy ActionWithDuration(float duartion, float angle)
        {
            return new MGRotateXBy
                       {
                           Diff = angle,
                           _duration = duartion
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgAngle = node.RotationAlongX;
            TarAngle = Diff + OrgAngle;
        }
    }
}