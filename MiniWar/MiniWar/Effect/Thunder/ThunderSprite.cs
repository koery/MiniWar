using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using MGFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VSZombie.Effect;

namespace ThreeDefense.Effect
{
    internal class ThunderSprite : MGSprite
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
            _electricEffect = new ElectricEffect(spriteBatch, _electricPointTextures) { Density = 4 };
        }

        public override void Update(float dt)
        {
            _electricEffect.Update(dt);
            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (_isEnded)
            {
                _electricEffect.Draw();
            }
        }

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
                if ((_endpos - _startpos).Length() < 100 || _startpos == Vector2.Zero)
                {
                    Hide();
                    return;
                }
                Points.Add(point);
                Savepoint.Add(point);
                if (Points.Count < 3 || Points.Count != Savepoint.Count)
                {
                    Hide();
                    return;
                }

                InitElectricEffect();
                Points = new List<Vector2>();

                var actions = new List<MGAction>
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
                                      MGCallFunc.ActionWithTarget(Hide)
                                  };
                RunAction(MGSequence.Actions(actions.ToArray()));
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

        public Vector2 GetBeginPoint()
        {
            Vector2 x = _startpos;
            _startpos = Vector2.Zero;
            return x;
        }

        public Vector2 GetEndPoint()
        {
            Vector2 y = _endpos;
            _endpos = Vector2.Zero;
            return y;
        }

        private void InitElectricEffect()
        {
            _electricEffect.ClearFlows();
            ElectricFlow flow = _electricEffect.AddFlow();
            const int count = 10;
            for (int j = 0; j < Points.Count; j++)
            {
                float radius = ((Points.Count - 1f) / 2 - Math.Abs(j - (Points.Count - 1f) / 2)) * count;
                Debug.WriteLine(radius);
                flow.AddNode(Points[j], radius, 1f);
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