using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace MGFramework
{
    public class MGScene : MGNode
    {
        public virtual void Init()
        {
        }

        public MGScene()
        {
            this.Init();
        }

        public override void OnSceneActive()
        {
            foreach (MGNode current in this.ChildList)
            {
                current.OnSceneActive();
            }
        }

        private bool _touch = false;
        private bool _cancel = false;
        public override void InputUpdate()
        {
            MouseState current = Mouse.GetState();
            {
                Rectangle _windowsBounds = MGDirector.WindowsBounds;
                var x = current.X;
                var y = current.Y;
                Vector2 point = MGDirector.SharedDirector().ConvertToGamePos(new Vector2(x, y));
                if (!_touch)
                {
                    if (current.LeftButton == ButtonState.Pressed)
                    {
                        _touch = true;
                        base.TouchesBegan(current, new Point((int)point.X, (int)point.Y));
                    }
                }
                else
                {
                    if (current.LeftButton == ButtonState.Pressed)
                    {
                        base.TouchesMoved(current, new Point((int)point.X, (int)point.Y));
                    }
                    else
                    {
                        if (current.LeftButton == ButtonState.Released)
                        {
                            _touch = false;
                            base.TouchesEnded(current, new Point((int)point.X, (int)point.Y));

                        }
                    }
                }

                if (current.RightButton == ButtonState.Pressed)
                {
                    _cancel = true;
                }
                if (_cancel)
                {
                    if (current.RightButton == ButtonState.Released)
                    {
                        _cancel = false;
                        base.TouchesCancel(current, new Point((int)point.X, (int)point.Y));
                    }
                }
            }
        }

        public virtual bool Back()
        {
            return false;
        }
    }
}
