using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Zombie;

namespace MiniWar
{
    class GameOverScene : MGScene
    {
        private MGSprite _sprite;
        public GameOverScene()
        {
            _sprite = MGSprite.MGSpriteWithFilename("images/gameover");
            AddChild(_sprite);
            _sprite.Position = new Vector2(1366 / 2f, 768 / 2f);


            var _btnagain = MGSprite.MGSpriteWithFilename("images/close");
            var s_btnagain = MGSprite.MGSpriteWithFilename("images/close");
            s_btnagain.Scale = new Vector2(.95f);
            var againItem = MGMenuItemSprite.itemFromNormalSprite(_btnagain, s_btnagain, null, (sender) =>
            {
                var index = ActorLv.Index;
                var lv = ActorLv.Lv;
                //if (lv == 1)
                //{
                //    if (ActorLv.Index == 0)
                //    {
                //        Control.SharedControl().ReplaceScene(Control.SceneType.TeachingSceneType);
                //    }
                //}
                //else
                {
                    MGDirector.SharedDirector().Game.Exit();
                }
            });

            var menu = MGMenu.menuWithItems(againItem);
            AddChild(menu);
            againItem.Position = new Vector2(939, 202);
        }
    }
}
