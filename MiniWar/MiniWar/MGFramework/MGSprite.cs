using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGSprite : MGNode
    {
        protected Texture2D _texture;
        protected Dictionary<string, MGAnimation> Anims;
        protected FrameStruct _fs;
        public Texture2D Texture
        {
            get
            {
                return this._texture;
            }
            set
            {
                this._texture = value;
            }
        }

        public FrameStruct FS
        {
            get
            {
                return this._fs;
            }
            set
            {
                this._fs = value;
                this._texture = this._fs.Texture;
                this.Left = this._fs.TextCoords.X;
                this.Top = this._fs.TextCoords.Y;
                this.Height = this._fs.Height;
                this.Width = this._fs.Width;
                this.Anchor = _fs.Anchor;
            }
        }

        protected Vector2 UnflippedOffsetPositionFromCenter;
        private bool rectRotated;
        public MGSpriteFrame DisplayFrame
        {
            set
            {
                UnflippedOffsetPositionFromCenter = value.Offset;

                Texture2D pNewTexture = value.Texture;
                if (pNewTexture != Texture)
                {
                    this.Texture = pNewTexture;
                }

                rectRotated = value.Rotated;
                SetTextureRect(value.Rect, value.Rotated, value.OriginalSize);
            }
            get
            {
                return MGSpriteFrame.FrameWithTexture(Texture,
                                                 Rect,
                                                 rectRotated,
                                                 UnflippedOffsetPositionFromCenter,
                                                 ContentSize);
            }
        }

        public bool FlipX { get; set; }
        public bool FlipY { get; set; }
        bool _usesBatchNode;
        protected MGTextureAtlas TextureAtlas;
        private bool m_bUseBatchNode;
        public bool IsUseBatchNode
        {
            get
            {
                return m_bUseBatchNode;
            }
            set
            {
                m_bUseBatchNode = value;
            }
        }
        protected void UpdateTextureCoords(Rectangle rect)
        {
            Texture2D tex = _usesBatchNode ? TextureAtlas.Texture : Texture;
            if (tex == null)
            {
                return;
            }

            float atlasWidth = (float)tex.Width;
            float atlasHeight = (float)tex.Height;

            float left, right, top, bottom;

            if (rectRotated)
            {
                left = rect.X / atlasWidth;
                right = left + (rect.Height / atlasWidth);
                top = rect.Y / atlasHeight;
                bottom = top + (rect.Width / atlasHeight);

                if (FlipX)
                {
                    Swap<float>(ref top, ref bottom);
                }

                if (FlipY)
                {
                    Swap<float>(ref left, ref right);
                }
            }
            else
            {
                left = rect.X / atlasWidth;
                right = left + rect.Width / atlasWidth;
                top = rect.Y / atlasHeight;
                bottom = top + rect.Height / atlasHeight;

                if (FlipX)
                {
                    Swap<float>(ref left, ref right);
                }

                if (FlipY)
                {
                    Swap<float>(ref top, ref bottom);
                }
            }
        }

        private void Swap<T>(ref T a, ref T b)
        {
            T tmp;
            tmp = a;
            a = b;
            b = tmp;
        }


        private Vector2 _offset;
        public Vector2 Offset
        {
            get { return _offset; }
        }

        private bool _dirty;
        public bool Dirty
        {
            get
            {
                return _dirty;
            }
            set
            {
                _dirty = value;
            }
        }

        protected Rectangle Rect;
        public void SetTextureRect(Rectangle rect)
        {
            SetTextureRect(rect, false, new Vector2(rect.Width, rect.Height));
        }
        public void SetTextureRect(Rectangle rect, bool rotated, Vector2 size)
        {
            Rect = rect;
            Left = Rect.Left;
            Top = Rect.Top;
            Width = Rect.Width;
            Height = Rect.Height;

            rectRotated = rotated;
            base.Anchor = new Vector2(.5f, .5f);

            ContentSize = size;
            UpdateTextureCoords(Rect);

            Vector2 relativeOffsetInPixels = UnflippedOffsetPositionFromCenter;

            if (FlipX)
            {
                relativeOffsetInPixels.X = -relativeOffsetInPixels.X;
            }
            if (FlipY)
            {
                relativeOffsetInPixels.Y = -relativeOffsetInPixels.X;
            }

            _offset.X = relativeOffsetInPixels.X + (ContentSize.X - Rect.Width) / 2;
            _offset.Y = relativeOffsetInPixels.X + (ContentSize.Y - Rect.Height) / 2;

            if (m_bUseBatchNode)
            {
                _dirty = true;
            }
            else
            {
                float x1 = 0 + Offset.X;
                float y1 = 0 + Offset.Y;
                float x2 = x1 + Rect.Width;
                float y2 = y1 + Rect.Height;
            }

        }

        public MGSprite()
        {
            this.Anims = null;
            this._texture = null;
            this._fs = null;
        }

        public MGSprite(string fsName)
        {
            this.Anims = null;
            FrameStruct fS = DataManager.GetFS(fsName);
            this.FS = fS;
        }

        public void InitWithFilename(string filename)
        {
            this._texture = MGTextureMgr.LoadTexture(filename);
            this.Left = 0f;
            this.Top = 0f;
            this.Height = (float)this._texture.Height;
            this.Width = (float)this._texture.Width;
            this.Anchor = new Vector2(.5f, .5f);
            InitWithTexture(_texture, new Rectangle((int)Left, (int)Top, (int)Width, (int)Height));
        }


        public void InitWithFilename(string filename, Vector2 size)
        {
            this._texture = MGTextureMgr.LoadTexture(filename);
            this.Left = 0f;
            this.Top = 0f;
            this.Height = (float)size.Y;
            this.Width = (float)size.X;
            this.Anchor = new Vector2(.5f, .5f);
            InitWithTexture(_texture, new Rectangle((int)Left, (int)Top, (int)Width, (int)Height));
        }

        public static MGSprite MGSpriteWithFilename(string filename)
        {
            MGSprite sprite = new MGSprite();
            sprite.InitWithFilename(filename);
            return sprite;
        }

        public static MGSprite MGSpriteWithFilename(string filename, Vector2 size)
        {
            MGSprite sprite = new MGSprite();
            sprite.InitWithFilename(filename, size);

            return sprite;
        }


        public static MGSprite MGSpriteWithSpriteFrameName(string pszSpriteFrameName)
        {
            MGSpriteFrame pFrame = MGSpriteFrameCache.SharedSpriteFrameCache().SpriteFrameByName(pszSpriteFrameName);
            string msg = string.Format("Invalid spriteFrameName: {0}", pszSpriteFrameName);
            Debug.Assert(pFrame != null, msg);
            return MGSpriteWithSpriteFrame(pFrame);
        }

        public static MGSprite MGSpriteWithSpriteFrame(MGSpriteFrame spriteFrame)
        {
            MGSprite pobSprite = new MGSprite();
            if (pobSprite != null && pobSprite.InitWithSpriteFrame(spriteFrame))
            {
                return pobSprite;
            }
            return null;
        }

        public bool InitWithSpriteFrame(MGSpriteFrame pSpriteFrame)
        {
            Debug.Assert(pSpriteFrame != null);
            bool bRet = InitWithTexture(pSpriteFrame.Texture, pSpriteFrame.Rect);
            DisplayFrame = pSpriteFrame;
            this._texture = pSpriteFrame.Texture;
            this._fs = new FrameStruct();

            _fs.Anchor = new Vector2(.5f, .5f);
            _fs.Width = pSpriteFrame.Rect.Width;
            _fs.Height = pSpriteFrame.Rect.Height;
            _fs.TextCoords = new Vector2(pSpriteFrame.Rect.X, pSpriteFrame.Rect.Y);
            _fs.Texture = pSpriteFrame.Texture;
            return bRet;
        }

        public bool InitWithTexture(Texture2D texture, Rectangle rect)
        {
            Debug.Assert(texture != null);
            Texture = texture;
            SetTextureRect(rect);

            return true;
        }

        public void AddAnimation(MGAnimation anim)
        {
            if (this.Anims == null)
            {
                this.Anims = new Dictionary<string, MGAnimation>();
            }
            if (!this.Anims.ContainsKey(anim.Name))
            {
                this.Anims.Add(anim.Name, anim);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (this.Visible && this._texture != null)
            {
                float num = 0f;
                Vector2 position;
                Vector2 scale;
                base.ConvertToWorld(out position, out scale);
                num = base.ConvertToWorldRot();
                position = new Vector2(position.X, MGDirector.WinHeight - position.Y);
                Color color = this._color * (this._opacity / 255f);
                color.A = (byte)this._opacity;
                if (camera == null)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, this._blendState);
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, this._blendState, null, null, null, null, camera.Transform);
                }
                Vector2 anchor = this._anchorPoisiton;
                SpriteEffects effects = SpriteEffects.None;
                //if (scale.X < 0f)
                //{
                //    scale.X = -scale.X;
                //    anchor = new Vector2(this._width - anchor.X, anchor.Y);
                //    effects = SpriteEffects.FlipHorizontally;
                //    num = -num;
                //}
                //if (scale.Y < 0f)
                //{
                //    scale.Y = -scale.Y;
                //    anchor = new Vector2(anchor.X, this._height - anchor.Y);
                //    effects = SpriteEffects.FlipVertically;
                //    num = -num;
                //}
                if (FlipX)
                {
                    anchor = new Vector2(this.Width - anchor.X, anchor.Y);
                    effects = SpriteEffects.FlipHorizontally;
                    num = -num;
                }
                if (FlipY)
                {
                    anchor = new Vector2(anchor.X, this.Height - anchor.Y);
                    effects = SpriteEffects.FlipVertically;
                    num = -num;
                }


                spriteBatch.Draw(this._texture, position, new Rectangle?(new Rectangle((int)this.Left, (int)this.Top, (int)Width, (int)this.Height)), color, num, anchor, scale, effects, 0f);
                spriteBatch.End();
            }
        }

        public virtual void SetFrame(string animName, int index)
        {
            if (this.Anims == null)
            {
                this.Anims = new Dictionary<string, MGAnimation>();
            }
            if (this.Anims.ContainsKey(animName))
            {
                this.FS = this.Anims[animName].GetFrame(index);
            }
        }

        public bool InTapInside(Point Point)
        {
            Vector2 value = base.ConvertToWorldPos();
            value -= this._anchorPoisiton;
            return Point.X >= value.X && Point.X < value.X + this.Width && Point.Y >= value.Y && Point.Y < value.Y + this.Height;
        }

        public bool GetInTapInside(Vector2 Point, out Rectangle rectangle)
        {
            Vector2 value = base.ConvertToWorldPos();
            value -= this._anchorPoisiton;
            rectangle = new Rectangle((int)value.X, (int)value.Y, (int)Width, (int)Height);
            return Point.X >= value.X && Point.X < value.X + this.Width && Point.Y >= value.Y && Point.Y < value.Y + this.Height;
        }


        public Rectangle GetRect()
        {
            return new Rectangle((int)(Position.X - _anchorPoisiton.X),
                (int)(Position.Y - _anchorPoisiton.Y),
                (int)ContentSize.X,
                (int)ContentSize.Y);
        }
    }
}
