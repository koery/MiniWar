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
    class ActorBabe : Actor
    {
        public ActorBabe()
        {
            Speed = 3;
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("40010.png");
            _y = Control.SharedControl().GetRandom(0, 5);
            Position = new Vector2(1360, _y * 94 + 57);
            var next = new Random().Next(-10, 10);
            Sprite.Position = new Vector2(0, 20 + next);

            ComeDirection = WireDirection.WireRight;
            GoDirection = WireDirection.WireLeft;
            AddChild(Sprite);
            RunAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(4001)[0]);
            StandAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(4001)[1]);
            DieAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(4001)[2]);
            _animate = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(4001)[3]);
            Hp = 800;

        }
    }
}
