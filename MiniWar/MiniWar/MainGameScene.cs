using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Card;
using MiniWar.Toobz;

namespace MiniWar
{
    class MainGameScene : MGScene
    {
        private static MainGameScene _shardMainGame;
        public static MainGameScene ShardMainGame()
        {
            return _shardMainGame;
        }

        public MGColorLayer CoverLayer;
        public MGNode CardShow;
        private MGSprite _bg_day;
        private MGSprite _bg_night;
        private MGSprite _bg_day_sky;
        private MGSprite _bg_night_sky;
        private bool _isNight;
        public void ChangeDayAndNight()
        {
            _isNight = !_isNight;
            if (_isNight)
            {
                _bg_day.Visible = false;
                _bg_night.Visible = true;
                _bg_day_sky.Visible = false;
                _bg_night_sky.Visible = true;
                //new Vector2(1133, 716);
                //new Vector2(1233, 736);
                sunSprite.RunAction(MGMoveTo.ActionWithDuration(.3f, new Vector2(100, 616)));
                moonSprite.RunAction(MGMoveTo.ActionWithDuration(.3f, new Vector2(1233, 736)));
            }
            else
            {
                _bg_day.Visible = true;
                _bg_night.Visible = false;
                _bg_day_sky.Visible = true;
                _bg_night_sky.Visible = false;
                sunSprite.RunAction(MGMoveTo.ActionWithDuration(.3f, new Vector2(1133, 716)));
                moonSprite.RunAction(MGMoveTo.ActionWithDuration(.3f, new Vector2(1133, 536)));
            }
        }


        private MGSprite sunSprite;
        private MGSprite moonSprite;
        public bool InDayOrNightSprite(Point vector2)
        {
            if (sunSprite != null && sunSprite.Visible && sunSprite.InTapInside(vector2))
            {
                ChangeDayAndNight();
                return true;
            }
            if (moonSprite != null && moonSprite.Visible && moonSprite.InTapInside(vector2))
            {
                ChangeDayAndNight();
                return true;
            }
            return false;
        }

        public MainGameScene()
        {
            _shardMainGame = this;
            MainGameLogic.SharedMainGameLogic().IsGameStart = true;

            CardShow = new MGNode();



            _bg_day_sky = MGSprite.MGSpriteWithSpriteFrameName("白天天空.png");
            AddChild(_bg_day_sky);
            _bg_day_sky.Anchor = new Vector2(.5f, 0);
            _bg_day_sky.Position = new Vector2(1366 / 2f, 768f);


            _bg_night_sky = MGSprite.MGSpriteWithSpriteFrameName("晚上天空.png");
            AddChild(_bg_night_sky);
            _bg_night_sky.Anchor = new Vector2(.5f, 0);
            _bg_night_sky.Position = new Vector2(1366 / 2f, 768f);
            _bg_night_sky.Visible = false;


            sunSprite = MGSprite.MGSpriteWithSpriteFrameName("日.png");
            moonSprite = MGSprite.MGSpriteWithSpriteFrameName("月.png");

            AddChild(sunSprite);
            AddChild(moonSprite);
            sunSprite.Position = new Vector2(1133, 716);
            moonSprite.Position = new Vector2(1133, 536);
            //moonSprite.Visible = false;

            _bg_day = MGSprite.MGSpriteWithFilename("Images/bg_day_main");
            AddChild(_bg_day);
            _bg_day.Anchor = new Vector2(0, 1);
            //_bg_day.Visible = false;

            _bg_night = MGSprite.MGSpriteWithFilename("Images/bg_night_main");
            AddChild(_bg_night);
            _bg_night.Anchor = new Vector2(0, 1);
            _bg_night.Visible = false;

            AddChild(WireShowLayer.SharedWireShow());
            WireShowLayer.SharedWireShow().Init();

            AddChild(Card.CardShowLayer.SharedCardShow());
            Card.CardShowLayer.SharedCardShow().Init();

            AddChild(Zombie.ZombieShowLayer.SharedZombieShow());
            Zombie.ZombieShowLayer.SharedZombieShow().Init();


            CoverLayer = new MGColorLayer(new Rectangle(0, 0, Config.ORI_WIN_HEIGHT, Config.ORI_WIN_WIDTH));
            AddChild(CoverLayer);
            CoverLayer.Opacity = 200;
            CoverLayer.SetColor(150, 150, 150);
            CoverLayer.Visible = false;

            var box = new Box();
            AddChild(box);
            box.Position = new Vector2(0, 768 - 55);
            AddChild(CardShow);

            AddChild(new TouchLayer());
        }
    }
}
