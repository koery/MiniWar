using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGAnimate : MGAction
    {
        public MGAnimation Anim;
        public string AnimName;
        public FrameStruct OrgFS;
        public bool RestoreOriginalFrame;

        public static MGAnimate ActionWithAnimation(MGAnimation anim)
        {
            return new MGAnimate
                       {
                           Anim = anim,
                           _duration = anim.Duration,
                           AnimName = anim.Name,
                           RestoreOriginalFrame = true
                       };
        }

        public static MGAnimate ActionWithAnimation(MGAnimation anim, bool restoreOriginalFrame)
        {
            return new MGAnimate
                       {
                           Anim = anim,
                           _duration = anim.Duration,
                           AnimName = anim.Name,
                           RestoreOriginalFrame = restoreOriginalFrame
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgFS = (Target as MGSprite).FS;
            ((MGSprite)Target).AddAnimation(Anim);
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                if (RestoreOriginalFrame)
                {
                    ((MGSprite)Target).FS = OrgFS;
                }
                return;
            }
            FrameStruct frameByTime = Anim.GetFrameByTime(t);
            ((MGSprite)Target).FS = frameByTime;
        }
    }
}