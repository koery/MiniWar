using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VSZombie.Effect
{
    public class ElectricFlow
    {
        #region Properties

        private const float TileLeft = .2f;
        private readonly SpriteBatch _spriteBatch;
        private readonly List<Texture2D> _textures;
        public float Age;
        public int Density = 10;
        public List<ElectricNode> ElectricNodes = new List<ElectricNode>();
        public Color LineColor = Color.Red;
        private Texture2D _currentTexture;

        #endregion

        #region Constructor

        public ElectricFlow(SpriteBatch spriteBatch, List<Texture2D> textures)
        {
            _spriteBatch = spriteBatch;
            _textures = textures;
            Reset();
        }

        #endregion

        #region Update & Draw

        public void Update(float elapsedTime)
        {
            if (Age < TileLeft)
            {
                Age += elapsedTime;
            }
            else
            {
                Reset();
            }

            foreach (ElectricNode node in ElectricNodes)
            {
                node.Update();
            }
        }

        public void Draw()
        {
            if (ElectricNodes.Count < 1)
            {
                return;
            }

            VertexPositionColor[] points = null;
            //switch (EffectType)
            //{
            //    case ElectricEffectType.Line:
            //points = PrimitiveUtil.DrawLine(ElectricNodes, 3, LineColor);
            //        break;
            //case ElectricEffectType.Bezier:
            points = PrimitiveUtil.DrawBezierCurve(ElectricNodes, 3, LineColor);
            //    break;
            //case ElectricEffectType.CatmullRom:
            //points = PrimitiveUtil.DrawCatmullRomCurve(ElectricNodes, Density, LineColor);
            //    break;
            //default:
            //    break;
            //}

            var position = new Vector2();
            var origin = new Vector2 { X = _currentTexture.Width / 2, Y = _currentTexture.Width / 2 };
            var color = new Color(128, 128, 128, Random.Next(32, 192));
            //Color color = new Color(255,255,255);
            foreach (VertexPositionColor point in points)
            {
                position.X = point.Position.X;
                position.Y = point.Position.Y;

                _spriteBatch.Draw(_currentTexture, position, null, color, 0f, origin, 1f, SpriteEffects.None, 0);
            }
        }

        #endregion

        private static readonly Random Random = new Random();

        private void Reset()
        {
            Age = 0;
            _currentTexture = _textures[Random.Next(_textures.Count)];
        }

        public void AddNode(Vector2 fiducialPoint, float radius, float? speedFactor)
        {
            if (speedFactor == null)
            {
                speedFactor = 1f;
            }
            var node = new ElectricNode(fiducialPoint, radius, speedFactor);
            ElectricNodes.Add(node);
        }
    }
}