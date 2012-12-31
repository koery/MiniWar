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
    class BigIcePaper : Paper
    {
        public BigIcePaper()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("9_2.png");
            Sprite.Anchor = new Vector2(0, 1);
        }

        private bool b;
        public override void Launch(float dt)
        {
            if (!b)
            {
                Sprite.RunAction(MGSequence.Actions(MGBlink.ActionWithDuration(2.4f, 6), MGCallFunc.ActionWithTarget(() =>
                {
                    Detonate();
                })));
                b = true;
            }
            base.Launch(dt);
        }

        public override void Detonate()
        {
            AudioMgr.PlayAudio(2);
            Sprite.Position += new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
            var action1 = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetBigIceAnimation());
            Sprite.RunAction(MGSequence.Actions(action1, MGCallFunc.ActionWithTarget(() =>
            {
                Sprite.Visible = false;
                for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                {
                    var actor = ZombieShowLayer.SharedZombieShow().Actors[i];
                    actor.StopAllAction();
                    actor.Sprite.StopAllAction();
                    actor.Sprite.SetColor(0, 163, 255);
                    actor.Sprite.RunAction(MGSequence.Actions(MGTintTo.ActionWithDuration(8, 255, 255, 255), MGCallFunc.ActionWithTarget(() =>
                    {
                        actor.GoRun();
                    })));

                }
                CardShowLayer.SharedCardShow().RemoveChild(Sprite);
            })));

        }
    }
}
