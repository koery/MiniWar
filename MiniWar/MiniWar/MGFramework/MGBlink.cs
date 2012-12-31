using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGFramework
{
    public class MGBlink : MGAction
    {
        protected int Times;
        public static MGBlink ActionWithDuration(float duartion, int times)
        {
            return new MGBlink
            {
                _duration = duartion,
                Times = times
            };
        }
        public override void Update(float t)
        {
            if (t == 1f)
            {
                this._isEnd = true;
                this.Target.Visible = true;
                return;
            }
            float slice = 1.0f / Times;
            float m = t % slice;
            Target.Visible = m > slice / 2;
        }
    }
}
