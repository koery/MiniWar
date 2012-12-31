using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class FrameStruct
    {
        public Texture2D Texture;
        public Vector2 TextCoords;
        public float Width;
        public float Height;
        public Vector2 Anchor;
        public static FrameStruct FrameStructWithImageName(string imgName)
        {
            Image image = DataManager.ImageByKey(imgName);
            return new FrameStruct
            {
                Texture = MGTextureMgr.LoadTexture(image.filename),
                TextCoords = image.topLeft,
                Width = image.widthHeight.X,
                Height = image.widthHeight.Y,
                Anchor = image.anchor
            };
        }

        public static FrameStruct FrameStructWithImage(Image img)
        {
            return new FrameStruct
            {
                Texture = MGTextureMgr.LoadTexture(img.filename),
                TextCoords = img.topLeft,
                Width = img.widthHeight.X,
                Height = img.widthHeight.Y,
                Anchor = img.anchor
            };
        }
    }
}
