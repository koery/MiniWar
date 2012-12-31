using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MGFramework
{
    public class MGUIBase : MGNode
    {
        public SoundEffect SoundEffect;
        protected int TouchID;
        public float _bold;
        public bool _enabled;

        public MGUIBase()
        {
            Init();
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public float Bold
        {
            get { return _bold; }
            set { _bold = value; }
        }

        private void Init()
        {
            TouchID = -1;
            _enabled = true;
            _bold = 0f;
            Width = 0f;
            Height = 0f;
            SoundEffect = null;
        }

        public bool IsInside(Vector2 point)
        {
            Vector2 value = base.ConvertToWorldPos();
            value -= _anchorPoisiton;
            return point.X >= value.X - _bold && point.X < value.X + Width + _bold && point.Y >= value.Y - _bold &&
                   point.Y < value.Y + Height + _bold;
        }

        public virtual void TouchesCancel()
        {
            TouchID = -1;
        }
    }
}