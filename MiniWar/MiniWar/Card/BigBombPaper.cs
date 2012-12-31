using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Zombie;

namespace MiniWar.Card
{
    class BigBombPaper : Paper
    {
        public BigBombPaper()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("6_2.png");
            Sprite.Anchor = new Vector2(0, 1);
        }

        private bool b;
        private Actor _actor;
        public override void Launch(float dt)
        {
            if (!b)
            {
                for (int i = 0; i < Zombie.ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                {
                    Actor actor = Zombie.ZombieShowLayer.SharedZombieShow().Actors[i];
                    if (actor.Position.X > GameConfig.RelativeOrigin.X + GameConfig.GirdSize * GameConfig.WidthX)
                    {
                        continue;
                    }
                    if (actor.Vector == Vector)
                    {
                        b = true;
                        Detonate();
                        return;
                    }
                }
            }
        }

        public override void Detonate()
        {
            AudioMgr.PlayAudio(0);
            if (_actor != null)
            {
                _actor.ChangeHp(2000, true);
            }

            for (int i = 0; i < Zombie.ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
            {
                Actor actor = Zombie.ZombieShowLayer.SharedZombieShow().Actors[i];
                if (actor == _actor)
                {
                    continue;
                }
                if (actor.AtVector == Vector || actor.Vector == Vector)
                {
                    actor.ChangeHp(2000, true);
                }
            }

            Sprite.Position += new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
            var action1 = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetBombAnimation());
            Sprite.RunAction(MGSequence.Actions(action1, MGCallFunc.ActionWithTarget(() =>
            {
                CardShowLayer.SharedCardShow().RemoveChild(Sprite);
            })));
        }
    }
}
