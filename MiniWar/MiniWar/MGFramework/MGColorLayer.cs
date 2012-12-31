using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGColorLayer : MGLayer
    {
        private readonly Vector2[] _p;

        public MGColorLayer()
        {
            _p = new Vector2[4];
            if (!MGDirector.SharedDirector().Landscape)
            {
                _p[0] = new Vector2(0f, Config.TARGET_WIN_HEIGHT);
                _p[1] = new Vector2(0f, 0f);
                _p[2] = new Vector2(Config.TARGET_WIN_WIDTH, Config.TARGET_WIN_HEIGHT);
                _p[3] = new Vector2(Config.TARGET_WIN_WIDTH, 0f);
                return;
            }
            _p[0] = new Vector2(0f, Config.TARGET_WIN_WIDTH);
            _p[1] = new Vector2(0f, 0f);
            _p[2] = new Vector2(Config.TARGET_WIN_HEIGHT, Config.TARGET_WIN_WIDTH);
            _p[3] = new Vector2(Config.TARGET_WIN_HEIGHT, 0f);
        }


        public MGColorLayer(Rectangle r)
        {
            _p = new Vector2[4];
            _p[0] = new Vector2(r.X, r.Height);
            _p[1] = new Vector2(r.X, r.Y);
            _p[2] = new Vector2(r.Width, r.Height);
            _p[3] = new Vector2(r.Width, r.Y);
        }

        public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
            Color color = _color * (_opacity / 255f);
            MGDirector.SharedDirector().BasicEffect.Alpha = _opacity / 255f;
            MGPrimitives.DrawPoly(_p, color);
        }
    }
}