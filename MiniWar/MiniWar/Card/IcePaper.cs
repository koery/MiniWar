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
    class IcePaper : Paper
    {
        public IcePaper()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("8_2.png");
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
                    if (actor.AtVector == Vector)
                    {
                        _actor = actor;
                        Detonate();
                        b = true;
                        return;
                    }
                }
            }
            base.Launch(dt);
        }

        public override void Detonate()
        {
            AudioMgr.PlayAudio(2);
            Sprite.Position += new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
            var action1 = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetIceAnimation());
            Sprite.RunAction(MGSequence.Actions(action1, MGCallFunc.ActionWithTarget(() =>
            {
                Sprite.Visible = false;

                _actor.StopAllAction();
                _actor.Sprite.StopAllAction();
                _actor.Sprite.SetColor(0, 163, 255);
                _actor.Sprite.RunAction(MGSequence.Actions(MGTintTo.ActionWithDuration(4, 255, 255, 255), MGCallFunc.ActionWithTarget(() =>
                {
                    _actor.GoRun();
                })));

                CardShowLayer.SharedCardShow().RemoveChild(Sprite);
            })));

        }
    }
}
