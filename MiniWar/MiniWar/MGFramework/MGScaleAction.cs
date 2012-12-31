using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGScaleAction : MGAction
    {
        protected float ScaleX;
        protected float ScaleY;

        public static MGScaleAction ActionWithScale(float scale)
        {
            return new MGScaleAction
                       {
                           ScaleX = scale,
                           ScaleY = scale
                       };
        }

        public static MGScaleAction ActionWithScaleXY(float scaleX, float scaleY)
        {
            return new MGScaleAction
                       {
                           ScaleX = scaleX,
                           ScaleY = scaleY
                       };
        }

        public override void Step(float dt)
        {
            Target.Scale = new Vector2(ScaleX, ScaleY);
            _isEnd = true;
        }
    }
}