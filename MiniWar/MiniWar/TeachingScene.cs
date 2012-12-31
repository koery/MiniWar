using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Toobz;
using MiniWar.Zombie;
using VSZombie.Toobz;

namespace MiniWar
{
    class TeachingScene : MGScene
    {

        public bool IsTeaching = false;
        private static TeachingScene _shardMainGame;
        public static TeachingScene ShardMainGame()
        {
            return _shardMainGame;
        }
        public MGNode CardShow;
        private MGSprite _bg_day;
        private MGSprite _bg_day_sky;

        public MGColorLayer CoverLayer;
        public TeachingScene()
        {
            _shardMainGame = this;
            IsTeaching = true;

            CardShow = new MGNode();


            _bg_day_sky = MGSprite.MGSpriteWithSpriteFrameName("白天天空.png");
            AddChild(_bg_day_sky);
            _bg_day_sky.Anchor = new Vector2(.5f, 0);
            _bg_day_sky.Position = new Vector2(1366 / 2f, 768f);


            _bg_day = MGSprite.MGSpriteWithFilename("Images/bg_day_main");
            AddChild(_bg_day);
            _bg_day.Anchor = new Vector2(0, 1);




            List<Wire> wires = new List<Wire>();
            wires.Add(new Wire(new IntVector(0, -1), WireType.WireT, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(0, -1), WireType.WireI, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(0, -1), WireType.WireL, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(0, -1), WireType.WireL, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(0, -1), WireType.WireT, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(0, -1), WireType.WireL, WireDirection.WireDown));

            wires.Add(new Wire(new IntVector(1, -1), WireType.WireI, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(1, -1), WireType.WireL, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(1, -1), WireType.WireX, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(1, -1), WireType.WireT, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(1, -1), WireType.WireT, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(1, -1), WireType.WireT, WireDirection.WireLeft));


            wires.Add(new Wire(new IntVector(2, -1), WireType.WireI, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(2, -1), WireType.WireT, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(2, -1), WireType.WireL, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(2, -1), WireType.WireL, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(2, -1), WireType.WireI, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(2, -1), WireType.WireT, WireDirection.WireDown));


            wires.Add(new Wire(new IntVector(3, -1), WireType.WireI, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(3, -1), WireType.WireT, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(3, -1), WireType.Wirei, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(3, -1), WireType.Wirei, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(3, -1), WireType.WireX, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(3, -1), WireType.WireT, WireDirection.WireDown));


            wires.Add(new Wire(new IntVector(4, -1), WireType.WireL, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(4, -1), WireType.WireL, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(4, -1), WireType.Wirei, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(4, -1), WireType.WireT, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(4, -1), WireType.WireL, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(4, -1), WireType.WireI, WireDirection.WireLeft));


            wires.Add(new Wire(new IntVector(5, -1), WireType.WireX, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(5, -1), WireType.WireI, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(5, -1), WireType.WireI, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(5, -1), WireType.WireL, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(5, -1), WireType.WireI, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(5, -1), WireType.WireT, WireDirection.WireRight));



            wires.Add(new Wire(new IntVector(6, -1), WireType.WireI, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(6, -1), WireType.WireT, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(6, -1), WireType.Wirei, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(6, -1), WireType.WireT, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(6, -1), WireType.WireL, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(6, -1), WireType.WireT, WireDirection.WireLeft));




            wires.Add(new Wire(new IntVector(7, -1), WireType.WireT, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(7, -1), WireType.WireI, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(7, -1), WireType.WireI, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(7, -1), WireType.WireL, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(7, -1), WireType.Wirei, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(7, -1), WireType.WireL, WireDirection.WireUp));




            wires.Add(new Wire(new IntVector(8, -1), WireType.WireL, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(8, -1), WireType.WireL, WireDirection.WireLeft));
            wires.Add(new Wire(new IntVector(8, -1), WireType.WireL, WireDirection.WireUp));
            wires.Add(new Wire(new IntVector(8, -1), WireType.WireL, WireDirection.WireRight));
            wires.Add(new Wire(new IntVector(8, -1), WireType.WireI, WireDirection.WireDown));
            wires.Add(new Wire(new IntVector(8, -1), WireType.Wirei, WireDirection.WireDown));

            AddChild(WireShowLayer.SharedWireShow());
            WireShowLayer.SharedWireShow().Init(wires);


            //AddChild(Card.CardShowLayer.SharedCardShow());
            //Card.CardShowLayer.SharedCardShow().Init();


            AddChild(Zombie.ZombieShowLayer.SharedZombieShow());
            Zombie.ZombieShowLayer.SharedZombieShow().Init();


            CoverLayer = new MGColorLayer(new Rectangle(0, 0, Config.ORI_WIN_HEIGHT, Config.ORI_WIN_WIDTH));
            CoverLayer.Opacity = 200;
            CoverLayer.SetColor(150, 150, 150);
            CoverLayer.Visible = false;
            AddChild(CoverLayer);


            touchLayer = new TeachingLayer();
            AddChild(touchLayer);
            touchLayer.IsTouchEnable = false;




            _tapTip = MGSprite.MGSpriteWithFilename("images/tapTip");
            AddChild(_tapTip, 9);
            _tapTip.Visible = false;





            AddChild(HelpLayer.SharedHelp());
        }


        

        private TeachingLayer touchLayer;
        public MGSprite _tapTip;
        private float _timeLeft1 = 1;
        private bool _fist = false;
        private bool _second;
        private bool _third;
        private bool _actorIsComeing = false;
        public bool _actorIsMoveing = false;
        public bool ShowHelp = false;
        List<Actor> actors = new List<Actor>();
        public override void Update(float time)
        {
            if (!_fist)
            {
                if ((_timeLeft1 -= time) < 0)
                {
                    _fist = true;

                    MGSprite sprite = MGSprite.MGSpriteWithFilename("images/go");
                    AddChild(sprite);
                    sprite.Anchor = new Vector2(.5f, 1);
                    sprite.Position = new Vector2(491, 0);
                    MGSprite txt = MGSprite.MGSpriteWithFilename("images/tiptxt2");
                    sprite.AddChild(txt);
                    txt.Position = new Vector2(0, 264);

                    MGSprite go = MGSprite.MGSpriteWithFilename("images/txtgo");
                    sprite.AddChild(go);
                    go.Position = new Vector2(0, 131);

                    sprite.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(.4f, new Vector2(491, 209)), MGDelay.ActionWithDuration(4.1f), MGMoveTo.ActionWithDuration(.2f, new Vector2(491, 1000)), MGCallFunc.ActionWithTarget(() =>
                    {
                        RemoveChild(sprite);


                        var tip = MGSprite.MGSpriteWithFilename("Images/tipZombie");
                        AddChild(tip);
                        tip.Position = new Vector2(GameConfig.WidthX + .3f, 3.5f) * 94 + GameConfig.RelativeOrigin;
                        var scale = MGSequence.Actions(MGScaleTo.ActionWithDuration(.2f, 1.1f), MGDelay.ActionWithDuration(.2f), MGScaleTo.ActionWithDuration(.2f, .95f), MGDelay.ActionWithDuration(.2f));
                        tip.RunAction(MGSequence.Actions(MGRepeat.Actions(scale, 8), new MGHide(), MGCallFunc.ActionWithTarget(() => { RemoveChild(tip); })));

                    })));

                    Zombie.Actor actor = new ActorNormal(3);
                    actor.Vector = new IntVector();
                    ZombieShowLayer.SharedZombieShow().AddChild(actor);
                    actor.Run();
                    actors.Add(actor);
                }
            }
            if (!_actorIsComeing)
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    if (actors[i].Position.X <= 1024)
                    {
                        ActorStopTipChangeWire(new Vector2(794, 445));
                        _actorIsComeing = true;
                    }
                }
            }

            if (_actorIsComeing && !_actorIsMoveing)
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    if (actors[i].Vector == new IntVector(5, 4))
                    {
                        _actorIsMoveing = true;
                        actors[i].Stop();
                        HelpLayer.SharedHelp().ChangeHelpShow();
                        touchLayer.IsTouchEnable = true;
                    }
                }
            }


            if (_fist && !_second)
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    if (actors[i].AtVector == new IntVector(5, 4))
                    {
                        actors[i].Stop();
                        ActorStopTipChangeWire(new Vector2(415, 533));
                        _second = true;
                        touchLayer.IsTouchEnable = true;
                    }
                }
            }


            if (!_third && _second)
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    if (actors[i].AtVector == new IntVector(2, 5))
                    {
                        actors[i].Stop();
                        ActorStopTipChangeWire(new Vector2(36, 633));
                        _third = true;
                        touchLayer.IsTouchEnable = true;
                    }
                }
            }


            base.Update(time);
        }


        public void ActorStopTipChangeWire(Vector2 vector2)
        {
            foreach (var actor in actors)
            {
                actor.Stop();
            }
            touchLayer.IsTouchEnable = true;

            _tapTip.Position = vector2;
            _tapTip.Visible = true;
        }
    }


    class TeachingLayer : MGLayer
    {
        public TeachingLayer()
        {
            IsTouchEnable = true;

        }

        private bool _gameStart = false;
        private bool _isfirst;
        private bool _issecond;
        private bool _isthird;
        private bool _isfourth = true;
        Rectangle _fistRect = new Rectangle(858, 358, 56, 58);
        Rectangle _secondRect = new Rectangle(480, 450, 56, 58);
        Rectangle _thirdRect = new Rectangle(96, 545, 56, 58);
        Rectangle _fourthRect = new Rectangle(624, 416, 45, 35);

        Rectangle _helpRect = new Rectangle(110, 86, 785, 535);
        Rectangle _understandRect = new Rectangle(517, 11, 411, 75);
        private bool _isShowHelpTouch;
        public override bool TouchesBegan(Microsoft.Xna.Framework.Input.MouseState touch, Point point)
        {
            if (_gameStart)
            {
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
            }
            else
            {
                if (!_isfirst)
                {
                    if (_fistRect.Contains(point))
                    {
                        var wire = WireShowLayer.SharedWireShow().WireDictionary[new IntVector(8, 3)];
                        wire.ChangeWireDirection();
                        if (wire.WireDir == WireDirection.WireRight)
                        {
                            TeachingScene.ShardMainGame()._tapTip.Visible = false;
                            for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                            {
                                ZombieShowLayer.SharedZombieShow().Actors[i].GoRun();
                            }
                            IsTouchEnable = false;
                            _isfirst = true;
                        }
                    }
                }

                if (!_issecond)
                {
                    if (_secondRect.Contains(point))
                    {
                        var wire = WireShowLayer.SharedWireShow().WireDictionary[new IntVector(4, 4)];
                        wire.ChangeWireDirection();
                        if (wire.WireDir == WireDirection.WireRight)
                        {
                            TeachingScene.ShardMainGame()._tapTip.Visible = false;
                            for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                            {
                                ZombieShowLayer.SharedZombieShow().Actors[i].GoRun();
                            }
                            IsTouchEnable = false;
                            _issecond = true;
                        }
                        else
                        {
                            TeachingScene.ShardMainGame()._tapTip.Position = new Vector2(415, 533);
                            TeachingScene.ShardMainGame()._tapTip.RunAction(MGJumpBy.ActionWithDuration(.8f, new Vector2(0, 10), 10, 3));
                        }
                    }
                }


                if (!_isthird)
                {
                    if (_thirdRect.Contains(point))
                    {
                        var wire = WireShowLayer.SharedWireShow().WireDictionary[new IntVector(0, 5)];
                        wire.ChangeWireDirection();
                        if (wire.WireDir == WireDirection.WireUp)
                        {
                            TeachingScene.ShardMainGame()._tapTip.Visible = false;

                            TeachingScene.ShardMainGame().ActorStopTipChangeWire(new Vector2(560, 491));
                            _isfourth = false;
                            _isthird = true;
                        }
                        else
                        {
                            TeachingScene.ShardMainGame()._tapTip.Position = new Vector2(415, 533);
                            TeachingScene.ShardMainGame()._tapTip.RunAction(MGJumpBy.ActionWithDuration(.8f, new Vector2(0, 10), 10, 3));
                        }
                    }
                }

                if (!_isfourth)
                {
                    if (_fourthRect.Contains(point))
                    {
                        for (int i = 0; i < WireShowLayer.SharedWireShow().RevolutionAnchors.Count; i++)
                        {
                            RevolutionAnchor revolution = WireShowLayer.SharedWireShow().RevolutionAnchors[i];
                            if (revolution.Sprite.InTapInside(point))
                            {
                                revolution.ClockwiseRotate();
                                TeachingScene.ShardMainGame()._tapTip.Visible = false;
                                Actor actor = new ActorNormal();
                                ZombieShowLayer.SharedZombieShow().AddChild(actor);
                                actor.Run();
                                MGSprite sprite = MGSprite.MGSpriteWithFilename("images/go");
                                AddChild(sprite);
                                sprite.Anchor = new Vector2(.5f, 1);
                                sprite.Position = new Vector2(491, 0);


                                MGSprite txt = MGSprite.MGSpriteWithFilename("images/tiptxt1");
                                sprite.AddChild(txt);
                                txt.Position = new Vector2(0, 239);

                                MGSprite go = MGSprite.MGSpriteWithFilename("images/txtgo");
                                sprite.AddChild(go);
                                go.Position = new Vector2(0, 71);

                                sprite.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(.4f, new Vector2(491, 209)), MGDelay.ActionWithDuration(2.1f), MGMoveTo.ActionWithDuration(.2f, new Vector2(491, 1000)), MGCallFunc.ActionWithTarget(() => RemoveChild(sprite))));
                                _gameStart = true;
                                _isfourth = true;
                                return false;
                            }
                        }
                    }
                }

                if (TeachingScene.ShardMainGame()._actorIsMoveing && !TeachingScene.ShardMainGame().ShowHelp)
                {
                    if (_helpRect.Contains(point))
                    {
                        _isShowHelpTouch = true;
                    }
                }
            }

            return base.TouchesBegan(touch, point);
        }

        public override bool TouchesMoved(Microsoft.Xna.Framework.Input.MouseState touch, Point point)
        {
            return base.TouchesMoved(touch, point);
        }

        public override bool TouchesEnded(Microsoft.Xna.Framework.Input.MouseState touch, Point point)
        {
            if (TeachingScene.ShardMainGame()._actorIsMoveing && !TeachingScene.ShardMainGame().ShowHelp)
            {
                if (_helpRect.Contains(point))
                {
                    if (_isShowHelpTouch)
                    {
                        HelpLayer.SharedHelp().ChangeHelpShow();
                    }
                }
            }
            if (HelpLayer.SharedHelp().understand.Visible)
            {
                if (_understandRect.Contains(point))
                {
                    TeachingScene.ShardMainGame().ShowHelp = true;
                    HelpLayer.SharedHelp().Help1.Visible = false;
                    HelpLayer.SharedHelp().Help2.Visible = false;
                    HelpLayer.SharedHelp().Help3.Visible = false;
                    HelpLayer.SharedHelp().understand.Visible = false;
                    IsTouchEnable = false;
                    for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                    {
                        ZombieShowLayer.SharedZombieShow().Actors[i].GoRun();
                    }
                }
            }
            return base.TouchesEnded(touch, point);
        }
    }

}
