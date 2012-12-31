namespace MGFramework
{
    public class MGOpacityAction : MGAction
    {
        protected float Opacity;

        public static MGOpacityAction ActionWithOpacity(float opacity)
        {
            return new MGOpacityAction
                       {
                           Opacity = opacity
                       };
        }

        public override void Step(float dt)
        {
            Target.Opacity = Opacity;
            _isEnd = true;
        }
    }
}