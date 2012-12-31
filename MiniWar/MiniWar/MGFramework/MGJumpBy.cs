using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGJumpBy : MGAction
    {
        protected Vector2 OrgPos;
        protected Vector2 Delta;
        protected float Height;
        protected int Jumps;
        public static MGJumpBy ActionWithDuration(float duartion, Vector2 delta, float height, int jumps)
        {
            return new MGJumpBy
            {
                _duration = duartion,
                Delta = delta,
                Height = height,
                Jumps = jumps
            };
        }

        public override void SetTarget(MGNode node)
        {
            this.FirstTick = true;
            this._isEnd = false;
            this.Target = node;
            this.OrgPos = node.Position;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                this._isEnd = true;
                this.Target.Position = new Vector2(this.Delta.X + this.OrgPos.X, this.Delta.Y + this.OrgPos.Y);
                return;
            }
            float num = this.Height * Math.Abs((float)Math.Sin((double)(t * 3.14159274f * (float)this.Jumps)));
            num += this.Delta.Y * t;
            float num2 = this.Delta.X * t;
            this.Target.Position = new Vector2(num2 + this.OrgPos.X, num + this.OrgPos.Y);
        }
    }
}
