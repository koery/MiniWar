using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGJumpTo : MGJumpBy
    {
        public static MGJumpTo JumpToWithDuration(float duartion, Vector2 delta, float height, int jumps)
        {
            return new MGJumpTo
                       {
                           _duration = duartion,
                           Delta = delta,
                           Height = height,
                           Jumps = jumps
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgPos = node.Position;
            Delta = new Vector2(Delta.X - OrgPos.X, Delta.Y - OrgPos.Y);
        }
    }
}