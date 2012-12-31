using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Zombie;
using VSZombie.Toobz;

namespace MiniWar.Card
{
    class RocketPaper : Paper
    {
        public RocketPaper()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("2_2.png");
            Sprite.Anchor = new Vector2(0, 1);
        }

        private Actor _actor;
        private bool b = false;

        public override void Launch(float dt)
        {
            if (!b)
            {
                for (int i = 0; i < Zombie.ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                {
                    Actor actor = Zombie.ZombieShowLayer.SharedZombieShow().Actors[i];
                    if (actor.Vector == Vector)
                    {
                        _actor = actor;
                        Detonate();
                        b = true;
                        return;
                    }
                }
            }

        }

        public override void Detonate()
        {
            AudioMgr.PlayAudio(2);
            var endpos = new Vector2(1300, GameConfig.GirdSize * Vector.Y + GameConfig.RelativeOrigin.Y + GameConfig.GirdSize / 2f);
            if (_actor != null)
            {
                _actor.StopAllAction();
                _actor.Vector = new IntVector(GameConfig.WidthX - 1, Vector.Y);

                _actor.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(2f, endpos), MGCallFunc.ActionWithTarget(() =>
                {
                    _actor.GoRun();
                })));
            }
            Sprite.Position += new Vector2(0, 100);
            var action1 = MGRepeat.Actions(MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetRocketAnimation()), 4);
            var action2 = MGMoveTo.ActionWithDuration(2f, endpos);
            Sprite.RunAction(MGSequence.Actions(MGSpawn.Actions(action1, action2), new MGHide(), MGCallFunc.ActionWithTarget(() =>
            {
                CardShowLayer.SharedCardShow().RemoveChild(Sprite);
            })));
        }
    }
}
