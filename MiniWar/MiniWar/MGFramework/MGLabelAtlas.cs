using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGLabelAtlas : MGAtlasNode
    {
        #region LabelType enum

        public enum LabelType
        {
            TextAlignmentLeft,
            TextAlignmentRight,
            TextAlignmentCenter
        }

        #endregion

        private readonly int _charWidth;
        private readonly char _startchar;
        private readonly LabelType _type;
        private string _text;
        public FrameStruct FS;

        public MGLabelAtlas()
        {
            _type = LabelType.TextAlignmentLeft;
        }

        public MGLabelAtlas(string fsName, LabelType type, string text, char startchar, int itemWidth, int itemHeight,
                            int charWidth)
            : base(fsName, itemWidth, itemHeight)
        {
            FS = DataManager.GetFS(fsName);
            Left = ((int) FS.TextCoords.X);
            Top = ((int) FS.TextCoords.Y);
            _type = type;
            _charWidth = charWidth;
            _startchar = startchar;
            SetString(text);
        }

        public void SetString(string text)
        {
            _text = text;
            TextureAtlas.ResetRect();
            for (int i = 0; i < _text.Length; i++)
            {
                int num = (_text[i] - _startchar);
                TextureAtlas.AddRect(new Rectangle((int) Left + num*ItemWidth, (int) Top, ItemWidth, ItemHeight));
            }
        }

        public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (Visible)
            {
                Color color = _color*(_opacity/255f);
                color.A = (byte) _opacity;
                Vector2 vector = base.ConvertToWorldPos();
                vector = new Vector2(vector.X, MGDirector.WinHeight - vector.Y - ItemHeight);
                if (camera == null)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null,
                                      camera.Transform);
                }
                if (_type == LabelType.TextAlignmentLeft)
                {
                    for (int i = 0; i < _text.Length; i++)
                    {
                        TextureAtlas.Draw(i, spriteBatch, new Vector2(vector.X + (i*_charWidth), vector.Y), _rotation,
                                          _scale, color);
                    }
                }
                else
                {
                    if (_type == LabelType.TextAlignmentRight)
                    {
                        for (int j = 0; j < _text.Length; j++)
                        {
                            TextureAtlas.Draw(j, spriteBatch,
                                              new Vector2(x: vector.X - (_text.Length*_charWidth) + (j*_charWidth), y: vector.Y), _rotation, _scale, color);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < _text.Length; k++)
                        {
                            TextureAtlas.Draw(k, spriteBatch,
                                              new Vector2(x: vector.X - (_text.Length*_charWidth/2) + (k*_charWidth), y: vector.Y), _rotation, _scale, color);
                        }
                    }
                }
                spriteBatch.End();
            }
        }
    }
}