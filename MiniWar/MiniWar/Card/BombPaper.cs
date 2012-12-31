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
    class BombPaper : Paper
    {
        public BombPaper()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("7_2.png");
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
        }

        public override void Detonate()
        {
            AudioMgr.PlayAudio(0);
            Sprite.Position += new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
            var action1 = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetBombAnimation2());
            Sprite.RunAction(MGSequence.Actions(action1, MGCallFunc.ActionWithTarget(() =>
            {
                if (_actor != null)
                {
                    _actor.ChangeHp(1000, true);
                }
                CardShowLayer.SharedCardShow().RemoveChild(Sprite);
            })));
        }
    }
}
