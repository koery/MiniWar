using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;

namespace MGFramework
{
    class MGTintBy : MGAction
    {
        protected short m_deltaR;
        protected short m_deltaG;
        protected short m_deltaB;

        protected short m_fromR;
        protected short m_fromG;
        protected short m_fromB;

        public static MGTintBy ActionWithDuration(float duration, short deltaRed, short deltaGreen, short deltaBlue)
        {
            MGTintBy ret = new MGTintBy() { _duration = duration, m_deltaR = deltaRed, m_deltaG = deltaGreen, m_deltaB = deltaBlue };
            return ret;
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            m_fromR = node.Color.R;
            m_fromG = node.Color.G;
            m_fromB = node.Color.B;
        }

        public override void Update(float dt)
        {
            if (dt == 1f)
            {
                _isEnd = true;
                return;
            }
            Target.SetColor((byte)(m_fromR + m_deltaR * dt),
                            (byte)(m_fromG + m_deltaG * dt),
                            (byte)(m_fromB + m_deltaB * dt));
        }
    }
}
