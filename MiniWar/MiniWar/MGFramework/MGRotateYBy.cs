namespace MGFramework
{
    public class MGRotateYBy : MGRotateXTo
    {
        public new static MGRotateYBy ActionWithDuration(float duartion, float angle)
        {
            return new MGRotateYBy
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
            OrgAngle = node.RotationAlongY;
            TarAngle = Diff + OrgAngle;
        }
    }
}