using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Toobz;
using MiniWar.Zombie;

namespace MiniWar.Card
{
    class RandomWirePaper : Paper
    {
        private bool _b = false;
        private float _timeleft = 0;
        public RandomWirePaper()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("Cloud_white_01.png");
            Sprite.Anchor = new Vector2(0, 1);
        }

        public override void Launch(float dt)
        {
            if (!_b)
            {
                if ((_timeleft -= dt) <= 0)
                {
                    _b = true;
                    Detonate();
                }
            }
        }

        public override void Detonate()
        {
            AudioMgr.PlayAudio(2);
            var action1 = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetCloudAnimation());
            Sprite.Position += new Vector2(94 / 2, 94 / 2);
            Sprite.RunAction(MGSequence.Actions(action1, MGCallFunc.ActionWithTarget(() =>
            {
                CardShowLayer.SharedCardShow().RemoveChild(Sprite);
                var oldstate = WireShowLayer.SharedWireShow().WireDictionary[Vector].WireState;
                WireShowLayer.SharedWireShow().WireDictionary[Vector].ChangeWireState(RandomWireType(oldstate));
            })));
        }


        private WireType RandomWireType(WireType type)
        {
            List<WireType> list = new List<WireType>()
                                           {
                                               WireType.Wirei,
                                               WireType.WireL,
                                               WireType.WireI,
                                               WireType.WireT,
                                               WireType.WireX
                                           };
            list.Remove(type);
            var value = new Random().Next(0, 4);
            return list[value];
        }
    }
}
