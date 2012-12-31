using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VSZombie.Effect
{
    class ThunderSprite : MGSprite
    {
        private readonly List<Texture2D> _electricPointTextures = new List<Texture2D>();
        public List<Vector2> Points = new List<Vector2>();
        public List<Vector2> Savepoint;
        private ElectricEffect _electricEffect;
        private Vector2 _endpos;
        private bool _isEnded;
        private Vector2 _startpos;

        public ThunderSprite()
        {
            Init();
        }

        private void Init()
        {
            for (int i = 1; i < 4; i++)
            {
                var texture =
                    MGDirector.SharedDirector().Content.Load<Texture2D>("ElectricPoint" +
                                                i.ToString(CultureInfo.InvariantCulture));
                _electricPointTextures.Add(texture);
            }
            var spriteBatch = new SpriteBatch(MGDirector.SharedDirector().Graphics.GraphicsDevice);
            _electricEffect = new ElectricEffect(spriteBatch, _electricPointTextures) { Density = 10 };
        }

        //public override void Update(float dt)
        //{
        //    _electricEffect.Update(dt);
        //    base.Update(dt);
        //}

        //public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        //{
        //    {
        //        _electricEffect.Draw();
        //    }
        //    base.Draw(spriteBatch, camera);
        //}


        public bool AddPoint(Vector2 point)
        {
            //Hide();
            if (point == Vector2.Zero && _isEnded)
                return false;

            Points.Add(point);
            if (_startpos == Vector2.Zero)
            {
                _startpos = point;
                Savepoint = new List<Vector2>();
            }
            Savepoint.Add(point);
            return true;
        }

        public void EndPoint(Vector2 point)
        {
            if (!_isEnded)
            {
                _isEnded = true;
                _endpos = point;
                //if ((_endpos - _startpos).Length() < 100 || _startpos == Vector2.Zero)
                //{
                //    Hide();
                //    return;
                //}
                Points.Add(point);
                Savepoint.Add(point);
                if (Points.Count < 3)
                {
                    var p = (Points[0] + Points[1]) / 2;
                    Points.Insert(1, p);
                }

                InitElectricEffect();
                Points = new List<Vector2>();

                //var actions = new List<FiniteTimeAction>
                //                  {
                //                      CCDelayTime.actionWithDuration(0.05f),
                //                      CCFadeTo.actionWithDuration(0.05f, 255),
                //                      CCDelayTime.actionWithDuration(0.05f),
                //                      CCFadeTo.actionWithDuration(0.05f, 50),
                //                      CCDelayTime.actionWithDuration(0.05f),
                //                      CCFadeTo.actionWithDuration(0.05f, 255),
                //                      CCDelayTime.actionWithDuration(0.05f),
                //                      CCFadeTo.actionWithDuration(0.05f, 50),
                //                      CCDelayTime.actionWithDuration(0.05f),
                //                      CCFadeTo.actionWithDuration(0.05f, 255),
                //                      CCCallFunc.actionWithTarget(null, Hide)
                //                  };
                //runAction(CCSequence.actions(actions.ToArray()));
            }
        }

        public int GetLength()
        {
            if (Savepoint == null)
            {
                Savepoint = new List<Vector2>();
            }
            return Savepoint.Count;
        }

        public float GetPixelLength()
        {
            if (Savepoint == null)
            {
                Savepoint = new List<Vector2>();
            }
            float num = 0f;
            for (int i = 1; i < Savepoint.Count; i++)
            {
                Vector2 vector = Savepoint[i - 1] - Savepoint[i];
                num += vector.Length();
            }
            return num;
        }

        public Vector2 GetPoint(int num)
        {
            if (Savepoint == null)
            {
                Savepoint = new List<Vector2>();
            }
            if (num >= Savepoint.Count)
            {
                num = Savepoint.Count - 1;
            }
            var p = new Vector2(Savepoint[num].X, Savepoint[num].Y);
            return p;
        }

        //public Vector2 GetBeginPoint()
        //{
        //    CCPoint x = CCDirector.sharedDirector().convertToGL(new Vector2(_startpos.X, _startpos.Y));
        //    _startpos = Vector2.Zero;
        //    return x;
        //}

        //public CCPoint GetEndPoint()
        //{
        //    CCPoint y = CCDirector.sharedDirector().convertToGL(new CCPoint(_endpos.X, _endpos.Y));
        //    _endpos = Vector2.Zero;
        //    return y;
        //}

        private void InitElectricEffect()
        {
            _electricEffect.ClearFlows();
            for (int i = 0; i < 1; i++)
            {
                ElectricFlow flow = _electricEffect.AddFlow();
                const int count = 20;
                for (int j = 0; j < Points.Count; j++)
                {
                    float radius = ((Points.Count - 1f) / 2 - Math.Abs(j - (Points.Count - 1f) / 2)) * count;
                    Debug.WriteLine(radius);
                    flow.AddNode(Points[j], radius, 1f);
                }
            }
        }

        public void Hide()
        {
            if (Savepoint == null)
            {
                Savepoint = new List<Vector2>();
            }
            if (Points == null)
            {
                Points = new List<Vector2>();
            }
            Savepoint.Clear();
            Points.Clear();
            _startpos = Vector2.Zero;
            _endpos = Vector2.Zero;
            _isEnded = false;
        }
    }
}
