using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGColorNode : MGNode
    {
        protected FrameStruct FS;
        protected Texture2D Texture;

        public MGColorNode()
        {
            FS = DataManager.GetFS("PixelTexture");
            Texture = FS.Texture;
            Left = FS.TextCoords.X;
            Top = FS.TextCoords.Y;
            Height = FS.Height;
            Width = FS.Width;
            _anchorPoisiton = FS.Anchor;
        }

        public MGColorNode(Color nodeColor)
            : this()
        {
            _color = nodeColor;
            _opacity = nodeColor.A;
            _scale = new Vector2(1f * MGDirector.WinWidth / FS.Width, 1f * MGDirector.WinHeight / FS.Height);
        }

        public MGColorNode(Color nodeColor, int width, int height)
            : this()
        {
            _color = nodeColor;
            _opacity = nodeColor.A;
            _scale = new Vector2(1f * width / FS.Width, 1f * height / FS.Height);
        }

        public override void SetPositionTL(float x, float y)
        {
            var vector = new Vector2(x + _scale.X * _anchorPoisiton.X, y + _scale.Y * _anchorPoisiton.Y);
            if (vector != _position)
            {
                base.Position = vector;
            }
        }

        public override void SetPositionTR(float x, float y)
        {
            var vector = new Vector2(MGDirector.WinWidth - x - (Width - _anchorPoisiton.X) * _scale.X, y + _anchorPoisiton.Y * _scale.Y);
            if (vector != _position)
            {
                base.Position = vector;
            }
        }

        public override void SetPositionBL(float x, float y)
        {
            var vector = new Vector2(x + _anchorPoisiton.X * _scale.X, MGDirector.WinHeight - y - (Height - _anchorPoisiton.Y) * _scale.Y);
            if (vector != _position)
            {
                base.Position = vector;
            }
        }

        public override void SetPositionBR(float x, float y)
        {
            var vector = new Vector2(MGDirector.WinWidth - x - (Width - _anchorPoisiton.X) * _scale.X,
                                     MGDirector.WinHeight - y - (Height - _anchorPoisiton.Y) * _scale.Y);
            if (vector != _position)
            {
                base.Position = vector;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (Visible)
            {
                Vector2 position;
                Vector2 vector;
                base.ConvertToWorld(out position, out vector);
                float rotation = base.ConvertToWorldRot();
                Color color = _color * (_opacity / 255f);
                color.A = (byte)_opacity;
                if (camera == null)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, _blendState);
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, _blendState, null, null, null, null, camera.Transform);
                }
                spriteBatch.Draw(Texture, position,
                                 new Rectangle((int)FS.TextCoords.X, (int)FS.TextCoords.Y, (int)FS.Width,
                                               (int)FS.Width), color, rotation, _anchorPoisiton, _scale, SpriteEffects.None,
                                 0f);
                spriteBatch.End();
            }
        }
    }
}