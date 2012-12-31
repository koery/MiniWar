using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGCamera
    {
        protected float ViewportHeight;
        protected float ViewportWidth;
        protected Vector3 _origin;
        protected Vector3 _centerPoint;
        protected Vector3 _position;
        protected bool TransformDirty;
        protected Matrix _transform;
        public Vector3 Origin
        {
            get
            {
                return this._origin;
            }
            set
            {
                this._origin = value;
                this.TransformDirty = true;
            }
        }
        public Vector3 CenterPoint
        {
            get
            {
                return this._centerPoint;
            }
            set
            {
                this._centerPoint = value;
                this.TransformDirty = true;
            }
        }
        public Vector3 Position
        {
            get
            {
                return this._position;
            }
            set
            {
                this._position = value;
                this.TransformDirty = true;
            }
        }
        public Matrix Transform
        {
            get
            {
                return this._transform;
            }
        }

        public MGCamera()
        {
            if (MGDirector.SharedDirector().Landscape)
            {
                this.ViewportHeight = (float)MGDirector.SharedDirector().ScreenWidth;
                this.ViewportWidth = (float)MGDirector.SharedDirector().ScreenHeight;
            }
            else
            {
                this.ViewportHeight = (float)MGDirector.SharedDirector().ScreenHeight;
                this.ViewportWidth = (float)MGDirector.SharedDirector().ScreenWidth;
            }
            this._centerPoint = new Vector3(this.ViewportWidth / 2f, this.ViewportHeight / 2f, 1.401298E-45f);
            this._origin = new Vector3(this._centerPoint.X, this._centerPoint.Y, this._centerPoint.Y);
            this._position = this._origin;
            this.TransformDirty = true;
        }

        public void UpdateCamera()
        {
            if (this.TransformDirty)
            {
                this._origin = new Vector3(this._centerPoint.X, this._centerPoint.Y, this._centerPoint.Z);
                this._transform = Matrix.Identity * Matrix.CreateTranslation(-this._position.X, -this._position.Y, this._position.Z) * Matrix.CreateTranslation(this._origin.X, this._origin.Y, this._origin.Z);
                this.TransformDirty = false;
            }
        }
    }
}
