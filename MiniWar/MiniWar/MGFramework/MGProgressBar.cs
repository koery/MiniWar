using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGProgressBar : MGSprite
    {
        #region BarType enum

        public enum BarType
        {
            ProgressBarLeft,
            ProgressBarRight,
            ProgressBarTop,
            ProgressBarBottom
        }

        #endregion

        private BarType _type;

        public MGProgressBar()
        {
            _type = BarType.ProgressBarLeft;
        }

        public MGProgressBar(string fsName)
            : base(fsName)
        {
            _type = BarType.ProgressBarLeft;
            Percent = 1.0;
        }

        public double Percent { get; set; }

        public void SetType(BarType type)
        {
            _type = type;
        }

        public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (Visible && _texture != null)
            {
                Vector2 position = base.ConvertToWorldPos();
                position = new Vector2(position.X + _anchorPoisiton.X, MGDirector.WinHeight - position.Y + _anchorPoisiton.Y);
                if (camera == null)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, _blendState);
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, _blendState, null, null, null, null, camera.Transform);
                }
                if (_type == BarType.ProgressBarLeft)
                {
                    spriteBatch.Draw(_texture, position,
                                     new Rectangle((int)Left, (int)Top, (int)(Width * Percent), (int)Height), _color,
                                     _rotation, _anchorPoisiton, _scale, SpriteEffects.None, 0f);
                }
                else
                {
                    if (_type == BarType.ProgressBarRight)
                    {
                        spriteBatch.Draw(_texture, position,
                                         new Rectangle((int)Left + (int)(Width * (1.0 - Percent)), (int)Top,
                                                       (int)(Width * Percent), (int)Height), _color, _rotation,
                                         new Vector2(_anchorPoisiton.X - ((int)(Width * (1.0 - Percent))), _anchorPoisiton.Y), _scale,
                                         SpriteEffects.None, 0f);
                    }
                    else
                    {
                        if (_type == BarType.ProgressBarTop)
                        {
                            spriteBatch.Draw(_texture, position,
                                             new Rectangle((int)Left, (int)Top, (int)Width, (int)(Height * Percent)),
                                             _color, _rotation, _anchorPoisiton, _scale, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            if (_type == BarType.ProgressBarBottom)
                            {
                                position.Y += (float)(Height * Percent);
                                spriteBatch.Draw(_texture, position,
                                                 new Rectangle((int)Left, (int)Top + (int)(Height * Percent),
                                                               (int)Width, (int)(Height * Percent)), _color, _rotation,
                                                 _anchorPoisiton, _scale, SpriteEffects.None, 0f);
                            }
                        }
                    }
                }
                spriteBatch.End();
            }
        }
    }
}