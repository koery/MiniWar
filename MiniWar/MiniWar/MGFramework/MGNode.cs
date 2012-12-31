using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGFramework
{
    public class MGNode
    {
        protected List<MGAction> ActionList;
        public List<MGNode> ChildList;
        protected MGNode _parent;

        public int Tag { get; set; }

        protected float Left;
        protected float Top;

        protected Matrix M;
        protected bool TransformDirty;
        protected Vector2 _anchorPoisiton { get; set; }

        protected BlendState _blendState;
        protected MGCamera _camera;

        protected Color _color;
        protected float _opacity;
        protected Vector2 _position;
        protected float _rotation;
        protected float _rotationAlongX;
        protected float _rotationAlongY;
        protected Vector2 _scale;

        public MGNode()
        {
            _parent = null;
            ZOrder = 0;
            Visible = true;
            _position.X = 0f;
            _position.Y = 0f;
            _rotation = 0f;
            _scale.X = 1f;
            _scale.Y = 1f;
            _opacity = 255f;
            Left = 0f;
            Top = 0f;
            Height = 0f;
            Width = 0f;
            _color = new Color(255, 255, 255, 255);
            ActionList = new List<MGAction>();
            ChildList = new List<MGNode>();
            TransformDirty = true;
            _blendState = BlendState.AlphaBlend;
            IsTouchEnable = false;
        }

        public MGNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                TransformDirty = true;
            }
        }

        private Vector2 _anchor;
        public Vector2 Anchor
        {
            get { return _anchor = new Vector2(_anchorPoisiton.X / Width, _anchorPoisiton.Y / Height); }
            set
            {
                _anchor = value;
                _anchorPoisiton = new Vector2(Width * value.X, Height * value.Y);
                TransformDirty = true;
            }
        }

        public Vector2 Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                TransformDirty = true;
            }
        }

        public float ScaleX
        {
            get { return _scale.X; }
            set
            {
                _scale.X = value;
                TransformDirty = true;
            }
        }

        public float ScaleY
        {
            get { return _scale.Y; }
            set
            {
                _scale.Y = value;
                TransformDirty = true;
            }
        }

        public float Rotation
        {
            get { return MathHelper.ToDegrees(_rotation); }
            set
            {
                _rotation = MathHelper.ToRadians(value);
                TransformDirty = true;
            }
        }

        public float RotationAlongY
        {
            get { return MathHelper.ToDegrees(_rotationAlongY); }
            set
            {
                _rotationAlongY = MathHelper.ToRadians(value);
                TransformDirty = true;
            }
        }

        public float RotationAlongX
        {
            get { return MathHelper.ToDegrees(_rotationAlongX); }
            set
            {
                _rotationAlongX = MathHelper.ToRadians(value);
                TransformDirty = true;
            }
        }

        public int ZOrder { get; set; }

        public int ZInsertOrder { get; set; }

        public bool Visible { get; set; }

        public Color Color
        {
            get { return _color; }
        }

        public virtual float Opacity
        {
            get { return _opacity; }
            set
            {
                foreach (var node in ChildList)
                {
                    node.Opacity = value;
                }
                _opacity = value;
                _color.A = (byte)_opacity;
            }
        }

        public MGCamera Camera
        {
            get { return _camera ?? (_camera = new MGCamera()); }
        }

        public float Height { get; set; }

        public float Width { get; set; }

        public BlendState BlendState
        {
            get { return _blendState; }
            set { _blendState = value; }
        }

        public void SetScale(float value)
        {
            _scale = new Vector2(value, value);
            TransformDirty = true;
        }


        protected Vector2 _contentSize;
        public Vector2 ContentSize
        {
            get
            {
                return _contentSize;
            }
            set
            {

                if (value != _contentSize)
                {
                    _contentSize = value;
                    _anchorPoisiton = new Vector2(ContentSize.X * _anchor.X, ContentSize.Y * _anchor.Y);
                    TransformDirty = true;
                }
            }
        }

        public virtual void SetColor(Color color)
        {
            _color = color;
        }

        public virtual void SetColor(float r, float g, float b)
        {
            _color.R = (byte)r;
            _color.G = (byte)g;
            _color.B = (byte)b;
        }


        public void InitUI(MGLayer parentLayer)
        {
            foreach (MGNode current in ChildList)
            {
                current.InitUI(parentLayer);
            }
            if (GetType() == typeof(MGButton))
            {
                ((MGButton)this).Delegate = (IMGButtonDelegate)parentLayer;
                parentLayer.AddUIBase((MGUIBase)this);
            }
        }

        public virtual void OnSceneActive()
        {
        }

        public virtual void Update(float time)
        {
        }

        public virtual void InputUpdate()
        {
        }

        public void UpdateNode(float time)
        {
            Update(time);
            for (int i = 0; i < ActionList.Count; i++)
            {
                if (ActionList[i].IsEnd)
                {
                    ActionList.Remove(ActionList[i]);
                    i--;
                }
                else
                {
                    ActionList[i].Step(time);
                    if (ActionList.Count == 0)
                    {
                        break;
                    }
                    if (ActionList[i].IsEnd)
                    {
                        ActionList.Remove(ActionList[i]);
                        i--;
                    }
                }
            }
            for (int i = 0; i < ChildList.Count; i++)
            {
                MGNode current = ChildList[i];
                current.UpdateNode(time);
            }
            if (_camera != null)
            {
                _camera.UpdateCamera();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
        }

        public virtual void DrawNode(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (!Visible)
            {
                return;
            }
            MGCamera camera2 = _camera ?? camera;
            foreach (MGNode current in ChildList)
            {
                if (current.ZOrder < 0)
                {
                    current.DrawNode(spriteBatch, camera2);
                }
            }
            Draw(spriteBatch, camera);
            int num = 0;
            for (int i = 0; i < ChildList.Count; i++)
            {
                MGNode node = ChildList[i];
                if (node.ZOrder >= 0)
                {
                    if (num == 0)
                    {
                        num = node.ZOrder;
                    }
                    else
                    {
                        num = node.ZOrder;
                    }
                    node.DrawNode(spriteBatch, camera2);
                }
            }
        }

        public void RunAction(MGAction action)
        {
            ActionList.Add(action);
            action.SetTarget(this);
        }

        public void AddChild(MGNode node, int z)
        {
            int num = 0;
            bool flag = false;
            for (int i = 0; i < ChildList.Count; i++)
            {
                MGNode mgNode = ChildList[i];
                if (mgNode.ZOrder > z)
                {
                    flag = true;
                    ChildList.Insert(num, node);
                    break;
                }
                num++;
            }
            if (!flag)
            {
                ChildList.Add(node);
            }
            node.ZOrder = z;
            node.ZInsertOrder = num;
            node.Parent = this;
            ChildList.Sort(delegate(MGNode p1, MGNode p2)
            {
                if (p1.ZOrder.CompareTo(p2.ZOrder) == 0)
                {
                    return p1.ZInsertOrder.CompareTo(p2.ZInsertOrder);
                }
                return p1.ZOrder.CompareTo(p2.ZOrder);
            });
        }

        public void AddChild(MGNode node)
        {
            AddChild(node, 0);
        }

        public void RemoveChild(MGNode node)
        {
            foreach (MGNode current in ChildList)
            {
                if (current == node)
                {
                    ChildList.Remove(current);
                    break;
                }
            }
        }

        public void ReorderChild(MGNode node, int newOrder)
        {
            RemoveChild(node);
            AddChild(node, newOrder);
        }

        public void StopAllAction()
        {
            ActionList.Clear();
        }

        public void PauseAllAction()
        {
            foreach (MGAction current in ActionList)
            {
                current.Pause();
            }
        }

        public void ResumeAllAction()
        {
            foreach (MGAction current in ActionList)
            {
                current.Resume();
            }
        }

        public bool IsTouchEnable { get; set; }

        public virtual bool TouchesBegan(MouseState touch, Point point)
        {
            foreach (MGNode current in ChildList)
            {
                if (current.IsTouchEnable)
                {
                    current.TouchesBegan(touch, point);
                }
            }
            return false;
        }

        public virtual bool TouchesEnded(MouseState touch, Point point)
        {
            foreach (MGNode current in ChildList)
            {
                if (current.IsTouchEnable)
                {
                    current.TouchesEnded(touch, point);
                }
            }
            return false;
        }

        public virtual bool TouchesMoved(MouseState touch, Point point)
        {
            foreach (MGNode current in this.ChildList)
            {
                if (current.IsTouchEnable)
                {
                    current.TouchesMoved(touch, point);
                }
            }
            return false;
        }

        public virtual bool TouchesCancel(MouseState touch, Point point)
        {
            foreach (MGNode current in this.ChildList)
            {
                if (current.IsTouchEnable)
                {
                    current.TouchesCancel(touch, point);
                }
            }
            return false;
        }

        public virtual Matrix NodeToParentTransform()
        {
            if (TransformDirty)
            {
                M = Matrix.Identity;
                if (_position.X != 0f || _position.Y != 0f)
                {
                    M = Matrix.CreateTranslation(new Vector3(_position.X, _position.Y, 0f)) * M;
                }
                if (_scale.X != 1f || _scale.Y != 1f)
                {
                    M = Matrix.CreateScale(new Vector3(_scale.X, _scale.Y, 1f)) * M;
                }
                if (_rotation != 0f)
                {
                    M = Matrix.CreateRotationZ(-_rotation) * M;
                }
                if (_rotationAlongX != 0f)
                {
                    M = Matrix.CreateRotationX(_rotationAlongX) * M;
                }
                if (_rotationAlongY != 0f)
                {
                    M = Matrix.CreateRotationY(_rotationAlongY) * M;
                }
                TransformDirty = false;
            }
            return M;
        }

        public Matrix NodeToWorldTransform()
        {
            Matrix matrix = Matrix.Identity;
            if (_parent != null)
            {
                matrix = _parent.NodeToWorldTransform();
            }
            matrix = NodeToParentTransform() * matrix;
            return matrix;
        }

        public Vector2 ConvertToWorldPos()
        {
            Vector3 vector;
            Quaternion quaternion;
            Vector3 vector2;
            NodeToWorldTransform().Decompose(out vector, out quaternion, out vector2);
            return new Vector2(vector2.X, vector2.Y);
        }

        public float ConvertToWorldRot()
        {
            float num = 0f;
            if (_parent != null)
            {
                num += _parent.ConvertToWorldRot();
            }
            num += _rotation;
            return num;
        }

        public void ConvertToWorld(out Vector2 p, out Vector2 s)
        {
            Vector3 vector;
            Quaternion quaternion;
            Vector3 vector2 = Vector3.Zero;
            NodeToWorldTransform().Decompose(out vector, out quaternion, out vector2);
            p = new Vector2(vector2.X, vector2.Y);
            s = new Vector2(vector.X * vector.Z, vector.Y);
        }

        public virtual void SetPosition(Vector2 newPosition)
        {
            if (newPosition != _position)
            {
                Position = newPosition;
            }
        }

        public virtual void SetPositionTL(float x, float y)
        {
            var vector = new Vector2(x + _anchorPoisiton.X, MGDirector.WinHeight - (y + _anchorPoisiton.Y));
            if (vector != _position)
            {
                Position = vector;
            }
        }

        public virtual void SetPositionTR(float x, float y)
        {
            var vector = new Vector2(MGDirector.WinWidth - x - Width + _anchorPoisiton.X, y + _anchorPoisiton.Y);
            if (vector != _position)
            {
                Position = vector;
            }
        }

        public virtual void SetPositionBL(float x, float y)
        {
            var vector = new Vector2(x + _anchorPoisiton.X, MGDirector.WinHeight - y - Height + _anchorPoisiton.Y);
            if (vector != _position)
            {
                Position = vector;
            }
        }

        public virtual void SetPositionBR(float x, float y)
        {
            var vector = new Vector2(MGDirector.WinWidth - x - Width + _anchorPoisiton.X,
                                     MGDirector.WinHeight - y - Height + _anchorPoisiton.Y);
            if (vector != _position)
            {
                Position = vector;
            }
        }
    }
}