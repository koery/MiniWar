namespace MGFramework
{
    public class MGColorAction : MGAction
    {
        protected float _b;
        protected float _g;
        protected float _r;

        public static MGColorAction ActionWithRGB(float R, float G, float B)
        {
            return new MGColorAction
                       {
                           _r = R,
                           _g = G,
                           _b = B
                       };
        }

        public override void Step(float dt)
        {
            Target.SetColor(_r, _g, _b);
            _isEnd = true;
        }
    }
}