using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Toobz;

namespace MiniWar.Zombie
{
    class ActorFast : Actor
    {
        public ActorFast()
        {
            Speed = 0;
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("50010.png");
            _y = Control.SharedControl().GetRandom(0, 5);
            Position = new Vector2(1360, _y * 94 + 57);
            var next = new Random().Next(-10, 10);
            Sprite.Position = new Vector2(0, 55 + next);

            ComeDirection = WireDirection.WireRight;
            GoDirection = WireDirection.WireLeft;
            AddChild(Sprite);
            RunAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(5001)[0]);
            StandAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(5001)[0]);
            DieAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(5001)[2]);
            _animate = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(5001)[1]);
            Hp = 1000;
        }
    }
}
