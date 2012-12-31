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
    class HorizontalPaper : Paper
    {
        private MGSprite _skill;
        private MGSprite _cardeffect;
        public int Tag { get; set; }
        public HorizontalPaper()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("5_2.png");
            _skill = MGSprite.MGSpriteWithSpriteFrameName("sd1.png");
            _cardeffect = MGSprite.MGSpriteWithSpriteFrameName("sd3.png");


            //_skill.Anchor = new Vector2(0, 1);
            Sprite.Anchor = new Vector2(0, 1);
            CardShowLayer.SharedCardShow().AddChild(_skill);
            CardShowLayer.SharedCardShow().AddChild(_cardeffect);
            _cardeffect.Visible = false;
            _skill.Visible = false;
        }

        public override void SetVector(int x, int y)
        {
            if (_skill != null)
            {
                _skill.Position = new IntVector(0, y).ToUIVector2();
                _skill.Position = new Vector2(4.5f * GameConfig.GirdSize + GameConfig.RelativeOrigin.X, _skill.Position.Y + GameConfig.GirdSize / 2f);
                _cardeffect.Position = new IntVector(x, y).ToUIVector2() + new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f + 30);
            }
            base.SetVector(x, y);
        }


        public override void Launch(float dt)
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
                    Detonate();
                    return;
                }
            }
        }

        public override void Detonate()
        {
            AudioMgr.PlayAudio(2);
            CardShowLayer.SharedCardShow().Papers.Remove(this);
            if (Tag == 1)
            {
                CardShowLayer.SharedCardShow().IsHas[Vector.Y] = false;
            }
            var action = MGSequence.Actions(MGJumpBy.ActionWithDuration(.5f, new Vector2(0, 0), 20, 3),
                               MGCallFunc.ActionWithTarget(() =>
                                                               {
                                                                   for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                                                                   {
                                                                       var actor = ZombieShowLayer.SharedZombieShow().Actors[i];
                                                                       if (actor.Position.X > GameConfig.RelativeOrigin.X + GameConfig.GirdSize * GameConfig.WidthX)
                                                                       {
                                                                           continue;
                                                                       }
                                                                       if (actor.Vector.Y == this.Vector.Y)
                                                                       {
                                                                           actor.ChangeHp(2000);
                                                                       }
                                                                   }
                                                                   _skill.Visible = true;
                                                                   _cardeffect.Visible = true;
                                                                   _cardeffect.RunAction(MGSequence.Actions(MGRepeat.Actions(MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetElectric2Animation()), 2), new MGHide()));
                                                                   CardShowLayer.SharedCardShow().RemoveChild(Sprite);
                                                                   _skill.RunAction(MGSequence.Actions(MGRepeat.Actions(MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetElectricAnimation()), 2), MGCallFunc.ActionWithTarget(() =>
                                                                   {
                                                                       //for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                                                                       //{
                                                                       //    var actor = ZombieShowLayer.SharedZombieShow().Actors[i];
                                                                       //    if (actor.Position.X > GameConfig.RelativeOrigin.X + GameConfig.GirdSize * GameConfig.WidthX)
                                                                       //    {
                                                                       //        continue;
                                                                       //    }
                                                                       //    if (actor.Vector.Y == this.Vector.Y)
                                                                       //    {
                                                                       //        actor.Dead();
                                                                       //    }
                                                                       //}
                                                                   }), new MGHide(), MGCallFunc.ActionWithTarget(() =>
                                                                   {
                                                                       CardShowLayer.SharedCardShow().RemoveChild(_skill);
                                                                       CardShowLayer.SharedCardShow().RemoveChild(_cardeffect);
                                                                   })));
                                                               }));
            Sprite.RunAction(action);
        }


    }
}
