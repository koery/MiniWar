using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGPlace : MGAction
    {
        protected Vector2 Pos;

        public static MGPlace ActionWithPosition(Vector2 pos)
        {
            return new MGPlace
                       {
                           Pos = pos
                       };
        }

        public override void Step(float dt)
        {
            Target.Position = Pos;
            _isEnd = true;
        }
    }
}