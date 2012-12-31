using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Card;
using MiniWar.Toobz;
using VSZombie.Toobz;

namespace MiniWar.Zombie
{
    class Actor : MGNode
    {
        public bool _islive = false;
        protected int Speed = 1;
        public int Hp { get; set; }
        private MGSprite _electricSprite;
        public IntVector Vector { get; set; }
        public IntVector AtVector { get; set; }
        public MGSprite Sprite { get; set; }
        public WireDirection ComeDirection;
        public WireDirection GoDirection;
        protected int _y;
        private WireDirection _defaultDirection = WireDirection.WireLeft;
        public Guid Guid { get; set; }

        protected MGAnimate StandAni;
        protected MGAnimate RunAni;
        protected MGAnimate DieAni;
        protected MGAnimate _animate;
        public Actor()
        {
            _islive = true;


            //Sprite = MGSprite.MGSpriteWithSpriteFrameName("40010.png");
            //_y = Control.SharedControl().GetRandom(0, 5);
            //Position = new Vector2(1360, _y * 94 + 47);
            //var next = new Random().Next(-10, 10);
            //Sprite.Position = new Vector2(0, 20 + next);

            //ComeDirection = WireDirection.WireRight;
            //GoDirection = WireDirection.WireLeft;
            //AddChild(Sprite);
            //RunAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(4001)[0]);
            //StandAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(4001)[1]);
            //DieAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(4001)[2]);

            //Hp = 1000;
            //_animate = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetElectricAnimation());
        }

        public void Run(Vector2 vector2)
        {
            this.Sprite.RunAction(MGRepeatForever.Actions(RunAni));
            float t = Vector2.Distance(vector2, Position) / 94f * 3.5f;
            this.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(t, vector2), MGCallFunc.ActionWithTarget(() =>
            {
                Stop();
            })));
        }

        public void Run()
        {
            this.Sprite.RunAction(MGRepeatForever.Actions(RunAni));
            Vector = new IntVector(GameConfig.WidthX - 1, _y);
            AtVector = new IntVector(GameConfig.WidthX, _y);
            var wire = WireShowLayer.SharedWireShow().WireDictionary[Vector];
            var fist = wire.Vector.ToUIVector2() + new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
            float t = Vector2.Distance(fist, Position) / 94f * 3.5f;
            this.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(t, fist), MGCallFunc.ActionWithTarget(() =>
            {
                FindPath(wire);
            })));
        }

        public void Stop()
        {
            this.Sprite.StopAllAction();
            this.Sprite.RunAction(MGRepeatForever.Actions(StandAni));
            this.StopAllAction();
        }

        public void GoRun()
        {
            this.Sprite.StopAllAction();
            this.Sprite.RunAction(MGRepeatForever.Actions(RunAni));
            if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(Vector))
            {
                var w = WireShowLayer.SharedWireShow().WireDictionary[Vector];
                var fist = w.Vector.ToUIVector2() + new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
                float t = Vector2.Distance(fist, Position) / 94f * 3.5f;
                if (Speed == 0)
                {
                    t = t / 2f;
                }
                else if (Speed == 2)
                {
                    t = t * 1.5f;
                }

                this.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(t, fist), MGCallFunc.ActionWithTarget(() =>
                {
                    FindPath(w);
                })));
            }
        }

        public void SetPosition()
        {
            Position = Vector.ToUIVector2() + new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
        }

        public void ChangeHp(int value, bool b = false)
        {
            //_electricSprite.Visible = true;
            Hp -= value;
            this.StopAllAction();
            Sprite.StopAllAction();
            if (b)
            {
                if (Hp <= 0)
                {
                    Dead();
                }
                else
                {
                    GoRun();
                }
            }
            else
            {
                Sprite.RunAction(MGSequence.Actions(MGRepeat.Actions(_animate, 3), MGCallFunc.ActionWithTarget(() =>
                {
                    if (Hp <= 0)
                    {
                        Dead();
                    }
                    else
                    {
                        GoRun();
                    }
                })));
            }

            //_electricSprite.RunAction(MGSequence.Actions(_animate, MGCallFunc.ActionWithTarget(() =>
            //{
            //    if (Hp <= 0)
            //    {
            //        //Dead();
            //    }
            //    else
            //    {
            //        Run();
            //    }
            //})));
        }

        public virtual void Dead()
        {
            GameConfig.ChangeMoney(30);
            Sprite.RunAction(MGSequence.Actions(DieAni, MGCallFunc.ActionWithTarget(() =>
            {
                Visible = false;
                ZombieShowLayer.SharedZombieShow().RemoveChild(this);
                _islive = false;
            })));
        }

        public void FindPath(Wire wire)
        {
            AtVector = Vector;
            if (wire.WireState != WireType.Wirei)
            {
                switch (ComeDirection)
                {
                    case WireDirection.WireRight:
                        #region Right
                        {
                            if (wire.WireData[3] != 0)
                            {
                                if (wire.WireData[1] == 0)
                                {
                                    if (wire.WireData[0] != 0 && wire.WireData[2] != 0)
                                    {
                                        if (Vector.Y < GameConfig.HightY / 2f)
                                        {
                                            ComeDirection = WireDirection.WireDown;
                                            GoDirection = WireDirection.WireUp;
                                        }
                                        else
                                        {
                                            ComeDirection = WireDirection.WireUp;
                                            GoDirection = WireDirection.WireDown;
                                        }
                                    }
                                    else
                                    {
                                        if (wire.WireData[0] != 0)
                                        {
                                            ComeDirection = WireDirection.WireDown;
                                            GoDirection = WireDirection.WireUp;
                                            if (wire.Vector.Y == GameConfig.HightY - 1)
                                            {
                                                ComeDirection = WireDirection.WireRight;
                                                GoDirection = WireDirection.WireLeft;
                                            }
                                        }
                                        if (wire.WireData[2] != 0)
                                        {
                                            ComeDirection = WireDirection.WireUp;
                                            GoDirection = WireDirection.WireDown;
                                            if (wire.Vector.Y == 0)
                                            {
                                                ComeDirection = WireDirection.WireRight;
                                                GoDirection = WireDirection.WireLeft;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    ComeDirection = WireDirection.WireRight;
                                    GoDirection = WireDirection.WireLeft;
                                }
                            }
                            else
                            {
                                ComeDirection = WireDirection.WireRight;
                                GoDirection = WireDirection.WireLeft;
                            }
                        }
                        #endregion
                        break;
                    case WireDirection.WireLeft:
                        #region Left
                        {
                            if (wire.WireData[1] != 0)
                            {
                                if (wire.WireData[0] != 0 && wire.WireData[2] != 0)
                                {
                                    if (Vector.Y > GameConfig.HightY / 2f)
                                    {
                                        ComeDirection = WireDirection.WireDown;
                                        GoDirection = WireDirection.WireUp;
                                    }
                                    else
                                    {
                                        ComeDirection = WireDirection.WireUp;
                                        GoDirection = WireDirection.WireDown;
                                    }
                                }
                                else
                                {
                                    if (wire.WireData[0] != 0)
                                    {
                                        ComeDirection = WireDirection.WireDown;
                                        GoDirection = WireDirection.WireUp;
                                    }
                                    if (wire.WireData[2] != 0)
                                    {
                                        ComeDirection = WireDirection.WireUp;
                                        GoDirection = WireDirection.WireDown;
                                    }
                                }
                                //else
                                //{
                                //    ComeDirection = WireDirection.WireLeft;
                                //    GoDirection = WireDirection.WireRight;
                                //}
                            }
                            else
                            {
                                ComeDirection = WireDirection.WireRight;
                                GoDirection = WireDirection.WireLeft;
                            }
                        }
                        #endregion
                        break;
                    case WireDirection.WireUp:
                        #region Up
                        {
                            if (wire.WireData[1] != 0)
                            {
                                GoDirection = WireDirection.WireLeft;
                                ComeDirection = WireDirection.WireRight;
                            }
                            else
                            {
                                if (wire.WireData[2] != 0)
                                {
                                    GoDirection = WireDirection.WireDown;
                                    ComeDirection = WireDirection.WireUp;
                                }
                                else
                                {
                                    if (wire.WireData[3] != 0)
                                    {
                                        GoDirection = WireDirection.WireRight;
                                        ComeDirection = WireDirection.WireLeft;
                                    }
                                    else
                                    {
                                        GoDirection = WireDirection.WireLeft;
                                        ComeDirection = WireDirection.WireRight;
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    case WireDirection.WireDown:
                        #region Down
                        {
                            if (wire.WireData[1] != 0)
                            {
                                ComeDirection = WireDirection.WireRight;
                                GoDirection = WireDirection.WireLeft;
                            }
                            else
                            {
                                if (wire.WireData[0] != 0)
                                {
                                    GoDirection = WireDirection.WireUp;
                                    ComeDirection = WireDirection.WireDown;
                                }
                                else
                                {
                                    if (wire.WireData[3] != 0)
                                    {
                                        GoDirection = WireDirection.WireRight;
                                        ComeDirection = WireDirection.WireLeft;
                                    }
                                    else
                                    {
                                        ComeDirection = WireDirection.WireRight;
                                        GoDirection = WireDirection.WireLeft;
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                }
            }
            else
            {
                ComeDirection = WireDirection.WireRight;
                GoDirection = WireDirection.WireLeft;
            }
            switch (GoDirection)
            {
                case WireDirection.WireUp:
                    Vector = new IntVector(Vector.X, Vector.Y + 1);
                    break;
                case WireDirection.WireLeft:
                    Vector = new IntVector(Vector.X - 1, Vector.Y);
                    break;
                case WireDirection.WireRight:
                    Vector = new IntVector(Vector.X + 1, Vector.Y);
                    break;
                case WireDirection.WireDown:
                    Vector = new IntVector(Vector.X, Vector.Y - 1);
                    break;
            }
            if (Vector.X > GameConfig.WidthX - 1)
            {
                Vector = new IntVector(GameConfig.WidthX - 1, Vector.Y);
                GoDirection = WireDirection.WireLeft;
                ComeDirection = WireDirection.WireRight;
            }
            if (Vector.X < 0)
            {
                //Vector = new IntVector(0, Vector.Y);
                GoDirection = WireDirection.WireLeft;
                ComeDirection = WireDirection.WireRight;

                var f = Vector.ToUIVector2() + new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
                this.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(3, f), MGCallFunc.ActionWithTarget(() =>
                {
                    //todo: GameOver
                    if (_islive)
                    {
                        //if (TeachingScene.ShardMainGame().IsTeaching)
                        //{
                        //    TeachingScene.ShardMainGame().IsTeaching = false;
                        //    TeachingScene.ShardMainGame().RunAction(MGSequence.Actions(MGJumpBy.ActionWithDuration(.8f, new Vector2(0, 10), 10, 7), MGCallFunc.ActionWithTarget(() =>
                        //    {
                        //        TeachingScene.ShardMainGame().Position = new Vector2();
                        //        Control.SharedControl().ReplaceScene(Control.SceneType.GameOverSceneType);
                        //    })));
                        //}
                        //else
                        {
                            if (CardShowLayer.SharedCardShow() == null)
                            {
                                MainGameScene.ShardMainGame().RunAction(MGSequence.Actions(MGJumpBy.ActionWithDuration(.8f, new Vector2(0, 10), 10, 7), MGCallFunc.ActionWithTarget(() =>
                                {
                                    MainGameScene.ShardMainGame().Position = new Vector2();
                                    Control.SharedControl().ReplaceScene(Control.SceneType.GameOverSceneType);
                                })));
                            }
                            else
                            {
                                if (!CardShowLayer.SharedCardShow().IsHas[Vector.Y])
                                {
                                    MainGameScene.ShardMainGame().RunAction(MGSequence.Actions(MGJumpBy.ActionWithDuration(1, new Vector2(0, 10), 10, 10), MGCallFunc.ActionWithTarget(() =>
                                    {
                                        MainGameScene.ShardMainGame().Position = new Vector2();
                                        Control.SharedControl().ReplaceScene(Control.SceneType.GameOverSceneType);
                                    })));
                                }
                            }
                        }


                    }
                    //var actor = new Actor();
                    //ZombieShowLayer.SharedZombieShow().AddChild(actor);
                    //actor.Run();
                })));
                return;
            }
            if (Vector.Y > GameConfig.HightY - 1)
            {
                Vector = new IntVector(Vector.X, Vector.Y - 1);
                GoDirection = WireDirection.WireLeft;
                ComeDirection = WireDirection.WireRight;
            }
            if (Vector.Y < 0)
            {
                Vector = new IntVector(Vector.X, 0);
                GoDirection = WireDirection.WireLeft;
                ComeDirection = WireDirection.WireRight;
            }

            var w = WireShowLayer.SharedWireShow().WireDictionary[Vector];
            var fist = w.Vector.ToUIVector2() + new Vector2(GameConfig.GirdSize / 2f, GameConfig.GirdSize / 2f);
            float t = Vector2.Distance(fist, Position) / 94f * 3.5f;
            if (Speed == 0)
            {
                t = t / 2f;
            }
            else if (Speed == 2)
            {
                t = t * 1.5f;
            }
            this.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(t, fist), MGCallFunc.ActionWithTarget(() =>
            {
                FindPath(w);
            })));
        }
    }
}
