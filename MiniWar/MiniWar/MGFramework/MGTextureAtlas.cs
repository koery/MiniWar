using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGTextureAtlas
    {
        private readonly List<Rectangle> _rectList;
        private readonly Texture2D _texture;
        private readonly int _totalRect;
        public Vector2 Anchor;

        public MGTextureAtlas(Texture2D texture, int capacity)
        {
            _texture = texture;
            _totalRect = capacity;
            _rectList = new List<Rectangle>(_totalRect);
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public void ResetRect()
        {
            _rectList.Clear();
        }

        public void AddRect(Rectangle rect)
        {
            _rectList.Add(rect);
        }

        public void Draw(int idx, SpriteBatch spriteBatch, Vector2 absPos, float rotation, Vector2 scale, Color color)
        {
            spriteBatch.Draw(_texture, absPos, _rectList[idx], color, rotation, Anchor, scale, SpriteEffects.None, 0f);
        }
    }
}