using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGAtlasNode : MGNode
    {
        protected int ItemHeight;
        protected int ItemWidth;
        protected int ItemsPerColumn;
        protected int ItemsPerRow;
        protected MGTextureAtlas TextureAtlas;

        public MGAtlasNode()
        {
            ItemsPerRow = 0;
            ItemsPerColumn = 0;
            ItemWidth = 0;
            ItemHeight = 0;
        }

        public MGAtlasNode(string fsName, int tileWidth, int tileHeight)
        {
            ItemWidth = tileWidth;
            ItemHeight = tileHeight;
            FrameStruct fS = DataManager.GetFS(fsName);
            Texture2D texture = fS.Texture;
            ItemsPerRow = texture.Bounds.Width/tileWidth;
            ItemsPerColumn = texture.Bounds.Height/tileHeight;
            TextureAtlas = new MGTextureAtlas(texture, ItemsPerRow*ItemsPerColumn);
        }
    }
}