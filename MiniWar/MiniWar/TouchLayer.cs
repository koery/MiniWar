using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Card;
using MiniWar.Toobz;
using MiniWar.Zombie;
using VSZombie.Toobz;

namespace MiniWar
{
    class TouchLayer : MGLayer
    {
        public static CallbackN ShopCallBack1;
        public static CallbackN ShopCallBack2;
        public static TouchLayer Touch;
        public bool Istouch = true;




        public TouchLayer()
        {

            Touch = this;
            IsTouchEnable = true;




            MGSprite nShop = MGSprite.MGSpriteWithSpriteFrameName("商店.png");
            MGSprite _nShop = MGSprite.MGSpriteWithSpriteFrameName("商店.png");
            _nShop.Scale = new Vector2(.95f, .95f);
            item = MGMenuItemSprite.itemFromNormalSprite(nShop, _nShop, null, (sender) =>
            {
                var menuitem = sender as MGMenuItemSprite;
                if (menuitem.Tag == 1)
                {
                    MGSprite sShop = MGSprite.MGSpriteWithSpriteFrameName("商店2.png");
                    MGSprite _sShop = MGSprite.MGSpriteWithSpriteFrameName("商店2.png");
                    _sShop.Scale = new Vector2(.95f, .95f);
                    menuitem.NormalImage = sShop;
                    menuitem.SelectedImage = _sShop;
                    if (ShopCallBack1 != null)
                    {
                        ShopCallBack1.Invoke(menu);
                        Istouch = false;
                        MainGameScene.ShardMainGame().CoverLayer.Visible = true;
                        //BugBtn.Visible = true;
                        MainGameLogic.SharedMainGameLogic().GamePause();
                    }
                    menuitem.Tag = 2;
                }
                else
                {
                    MGSprite shop = MGSprite.MGSpriteWithSpriteFrameName("商店.png");
                    MGSprite _shop = MGSprite.MGSpriteWithSpriteFrameName("商店.png");
                    _shop.Scale = new Vector2(.95f, .95f);
                    menuitem.NormalImage = shop;
                    menuitem.SelectedImage = _shop;
                    if (ShopCallBack2 != null)
                    {
                        Istouch = true;
                        ShopCallBack2.Invoke(menu);
                        MainGameScene.ShardMainGame().CoverLayer.Visible = false;
                        _buyBtn.Visible = false;
                        MainGameLogic.SharedMainGameLogic().GameResume();
                    }
                    menuitem.Tag = 1;
                }
            });
            item.Tag = 1;
            menu = MGMenu.menuWithItems(item);
            AddChild(menu);
            menu.Position = new Vector2(40, 500);




            nsp = MGSprite.MGSpriteWithSpriteFrameName("购买1.png");
            _nsp = MGSprite.MGSpriteWithSpriteFrameName("购买1.png");
            _nsp.Scale = new Vector2(.95f, .95f);
            ssp = MGSprite.MGSpriteWithSpriteFrameName("购买2.png");
            _ssp = MGSprite.MGSpriteWithSpriteFrameName("购买2.png");
            _ssp.Scale = new Vector2(.95f, .95f);
            _buymenu = MGMenuItemSprite.itemFromNormalSprite(nsp, _nsp, null, (sender) =>
            {
                if (_paperInfo != null)
                {
                    _paperInfo.Sprite.SetColor(255, 255, 255);
                    if (CardShowLayer.SharedCardShow().Boxs.Count < GameConfig.CardCount)
                    {
                        if (GameConfig.Money >= _paperInfo.Price)
                        {
                            if (GameConfig.ChangeMoney(-_paperInfo.Price))
                            {
                                var card = new CardBox(_paperInfo.Id);
                                card.SetPoint(new Vector2(132 + 7 * 76, 713));
                                CardShowLayer.SharedCardShow().AddChild(card);
                            }
                        }
                    }
                    _paperInfo = null;
                    _buymenu.NormalImage = nsp;
                    _buymenu.NormalImage = _nsp;
                }
            });
            _buyBtn = MGMenu.menuWithItems(_buymenu);
            AddChild(_buyBtn);
            _buyBtn.Position = new Vector2(240, 90);
            _buyBtn.Visible = false;
        }

        private MGMenuItemSprite item;
        private Rectangle _money = new Rectangle(5, 768 - 73, 73, 73);

        private MGSprite nsp;
        private MGSprite _nsp;
        private MGSprite ssp;
        private MGSprite _ssp;
        private MGMenu _buyBtn;
        private MGMenuItemSprite _buymenu;


        private MGSprite _oldPointSporte;
        private MGMenu menu;

        private Card.CardBox _box;
        private PaperInfo _paperInfo;
        private Rectangle _grassRange = new Rectangle((int)GameConfig.RelativeOrigin.X, (int)GameConfig.RelativeOrigin.Y, GameConfig.WidthX * GameConfig.GirdSize, GameConfig.HightY * GameConfig.GirdSize);
        public override bool TouchesBegan(Microsoft.Xna.Framework.Input.MouseState touch, Point point)
        {
            if (_grassRange.Contains(point))
            {
                for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                {
                    var sprite = ZombieShowLayer.SharedZombieShow().Actors[i].Sprite;
                    if (sprite.InTapInside(point))
                    {
                        sprite.Opacity = 100;
                        sprite.RunAction(MGFadeTo.ActionWithDuration(1.3f, 255));
                    }
                }
            }

            if (_money.Contains(point))
            {
                GameConfig.ChangeMoney(0);
                if (item.Tag == 1)
                {
                    MGSprite sShop = MGSprite.MGSpriteWithSpriteFrameName("商店2.png");
                    MGSprite _sShop = MGSprite.MGSpriteWithSpriteFrameName("商店2.png");
                    _sShop.Scale = new Vector2(.95f, .95f);
                    item.NormalImage = sShop;
                    item.SelectedImage = _sShop;
                    if (ShopCallBack1 != null)
                    {
                        ShopCallBack1.Invoke(menu);
                        Istouch = false;
                        MainGameScene.ShardMainGame().CoverLayer.Visible = true;
                        //BugBtn.Visible = true;
                        MainGameLogic.SharedMainGameLogic().GamePause();
                    }
                    item.Tag = 2;
                }
                else
                {
                    MGSprite shop = MGSprite.MGSpriteWithSpriteFrameName("商店.png");
                    MGSprite _shop = MGSprite.MGSpriteWithSpriteFrameName("商店.png");
                    _shop.Scale = new Vector2(.95f, .95f);
                    item.NormalImage = shop;
                    item.SelectedImage = _shop;
                    if (ShopCallBack2 != null)
                    {
                        Istouch = true;
                        ShopCallBack2.Invoke(menu);
                        MainGameScene.ShardMainGame().CoverLayer.Visible = false;
                        _buyBtn.Visible = false;
                        MainGameLogic.SharedMainGameLogic().GameResume();
                    }
                    item.Tag = 1;
                }



            }
            if (!Istouch)
            {
                var b = _buyBtn.TouchesBegan(touch, point);

                if (!b)
                {
                    if (_oldPointSporte != null && _oldPointSporte.InTapInside(point))
                    {
                        _buymenu.Activate();
                        _oldPointSporte = null;
                    }
                    else
                    {
                        _paperInfo = null;
                        for (int i = 0; i < MainGameLogic.SharedMainGameLogic().PaperInfos.Count; i++)
                        {
                            var sprite = MainGameLogic.SharedMainGameLogic().PaperInfos[i].Sprite;
                            sprite.SetColor(255, 255, 255);
                            if (sprite.InTapInside(point))
                            {
                                sprite.SetColor(new Color(200, 200, 0));
                                _oldPointSporte = sprite;
                                sprite.Tag = 1;
                                _buyBtn.Visible = true;
                                _paperInfo = MainGameLogic.SharedMainGameLogic().PaperInfos[i];
                                if (GameConfig.Money >= _paperInfo.Price)
                                {
                                    _buymenu.NormalImage = ssp;
                                    _buymenu.SelectedImage = _ssp;
                                }
                                else
                                {
                                    _buymenu.NormalImage = nsp;
                                    _buymenu.SelectedImage = _nsp;
                                }
                            }
                            else
                            {
                                sprite.Tag = 0;
                            }
                        }
                    }
                }
                return base.TouchesBegan(touch, point);
            }

            for (int i = 0; i < Card.CardShowLayer.SharedCardShow().Boxs.Count; i++)
            {
                var box = Card.CardShowLayer.SharedCardShow().Boxs[i];
                if (box.ICOSprite.InTapInside(point))
                {
                    box.MouseClick();
                    _box = box;
                    return false;
                }
            }

            for (int i = 0; i < WireShowLayer.SharedWireShow().RevolutionAnchors.Count; i++)
            {
                RevolutionAnchor revolution = WireShowLayer.SharedWireShow().RevolutionAnchors[i];
                if (revolution.Sprite.InTapInside(point))
                {
                    revolution.ClockwiseRotate();
                    return false;
                }
            }
            for (int i = 0; i < WireShowLayer.SharedWireShow().Wires.Count; i++)
            {
                var wire = WireShowLayer.SharedWireShow().Wires[i];
                if (wire.Sprite.InTapInside(point))
                {
                    wire.ChangeWireDirection();
                }
            }
            return base.TouchesBegan(touch, point);
        }

        public override bool TouchesMoved(Microsoft.Xna.Framework.Input.MouseState touch, Point point)
        {
            if (!Istouch)
                return base.TouchesMoved(touch, point);


            if (_box != null)
            {
                _box.MouseMove();
                _box.Sprite.Position = new Vector2(point.X,point.Y);
            }
            return base.TouchesMoved(touch, point);
        }

        public override bool TouchesEnded(Microsoft.Xna.Framework.Input.MouseState touch, Point point)
        {
            if (!Istouch)
                return base.TouchesEnded(touch, point);
            if (MainGameScene.ShardMainGame().InDayOrNightSprite(point))
            {
                return base.TouchesEnded(touch, point);
            }
            if (_box != null)
            {
                var vector = IntVector.ToGridIntVector(new Vector2(point.X,point.Y));
                if (vector.X < 0 || vector.X > GameConfig.WidthX - 1 || vector.Y < 0 || vector.Y > GameConfig.HightY - 1)
                {
                    _box.MouseCancel();
                }
                else
                {
                    _box.MouseEnd();
                    Card.CardShowLayer.SharedCardShow().RemoveChild(_box);
                    for (int i = 0; i < Card.CardShowLayer.SharedCardShow().Boxs.Count; i++)
                    {
                        CardBox cardBox = Card.CardShowLayer.SharedCardShow().Boxs[i];
                        cardBox.SetPoint(i);
                    }
                    //todo: xx
                    Paper paper = new Paper();
                    switch (_box.Id)
                    {
                        case 1:
                            paper = new RandomWirePaper();
                            break;
                        case 2:
                            paper = new IcePaper();
                            break;
                        case 3:
                            paper = new RocketPaper();
                            break;
                        case 4:
                            paper = new XWirePaper();
                            break;
                        case 5:
                            paper = new BombPaper();
                            break;
                        case 6:
                            paper = new BigIcePaper();
                            break;
                        case 7:
                            paper = new BigBombPaper();
                            break;
                        case 8:
                            paper = new HorizontalPaper();
                            break;
                    }
                    paper.SetVector(vector.X, vector.Y);
                    CardShowLayer.SharedCardShow().AddChild(paper);

                }

            }
            _box = null;
            return base.TouchesEnded(touch, point);
        }
    }
}
