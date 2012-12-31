using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGTextureCache
    {
        protected Dictionary<string, Texture2D> m_pTextures;
        private static MGTextureCache _sharedTextureCache;

        private MGTextureCache()
        {
            Debug.Assert(_sharedTextureCache == null, "Attempted to allocate a second instance of a singleton.");
            m_pTextures = new Dictionary<string, Texture2D>();
            _dictLock = new object();
        }

        public static MGTextureCache SharedTextureCache()
        {
            return _sharedTextureCache ?? (_sharedTextureCache = new MGTextureCache());
        }

        public static void PurgeSharedTextureCache()
        {
            _sharedTextureCache = null;
        }

        private object _dictLock;
        public Texture2D AddImage(string fileimage)
        {
            Debug.Assert(fileimage != null, "TextureCache: fileimage MUST not be NULL");

            Texture2D texture;
            lock (_dictLock)
            {
                string pathKey = fileimage;
                bool isTextureExist = m_pTextures.TryGetValue(pathKey, out texture);
                if (!isTextureExist)
                {
                    texture = MGDirector.SharedDirector().Content.Load<Texture2D>(fileimage);
                    m_pTextures.Add(pathKey, texture);
                }
            }

            return texture;
        }

        public Texture2D TextureForKey(string key)
        {
            Texture2D texture = null;
            try
            {
                m_pTextures.TryGetValue(key, out texture);
            }
            catch (ArgumentNullException)
            {
                Debug.WriteLine("Texture of key {0} is not exist.", key);
            }

            return texture;
        }

        public void RemoveAllTextures()
        {
            m_pTextures.Clear();
        }

        public void RemoveTexture(Texture2D texture)
        {
            if (texture == null)
            {
                return;
            }

            string key = (from kvp in m_pTextures where kvp.Value == texture select kvp.Key).FirstOrDefault();
            if (key != null)
            {
                m_pTextures.Remove(key);
            }
        }
    }
}
