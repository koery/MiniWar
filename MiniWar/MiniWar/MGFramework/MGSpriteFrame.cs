using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGSpriteFrame
    {
        public bool Rotated { get; set; }
        public Vector2 Offset { get; set; }
        public Vector2 OriginalSize { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle Rect { get; set; }

        public static MGSpriteFrame FrameWithTexture(Texture2D pobTexture, Rectangle rect)
        {
            MGSpriteFrame pSpriteFrame = new MGSpriteFrame(); ;
            pSpriteFrame.InitWithTexture(pobTexture, rect);

            return pSpriteFrame;
        }

        public static MGSpriteFrame FrameWithTexture(Texture2D pobTexture, Rectangle rect, bool rotated, Vector2 offset, Vector2 originalSize)
        {
            MGSpriteFrame pSpriteFrame = new MGSpriteFrame();
            pSpriteFrame.InitWithTexture(pobTexture, rect, rotated, offset, originalSize);

            return pSpriteFrame;
        }

        public bool InitWithTexture(Texture2D pobTexture, Rectangle rect)
        {
            Rect = rect;
            return InitWithTexture(pobTexture, Rect, false, new Vector2(0, 0), new Vector2(rect.Width,rect.Height));
        }

        public bool InitWithTexture(Texture2D pobTexture, Rectangle rect, bool rotated, Vector2 offset, Vector2 originalSize)
        {
            Texture = pobTexture;
            Rect = rect;
            Rotated = rotated;
            Offset = offset;
            OriginalSize = originalSize;
            return true;
        }
    }
}
