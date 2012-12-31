using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGParabolaJumpBy : MGAction
    {
        public float A;
        public float B;
        public float C;
        public Vector2 Delta;
        public float Height;
        public Vector2 RefPoint;
        public Vector2 StartPosition;

        public static MGParabolaJumpBy ParabolaJumpByWithDuration(float duration, Vector2 position, Vector2 RefPoint)
        {
            return new MGParabolaJumpBy
                       {
                           Delta = position,
                           _duration = duration,
                           RefPoint = RefPoint
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            StartPosition = Target.Position;
            A = (RefPoint.Y/RefPoint.X - Delta.Y/Delta.X)/(RefPoint.X - Delta.X);
            B = Delta.Y/Delta.X - (Delta.X + 2f*StartPosition.X)*A;
            C = StartPosition.Y - A*StartPosition.X*StartPosition.X - B*StartPosition.X;
        }

        public override void Update(float t)
        {
            float num = t*Delta.X + StartPosition.X;
            float num2 = num;
            float y = A*num2*num2 + B*num2 + C;
            Target.Position = new Vector2(num2, y);
        }
    }
}