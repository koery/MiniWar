namespace MGFramework
{
    public class MGRotationAction : MGAction
    {
        protected float Rotation;

        public static MGRotationAction ActionWithRotation(float rotation)
        {
            return new MGRotationAction
                       {
                           Rotation = rotation
                       };
        }

        public override void Step(float dt)
        {
            Target.Rotation = Rotation;
            _isEnd = true;
        }
    }
}