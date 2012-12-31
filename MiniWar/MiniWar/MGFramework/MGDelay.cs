using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGFramework
{
    public class MGDelay : MGAction
    {
        public static MGDelay ActionWithDuration(float duartion)
        {
            return new MGDelay
            {
                _duration = duartion
            };
        }
        public override void Update(float t)
        {
            if (t == 1f)
            {
                this._isEnd = true;
            }
        }
    }
}
