using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Zombie;
using ThreeDefense.Effect;

namespace MiniWar
{
    class StartSceen : MGScene
    {
        private MGSprite _bg;
        private static StartSceen _sharedStartSceen;
        public static StartSceen SharedStartSceen()
        {
            return _sharedStartSceen;

        }

        public Actor actor;

        public StartSceen()
        {
            _sharedStartSceen = this;
            _bg = MGSprite.MGSpriteWithFilename("images/name_bg");
            AddChild(_bg);
            _bg.Anchor = new Vector2(0, 1);

            var logo = MGSprite.MGSpriteWithFilename("logo");
            AddChild(logo);
            logo.Position = new Vector2(676, 609);

            CreateAcotr();

            var nsprite = MGSprite.MGSpriteWithFilename("images/start2--x492-y503-w236-h199");
            var ssprite = MGSprite.MGSpriteWithFilename("images/start1--x492-y503-w236-h199");
            var menuItem = MGMenuItemSprite.itemFromNormalSprite(nsprite, ssprite, null, (sender) => { AudioMgr.PlayAudio(1); Control.SharedControl().ReplaceScene(Control.SceneType.MainMenuSceneType); });
            var menu = MGMenu.menuWithItems(menuItem);
            AddChild(menu, 10);
            menu.Position = new Vector2(492 + 236 / 2f, 768 - 504 - 199 / 2f);
        }

        private float timeLeft = 0;
        public override void Update(float time)
        {
            if ((timeLeft += time) > 1)
            {
                if (actor != null)
                {
                    if (!actor._islive)
                    {
                        CreateAcotr();
                    }
                }
            }

            base.Update(time);
        }

        public void CreateAcotr()
        {
            if (actor != null)
            {
                RemoveChild(actor);
            }
            switch (ActorLv.GetRandomActorType(3).Name)
            {
                case "ActorBabe":
                    actor = new ActorBabe();
                    break;
                case "ActorNormal":
                    actor = new ActorNormal();
                    break;
                case "ActorLr":
                    actor = new ActorLr();
                    break;
                case "ActorFast":
                    actor = new ActorFast();
                    break;
            }
            AddChild(actor, 6);
            actor.Position = new Vector2(1388, Control.SharedControl().GetRandom(230, 286));
            actor.Run(new Vector2(Control.SharedControl().GetRandom(324, 855), Control.SharedControl().GetRandom(230, 286)));
        }
    }
}
