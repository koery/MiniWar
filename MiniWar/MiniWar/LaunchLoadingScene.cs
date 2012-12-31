using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;

namespace MiniWar
{
    partial class LaunchLoadingScene
    {
        private const string LoadingSceneBackgroundName = "loading";
        private float _timeLeft = 1;
        public void LoadData()
        {
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/card.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/CardBox.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/ShopBox.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/1001.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/2001.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/3001.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/4001.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/5001.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/6001.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/dj.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Zombie/dj1.plist");

            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/d.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/RocketSkill.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/IceSkill.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/BombSkill.plist");
            //MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/effectbomb.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/cloud.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/brick.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Images/bg_sky.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Images/btn.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/xbing.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Card/xlei.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/shandian.plist");

            //MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/images/lamp.plist");
            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/images/10000.plist");
            //MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/thunderSkill.plist");

            MGSpriteFrameCache.SharedSpriteFrameCache().AddSpriteFramesWithFile("Content/Images/bluebtn.plist");

        }
    }

    partial class LaunchLoadingScene : MGScene
    {
        public LaunchLoadingScene()
        {
            //var background = MGSprite.MGSpriteWithFilename(LoadingSceneBackgroundName);
            //AddChild(background);
            //background.Anchor = new Vector2(0, 1);

            LoadData();
        }
        private bool _isEnd;
        public override void Update(float time)
        {
            if (!_isEnd)
            {
                if ((_timeLeft -= time) < 0)
                {
                    Control.SharedControl().ReplaceScene(Control.SceneType.StartSceneType);
                    _isEnd = true;
                }
            }
            base.Update(time);
        }
    }
}
