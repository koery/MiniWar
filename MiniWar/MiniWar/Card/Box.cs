using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;

namespace MiniWar.Card
{
    class Box : MGLayer
    {
        private static Box _shardBox;
        public static Box ShardBox()
        {
            return _shardBox;
        }

        private List<MGSprite> _repeatSprite;
        public MGSprite JingbiSprite;
        private MGAnimate _jbAni;

        public Box()
        {
            _repeatSprite = new List<MGSprite>();
            MGSprite cardSp1 = MGSprite.MGSpriteWithSpriteFrameName("卡槽1.png");

            MGSprite cardSp3 = MGSprite.MGSpriteWithSpriteFrameName("卡槽3.png");
            cardSp1.Anchor = new Vector2(0, .5f);

            cardSp3.Anchor = new Vector2(0, .5f);
            AddChild(cardSp1);
            AddChild(cardSp3);
            for (int i = 6; i <= GameConfig.CardCount; i++)
            {
                MGSprite cardSp2 = MGSprite.MGSpriteWithSpriteFrameName("卡槽2.png");
                cardSp2.Anchor = new Vector2(0, .5f);
                AddChild(cardSp2);
                cardSp2.Position = new Vector2(394 + (i - 6) * 73, 0);
                _repeatSprite.Add(cardSp2);
            }

            cardSp3.Position = new Vector2(73 * (GameConfig.CardCount + 1 - 6) + 394, 0);

            JingbiSprite = MGSprite.MGSpriteWithSpriteFrameName("jb1.png");
            AddChild(JingbiSprite);
            JingbiSprite.Position = new Vector2(48, 15);

            var frames = new List<MGSpriteFrame>();
            var cache = MGSpriteFrameCache.SharedSpriteFrameCache();
            for (int i = 1; i < 7; i++)
            {
                var frame = cache.SpriteFrameByName("jb" + i + ".png");
                frames.Add(frame);
            }
            _jbAni = MGAnimate.ActionWithAnimation(MGAnimation.animationWithFrames(frames));
            frames.Clear();

            MGLabel label = new MGLabel(GameConfig.Money.ToString(), MGLabel.LabelType.TextAlignmentRight, "Card/QuartzMS");
            AddChild(label);
            label.SetColor(new Color(255, 0, 0));
            label.Position = new Vector2(79, -35);
            GameConfig.ChangeMoneyCallBack = () => { JingbiSprite.RunAction(MGRepeat.Actions(_jbAni, 2)); label.SetString(GameConfig.Money.ToString()); };

            MGSprite shopBox = MGSprite.MGSpriteWithSpriteFrameName("商店卡槽.png");
            AddChild(shopBox);
            shopBox.Anchor = new Vector2(0, 0);
            shopBox.Position = new Vector2(-380, -56);
            TouchLayer.ShopCallBack1 = (sender) =>
            {
                sender.IsTouchEnable = false; sender.RunAction(MGMoveTo.ActionWithDuration(.3f, new Vector2(388, 500)));
                shopBox.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(.3f, new Vector2(0, -56)), MGCallFunc.ActionWithTarget(() =>
                 {

                     sender.IsTouchEnable = true;
                 })));
            };
            TouchLayer.ShopCallBack2 = (sender) =>
            {
                sender.IsTouchEnable = false; sender.RunAction(MGMoveTo.ActionWithDuration(.3f, new Vector2(40, 500)));
                shopBox.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(.3f, new Vector2(-380, -56)), MGCallFunc.ActionWithTarget(() =>
                {
                    sender.IsTouchEnable = true;
                })));
            };


            for (int i = 0; i < 8; i++)
            {
                AudioMgr.PlayAudio(1);
                shopBox.AddChild(CardConfig.PaperInfos[i].Sprite);
                CardConfig.PaperInfos[i].Sprite.Position = new Vector2(60 + 67 * (i % 5), i / 5 * -94 - 97);
                MainGameLogic.SharedMainGameLogic().PaperInfos.Add(CardConfig.PaperInfos[i]);
            }


        }


        public void UpdateBox()
        {
            for (int i = 6; i <= GameConfig.CardCount; i++)
            {
                int index = i - 6;
                if (_repeatSprite.Count > i - 6)
                {
                    _repeatSprite[index].Position = new Vector2(394 + (i - 6) * 73, 0);
                }
                else
                {
                    MGSprite cardSp2 = MGSprite.MGSpriteWithSpriteFrameName("卡槽2.png");
                    cardSp2.Anchor = new Vector2(0, .5f);
                    AddChild(cardSp2);
                    cardSp2.Position = new Vector2(394 + (i - 6) * 73, 0);
                    _repeatSprite.Add(cardSp2);
                }
            }
        }



    }
}
