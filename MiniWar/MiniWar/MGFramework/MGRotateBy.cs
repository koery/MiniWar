namespace MGFramework
{
    public class MGRotateBy : MGRotateTo
    {
        public static MGRotateBy RotateByWithDuration(float duartion, float angle)
        {
            return new MGRotateBy
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
            OrgAngle = node.Rotation;
            TarAngle = Diff + OrgAngle;
        }
    }
}