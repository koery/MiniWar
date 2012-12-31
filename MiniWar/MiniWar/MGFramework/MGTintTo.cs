using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    class MGTintTo : MGAction
    {
        public static MGTintTo ActionWithDuration(float duration, byte red, byte green, byte blue)
        {
            MGTintTo cCTintTo = new MGTintTo() { _duration = duration, m_to = new Color(red, green, blue) };
            return cCTintTo;
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            this.m_from = node.Color;
            base.SetTarget(node);
        }

        public override void Update(float dt)
        {
            if (dt == 1f)
            {
                _isEnd = true;
                return;
            }
            Target.SetColor((byte)((float)this.m_from.R + (float)(this.m_to.R - this.m_from.R) * dt), (byte)((float)this.m_from.G + (float)(this.m_to.G - this.m_from.G) * dt), (byte)((float)this.m_from.B + (float)(this.m_to.B - this.m_from.B) * dt));
            base.Update(dt);
        }

        protected Color m_to;
        protected Color m_from;
    }
}
