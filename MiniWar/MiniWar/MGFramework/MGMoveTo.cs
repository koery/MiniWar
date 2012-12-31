using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGMoveTo : MGAction
    {
        protected Vector2 Diff;
        protected Vector2 OrgPos;
        protected Vector2 Pos;

        public static MGMoveTo ActionWithDuration(float duartion, Vector2 pos)
        {
            return new MGMoveTo
                       {
                           Pos = pos,
                           _duration = duartion
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgPos = node.Position;
            Diff = Pos - OrgPos;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                Target.Position = Pos;
                return;
            }
            Target.Position = t*Diff + OrgPos;
        }
    }
}