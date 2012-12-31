using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGLabel : MGNode
    {
        #region LabelType enum

        public enum LabelType
        {
            TextAlignmentLeft,
            TextAlignmentRight,
            TextAlignmentCenter
        }

        #endregion

        private readonly SpriteFont _spriteFont;

        private readonly LabelType _type;
        private string _text;

        public MGLabel()
        {
            _type = LabelType.TextAlignmentLeft;
        }

        public MGLabel(string text, LabelType type, string spriteFont)
        {
            var spritefont = MGDirector.SharedDirector().Content.Load<SpriteFont>(spriteFont);
            _spriteFont = spritefont;
            _text = text;
            _type = type;
            if (_type == LabelType.TextAlignmentLeft)
            {
                _anchorPoisiton = new Vector2(0f, _spriteFont.MeasureString(_text).Y / 2f);
                return;
            }
            if (_type == LabelType.TextAlignmentRight)
            {
                _anchorPoisiton = new Vector2(_spriteFont.MeasureString(_text).X, _spriteFont.MeasureString(_text).Y / 2f);
                return;
            }
            if (_type == LabelType.TextAlignmentCenter)
            {
                _anchorPoisiton = _spriteFont.MeasureString(_text) / 2f;
            }
        }

        public MGLabel(string text, LabelType type, SpriteFont spriteFont)
        {
            _spriteFont = spriteFont;
            _text = text;
            _type = type;
            if (_type == LabelType.TextAlignmentLeft)
            {
                _anchorPoisiton = new Vector2(0f, _spriteFont.MeasureString(_text).Y / 2f);
                return;
            }
            if (_type == LabelType.TextAlignmentRight)
            {
                _anchorPoisiton = new Vector2(_spriteFont.MeasureString(_text).X, _spriteFont.MeasureString(_text).Y / 2f);
                return;
            }
            if (_type == LabelType.TextAlignmentCenter)
            {
                _anchorPoisiton = _spriteFont.MeasureString(_text) / 2f;
            }
        }

        public void SetString(string text)
        {
            _text = text;
            if (_type == LabelType.TextAlignmentLeft)
            {
                _anchorPoisiton = new Vector2(0f, _spriteFont.MeasureString(_text).Y / 2f);
                return;
            }
            if (_type == LabelType.TextAlignmentRight)
            {
                _anchorPoisiton = new Vector2(_spriteFont.MeasureString(_text).X, _spriteFont.MeasureString(_text).Y / 2f);
                return;
            }
            if (_type == LabelType.TextAlignmentCenter)
            {
                _anchorPoisiton = _spriteFont.MeasureString(_text) / 2f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (Visible)
            {
                Vector2 position;
                Vector2 scale;
                base.ConvertToWorld(out position, out scale);
                float rotation = base.ConvertToWorldRot();
                position = new Vector2(position.X, MGDirector.WinHeight - position.Y);
                if (camera == null)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null,
                                      camera.Transform);
                }
                spriteBatch.DrawString(_spriteFont, _text, position, _color, rotation, _anchorPoisiton, scale,
                                       SpriteEffects.None, 0f);
                spriteBatch.End();
            }
        }
    }
}