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
    class MainMenuScene : MGScene
    {
        private F1 f1;

        public MainMenuScene()
        {
            var bg = MGSprite.MGSpriteWithFilename("images/menu_bg");
            AddChild(bg);
            bg.Anchor = new Vector2(0, 1);

            MGSprite nbackSprite = MGSprite.MGSpriteWithFilename("images/back2");
            MGSprite sbackSprite = MGSprite.MGSpriteWithFilename("images/back1");
            var backMenuitem = MGMenuItemSprite.itemFromNormalSprite(nbackSprite, sbackSprite, null, (sender) => { Control.SharedControl().ReplaceScene(Control.SceneType.StartSceneType); AudioMgr.PlayAudio(1); });
            MGMenu backbtn = MGMenu.menuWithItems(backMenuitem);
            AddChild(backbtn);
            backbtn.Position = new Vector2(218 + 355 / 2f, 349 + 63 - 316 / 2f);


            f1 = new F1();
            AddChild(f1);

            f1.IsTouchEnable = true;


            var grassland = MGSprite.MGSpriteWithFilename("images/grassland-x465-y635-w901-h133");
            AddChild(grassland); grassland.Position = new Vector2(465 + 901 / 2f, 133 / 2f);
        }

        private int _index = 0;




        class F1 : MGLayer
        {
            private List<Vector2> _points = new List<Vector2>() { new Vector2(798, 377), new Vector2(979, 377), new Vector2(1164, 377), new Vector2(857, 239), new Vector2(1049, 239) };
            private MGSprite f1;
            public F1()
            {
                f1 = MGSprite.MGSpriteWithFilename("images/f1-x485-y0-w881-h768");
                AddChild(f1); f1.Position = new Vector2(485 + 881 / 2f, 768 / 2);
                List<MGMenuItemSprite> itemlist = new List<MGMenuItemSprite>();
                for (int i = 0; i < 5; i++)
                {
                    var nsprite = MGSprite.MGSpriteWithSpriteFrameName("闭窗.png");
                    var ssprite = MGSprite.MGSpriteWithSpriteFrameName("闭窗.png");
                    ssprite.Scale = new Vector2(.95f);
                    var menuitem = MGMenuItemSprite.itemFromNormalSprite(nsprite, ssprite, null, (sender) =>
                    {
                        ActorLv.Lv = 1;
                        var item = sender as MGMenuItemSprite;
                        if (item != null) ActorLv.Index = item.Tag;
                        {
                            AudioMgr.PlayAudio(1);
                            Control.SharedControl().ReplaceScene(Control.SceneType.MainGameSceneType);
                        }
                    });
                    menuitem.Tag = i;
                    itemlist.Add(menuitem);
                    menuitem.Position = _points[i];
                }
                var menu1 = MGMenu.menuWithItems(itemlist.ToArray());
                AddChild(menu1);
            }
        }


    }


}
