using System.Collections.Generic;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar;
using MiniWar.Zombie;

namespace ThreeDefense.Effect
{

    internal class Thunder
    {
        public Callback Callback;
        private const int BranchCount = 5;
        private const int SpotCount = 2;
        private MGSprite[] _branchSprite;
        private MGLayer _layer;
        private MGSprite[] _spotSprite;
        public ThunderSprite _thunderSprite;

        public void Init(MGLayer layer)
        {
            _layer = layer;
            _thunderSprite = new ThunderSprite();
            _spotSprite = new MGSprite[SpotCount];
            _branchSprite = new MGSprite[BranchCount];

            _layer.AddChild(_thunderSprite);

            for (int i = 0; i < SpotCount; i++)
            {
                _spotSprite[i] = MGSprite.MGSpriteWithSpriteFrameName("thunder_point.png");
                _layer.AddChild(_spotSprite[i], 310);
            }

            for (int i = 0; i < BranchCount; i++)
            {
                _branchSprite[i] = MGSprite.MGSpriteWithSpriteFrameName("thunder_branch.png");
                _layer.AddChild(_branchSprite[i], 310);
            }

            Hide();
        }

        //private float _interval;
        //private bool _candrag;
        //public void Update(float dt)
        //{
        //    _interval += dt;
        //    if (_interval > 0.05)
        //    {
        //        _interval = 0f;
        //        _candrag = true;
        //    }
        //}

        public bool AddPoint(Vector2 point, bool first)
        {
            //var mainGameLogic = MainGameLogic.SharedGameLogic();
            //if (mainGameLogic.Mp <= 120)
            //    return false;
            bool flag = false;
            if (first)
            {
                _thunderSprite.Hide();
                flag = _thunderSprite.AddPoint(point);
            }
            else
            {
                flag = _thunderSprite.AddPoint(point);
            }
            //var length = _thunderSprite.GetLength();
            //var num = _thunderSprite.GetPixelLength();
            //if (!flag)
            //{
            //    num = _thunderSprite.GetPixelLength();
            //    var pos = _thunderSprite.GetPoint(length);
            //    num += (pos - point).Length();
            //}
            //if (num > mainGameLogic.Mp)
            //{
            //    EndPoint(point);
            //}
            return flag;
        }

        public void Reset()
        {
            _thunderSprite.Hide();
        }


        public void EndPoint(Vector2 point)
        {
            _thunderSprite.EndPoint(point);

            Vector2 position = _thunderSprite.GetBeginPoint();
            if (position.X == 0)
            {
                return;
            }
            _spotSprite[0].Position = new Vector2(position.X, 768 - position.Y);
            position = _thunderSprite.GetEndPoint();
            _spotSprite[1].Position = new Vector2(position.X, 768 - position.Y);
            for (int i = 0; i < SpotCount; i++)
            {
                _spotSprite[i].Visible = true;
            }

            var array = new[] { 3, 5, 8, 12, 15 };
            var array2 = new[] { 2, 4, 6, 9, 10 };
            for (int j = 0; j < BranchCount; j++)
            {
                if (_thunderSprite.GetLength() > array[j])
                {
                    _branchSprite[j].Visible = true;
                    _branchSprite[j].Rotation = Control.SharedControl().GetRandom(359);
                    var uipoint = new Vector2(_thunderSprite.GetPoint(array2[j]).X, _thunderSprite.GetPoint(array2[j]).Y);
                    _branchSprite[j].Position = new Vector2(uipoint.X, 768 - uipoint.Y);
                }
            }

            for (int k = 0; k < BranchCount; k++)
            {
                MGCallFunc callback = MGCallFunc.ActionWithTarget(Hide);
                var action = new List<MGAction>
                                 {
                                     MGDelay.ActionWithDuration(0.05f),
                                     MGFadeTo.ActionWithDuration(0.05f, 255),
                                     MGDelay.ActionWithDuration(0.05f),
                                     MGFadeTo.ActionWithDuration(0.05f, 50),
                                     MGDelay.ActionWithDuration(0.05f),
                                     MGFadeTo.ActionWithDuration(0.05f, 255),
                                     MGDelay.ActionWithDuration(0.05f),
                                     MGFadeTo.ActionWithDuration(0.05f, 50),
                                     MGDelay.ActionWithDuration(0.05f),
                                     MGFadeTo.ActionWithDuration(0.05f, 255),
                                     MGFadeTo.ActionWithDuration(0.05f, 50),
                                     callback
                                 };
                _branchSprite[k].RunAction(MGSequence.Actions(action.ToArray()));
            }
            Collision();
        }


        public Actor actor;
        private void Collision()
        {
            if (actor != null)
            {
                int length = _thunderSprite.GetLength();
                for (int i = 1; i < length; i++)
                {
                    Vector2 point = _thunderSprite.GetPoint(i - 1);
                    Vector2 point2 = _thunderSprite.GetPoint(i);
                    Vector2 p1 = new Vector2(point.X, 768 - point.Y);
                    Vector2 p2 = new Vector2(point2.X, 768 - point2.Y);

                    if (actor.Sprite.InTapInside(new Point((int)((p1 + p2) / 2f).X,(int)((p1 + p2) / 2f).Y)))
                    {
                        actor.ChangeHp(1000);
                        if (actor.Hp > 0)
                        {
                            actor.Run(new Vector2(855, 286));
                        }
                        return;
                    }

                    //if (actor.Position.X < p2.X && actor.Position.X > p1.X)
                    //{
                    //    if (actor.Position.Y + actor.Sprite.Position.Y / 2f < p2.Y + 40 && actor.Position.Y + actor.Sprite.Position.Y / 2f > p1.Y - 40)
                    //    {
                    //        actor.ChangeHp(1000);
                    //        if (actor.Hp > 0)
                    //        {
                    //            actor.Run(new Vector2(855, 286));
                    //        }
                    //        return;
                    //    }
                    //}

                    //else if (actor.Position.X < p1.X && actor.Position.X > p2.X)
                    //{
                    //    if (actor.Position.Y + actor.Sprite.Position.Y / 2f < p1.Y + 40 && actor.Position.Y + actor.Sprite.Position.Y / 2f > p2.Y - 40)
                    //    {
                    //        actor.ChangeHp(1000);
                    //        if (actor.Hp > 0)
                    //        {
                    //            actor.Run(new Vector2(855, 286));
                    //        }
                    //        return;
                    //    }
                    //}
                }
            }

        }

        private void Hide()
        {
            for (int i = 0; i < SpotCount; i++)
            {
                _spotSprite[i].Visible = false;
            }

            for (int i = 0; i < BranchCount; i++)
            {
                MGSprite t = _branchSprite[i];
                t.Visible = false;
            }
        }
    }
}