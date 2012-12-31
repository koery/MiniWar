using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;

namespace MiniWar
{

    class HelpLayer : MGLayer
    {
        private static HelpLayer _sharedHelp;
        public static HelpLayer SharedHelp()
        {
            if (_sharedHelp == null)
            {
                _sharedHelp = new HelpLayer();
            }
            return _sharedHelp;
        }

        public MGNode Help1;
        public MGNode Help2;
        public MGNode Help3;
        public MGSprite understand;
        public HelpLayer()
        {
            Help1 = new MGNode();
            AddChild(Help1);
            Help2 = new MGNode();
            AddChild(Help2);
            Help3 = new MGSprite();
            AddChild(Help3);

            var help1Sprite = MGSprite.MGSpriteWithFilename("images/help1");
            Help1.AddChild(help1Sprite);
            help1Sprite.Position = new Vector2(77 + 851 / 2f, 657 - 601 / 2f);
            CreatLamp(Help1, new Vector2(381, 555), new Vector2(238, 555), new Vector2(239, 444), -90);
            CreatLamp(Help1, new Vector2(820, 183), new Vector2(643, 183), new Vector2(640, 312), 90);
            CreatLamp(Help1, new Vector2(442, 212), new Vector2(221, 212));
            CreatLamp(Help1, new Vector2(805, 532), new Vector2(579, 532));
            Help1.Visible = false;


            var help2Sprite = MGSprite.MGSpriteWithFilename("images/help2");
            Help2.AddChild(help2Sprite);
            help2Sprite.Position = new Vector2(77 + 851 / 2f, 657 - 601 / 2f);
            CreatLamp(Help2, new Vector2(328, 411), new Vector2(203, 411), new Vector2(203, 577), 90);
            CreatLamp(Help2, new Vector2(772, 320), new Vector2(674, 320), new Vector2(674, 181), -90);
            CreatLamp(Help2, new Vector2(451, 181), new Vector2(235, 181));
            CreatLamp(Help2, new Vector2(775, 543), new Vector2(557, 543));
            Help2.Visible = false;


            var help3Sprite = MGSprite.MGSpriteWithFilename("images/help3");
            Help3.AddChild(help3Sprite);
            help3Sprite.Position = new Vector2(77 + 851 / 2f, 657 - 601 / 2f);
            CreatLamp(Help3, new Vector2(410, 516), new Vector2(224, 516));
            CreatLamp(Help3, new Vector2(410, 371), new Vector2(230, 371));
            CreatLamp(Help3, new Vector2(424, 209), new Vector2(205, 209));
            CreatLamp(Help3, new Vector2(786, 556), new Vector2(611, 556));
            CreatLamp(Help3, new Vector2(786, 435), new Vector2(611, 435));
            CreatLamp(Help3, new Vector2(810, 315), new Vector2(637, 315));
            CreatLamp(Help3, new Vector2(793, 191), new Vector2(624, 191));
            Help3.Visible = false;

            understand = MGSprite.MGSpriteWithFilename("images/understand");
            AddChild(understand);
            understand.Position = new Vector2(517 + 411 / 2f, 86 - 75 / 2f);
            understand.Visible = false;
        }

        private int _index = 0;
        public void ChangeHelpShow()
        {
            int i = _index % 3;
            if (i == 0)
            {
                Help1.Visible = true;
                Help2.Visible = false;
                Help3.Visible = false;
                understand.Visible = false;
            }
            else if (i == 1)
            {
                Help1.Visible = false;
                Help2.Visible = false;
                Help3.Visible = true;
                understand.Visible = false;
            }
            else if (i == 2)
            {
                Help1.Visible = false;
                Help2.Visible = true;
                Help3.Visible = false;
                understand.Visible = true;
            }
            _index++;
        }

        public void CreatLamp(MGNode basenode, Vector2 point, Vector2 vector1, Vector2 vector2, float angle)
        {
            var lamp1 = new LampLayer();
            basenode.AddChild(lamp1);
            lamp1.Position = point;
            var move1 = MGMoveTo.ActionWithDuration(2.4f, vector1);
            var move2 = MGMoveTo.ActionWithDuration(2.4f, vector2);
            lamp1.RunAction(MGRepeatForever.Actions(MGSequence.Actions(move1, MGRotateTo.ActionWithDuration(.2f, angle), move2, MGCallFunc.ActionWithTarget(() =>
            {
                lamp1.Rotation = 0;
                lamp1.Position = point;
            }))));
        }

        public void CreatLamp(MGNode basenode, Vector2 point, Vector2 vector1)
        {
            var lamp1 = new LampLayer();
            basenode.AddChild(lamp1);
            lamp1.Position = point;
            var move1 = MGMoveTo.ActionWithDuration(4.4f, vector1);
            lamp1.RunAction(MGRepeatForever.Actions(MGSequence.Actions(move1, MGCallFunc.ActionWithTarget(() =>
            {
                lamp1.Rotation = 0;
                lamp1.Position = point;
            }))));
        }

        class LampLayer : MGLayer
        {
            public LampLayer()
            {
                var lamp = MGSprite.MGSpriteWithSpriteFrameName("lamp.png");
                List<MGSpriteFrame> frames = new List<MGSpriteFrame>();
                frames.Add(MGSpriteFrameCache.SharedSpriteFrameCache().SpriteFrameByName("lamp.png"));
                frames.Add(MGSpriteFrameCache.SharedSpriteFrameCache().SpriteFrameByName("lamp1.png"));
                var animate = MGAnimate.ActionWithAnimation(MGAnimation.animationWithFrames(frames));
                lamp.RunAction(MGRepeatForever.Actions(animate));
                AddChild(lamp);
            }
        }
    }
}
