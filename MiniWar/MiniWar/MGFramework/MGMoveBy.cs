using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGMoveBy : MGMoveTo
    {
        protected float LastTime;

        public static MGMoveBy MoveByWithDuration(float duartion, Vector2 pos)
        {
            return new MGMoveBy
                       {
                           Diff = pos,
                           _duration = duartion,
                           LastTime = 0f
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgPos = node.Position;
            Pos = OrgPos + Diff;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                Target.Position = Pos;
                return;
            }
            Vector2 position = Target.Position;
            Target.Position = (t - LastTime)*Diff + position;
            LastTime = t;
        }
    }
}