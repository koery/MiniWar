using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGSpriteFrameCache
    {
        protected Dictionary<string, MGSpriteFrame> m_pSpriteFrames;
        protected Dictionary<string, string> m_pSpriteFramesAliases;

        private static MGSpriteFrameCache _sharedSpriteFrameCache = null;
        public static MGSpriteFrameCache SharedSpriteFrameCache()
        {
            return _sharedSpriteFrameCache ?? (_sharedSpriteFrameCache = new MGSpriteFrameCache());
        }

        private MGSpriteFrameCache()
        {
            m_pSpriteFrames = new Dictionary<string, MGSpriteFrame>();
            m_pSpriteFramesAliases = new Dictionary<string, string>();
        }

        public static void PurgeSharedSpriteFrameCache()
        {
            _sharedSpriteFrameCache = null;
        }


        public void AddSpriteFrame(MGSpriteFrame pobFrame, string pszFrameName)
        {
            if (!m_pSpriteFrames.ContainsKey(pszFrameName))
            {
                m_pSpriteFrames.Add(pszFrameName, pobFrame);
            }
        }

        public void AddSpriteFramesWithFile(string pszPlist)
        {
            string pszPath = pszPlist;
            Dictionary<string, Object> dict = MGFileUtils.DictionaryWithContentsOfFile(pszPath);

            string texturePath = "";
            Dictionary<string, Object> metadataDict = dict.Keys.Contains("metadata") ?
                (Dictionary<string, Object>)dict["metadata"] : null;

            if (metadataDict != null)
            {
                // try to read  texture file name from meta data
                if (metadataDict.Keys.Contains("textureFileName"))
                {
                    texturePath = (valueForKey("textureFileName", metadataDict));
                }
            }

            if (!string.IsNullOrEmpty(texturePath))
            {
                texturePath = MGFileUtils.fullPathFromRelativeFile(texturePath, pszPath);
            }
            else
            {
                texturePath = pszPath;
                int index = pszPath.IndexOf("/");
                if (index < 0)
                {
                    index = pszPath.IndexOf(@"\");
                }
                if (index > 0)
                {
                    texturePath = Path.GetFileNameWithoutExtension(pszPath);
                }

                Debug.WriteLine("cocos2d: CCSpriteFrameCache: Trying to use file {0} as texture", texturePath);
            }

            Texture2D pTexture = MGTextureCache.SharedTextureCache().AddImage(texturePath);

            if (pTexture != null)
            {
                AddSpriteFramesWithDictionary(dict, pTexture);
            }
            else
            {
                Debug.WriteLine("cocos2d: CCSpriteFrameCache: Couldn't load texture");
            }
        }

        public void AddSpriteFramesWithDictionary(Dictionary<string, Object> pobDictionary, Texture2D pobTexture)
        {
            Dictionary<string, Object> metadataDict = null;
            if (pobDictionary.Keys.Contains("metadata"))
            {
                metadataDict = (Dictionary<string, Object>)pobDictionary["metadata"];
            }

            Dictionary<string, Object> framesDict = null;
            if (pobDictionary.Keys.Contains("frames"))
            {
                framesDict = (Dictionary<string, Object>)pobDictionary["frames"];
            }

            int format = 0;

            // get the format
            if (metadataDict != null)
            {
                format = int.Parse(metadataDict["format"].ToString());
            }

            Debug.Assert(format >= 0 && format <= 3);

            foreach (var key in framesDict.Keys)
            {
                Dictionary<string, Object> frameDict = framesDict[key] as Dictionary<string, Object>;
                MGSpriteFrame spriteFrame = new MGSpriteFrame();

                if (format == 0)
                {
                    float x = float.Parse(frameDict["x"].ToString());
                    float y = float.Parse(frameDict["y"].ToString());
                    float w = float.Parse(frameDict["width"].ToString());
                    float h = float.Parse(frameDict["height"].ToString());
                    float ox = float.Parse(frameDict["offsetX"].ToString());
                    float oy = float.Parse(frameDict["offsetY"].ToString());
                    int ow = int.Parse(frameDict["originalWidth"].ToString());
                    int oh = int.Parse(frameDict["originalHeight"].ToString());
                    // check ow/oh
                    if (ow == 0 || oh == 0)
                    {
                        Debug.WriteLine("cocos2d: WARNING: originalWidth/Height not found on the CCSpriteFrame. AnchorPoint won't work as expected. Regenrate the .plist");
                    }
                    // abs ow/oh
                    ow = Math.Abs(ow);
                    oh = Math.Abs(oh);
                    // create frame
                    spriteFrame = new MGSpriteFrame();
                    spriteFrame.InitWithTexture(pobTexture,
                                                new Rectangle((int)x, (int)y, (int)w, (int)h),
                                                false,
                                                new Vector2(ox, oy),
                                                new Vector2(ow, oh)
                                                );
                }
                else if (format == 1 || format == 2)
                {
                    Rectangle frame = MNS.RectangleFromString(frameDict["frame"].ToString());
                    bool rotated = false;

                    // rotation
                    if (format == 2)
                    {
                        if (frameDict.Keys.Contains("rotated"))
                        {
                            rotated = int.Parse(valueForKey("rotated", frameDict)) == 0 ? false : true;
                        }
                    }

                    Vector2 offset = MNS.Vector2FromString(valueForKey("offset", frameDict));
                    Vector2 sourceSize = MNS.Vector2FromString(valueForKey("sourceSize", frameDict));

                    // create frame
                    spriteFrame = new MGSpriteFrame();
                    spriteFrame.InitWithTexture(pobTexture,
                        frame,
                        rotated,
                        offset,
                        sourceSize
                        );
                }
                else
                    if (format == 3)
                    {
                        // get values
                        Rectangle spriteSize = MNS.RectangleFromString(valueForKey("spriteSize", frameDict));
                        Vector2 spriteOffset = MNS.Vector2FromString(valueForKey("spriteOffset", frameDict));
                        Vector2 spriteSourceSize = MNS.Vector2FromString(valueForKey("spriteSourceSize", frameDict));
                        Rectangle textureRect = MNS.RectangleFromString(valueForKey("textureRect", frameDict));
                        bool textureRotated = false;
                        if (frameDict.Keys.Contains("textureRotated"))
                        {
                            textureRotated = int.Parse(valueForKey("textureRotated", frameDict)) == 0 ? false : true;
                        }

                        // get aliases
                        var list = frameDict["aliases"];
                        List<object> aliases = (frameDict["aliases"] as List<object>);
                        string frameKey = key;
                        foreach (var item2 in aliases)
                        {
                            string oneAlias = item2.ToString();
                            if (m_pSpriteFramesAliases.Keys.Contains(oneAlias))
                            {
                                if (m_pSpriteFramesAliases[oneAlias] != null)
                                {
                                    Debug.WriteLine("cocos2d: WARNING: an alias with name {0} already exists", oneAlias);
                                }
                            }
                            if (!m_pSpriteFramesAliases.Keys.Contains(frameKey))
                            {
                                m_pSpriteFramesAliases.Add(frameKey, oneAlias);
                            }
                        }

                        // create frame
                        spriteFrame = new MGSpriteFrame();
                        spriteFrame.InitWithTexture(pobTexture,
                                        new Rectangle(textureRect.X, textureRect.Y, spriteSize.Width, spriteSize.Height),
                                        textureRotated,
                                        spriteOffset,
                                        spriteSourceSize);
                    }

                // add sprite frame
                if (!m_pSpriteFrames.Keys.Contains(key))
                {
                    m_pSpriteFrames.Add(key, spriteFrame);
                }
            }

        }

        public void AddSpriteFramesWithFile(string pszPlist, Texture2D pobTexture)
        {
            string pszPath = pszPlist;
            Dictionary<string, Object> dict = MGFileUtils.DictionaryWithContentsOfFile(pszPath);
            AddSpriteFramesWithDictionary(dict, pobTexture);
        }

        public void AddSpriteFramesWithFile(string plist, string textureFileName)
        {
            Debug.Assert(textureFileName != null);
            Texture2D texture = MGTextureCache.SharedTextureCache().AddImage(textureFileName);

            if (texture != null)
            {
                AddSpriteFramesWithFile(plist, texture);
            }
            else
            {
                Debug.WriteLine("cocos2d: CCSpriteFrameCache: couldn't load texture file. File not found {0}", textureFileName);
            }
        }

        public void RemoveSpriteFrames()
        {
            this.m_pSpriteFrames.Clear();
            this.m_pSpriteFramesAliases.Clear();
        }

        public void RemoveSpriteFrameByName(string pszName)
        {
            if (string.IsNullOrEmpty(pszName))
            {
                return;
            }

            string key = m_pSpriteFramesAliases[pszName];

            if (!string.IsNullOrEmpty(key))
            {
                m_pSpriteFrames.Remove(key);
                m_pSpriteFramesAliases.Remove(key);
            }
            else
            {
                m_pSpriteFrames.Remove(pszName);
            }
        }

        public void RemoveSpriteFramesFromFile(string plist)
        {
            string path = plist;
            Dictionary<string, object> dict = MGFileUtils.DictionaryWithContentsOfFile(path);

            RemoveSpriteFramesFromDictionary(dict);
        }

        public void RemoveSpriteFramesFromDictionary(Dictionary<string, object> dictionary)
        {
            Dictionary<string, Object> framesDict = (Dictionary<string, Object>)dictionary["frames"];
            List<string> keysToRemove = new List<string>();

            foreach (var key in framesDict.Keys)
            {
                if (m_pSpriteFrames.ContainsKey(key))
                {
                    keysToRemove.Remove(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                m_pSpriteFrames.Remove(key);
            }
        }

        public void RemoveSpriteFramesFromTexture(Texture2D texture)
        {
            List<string> keysToRemove = new List<string>();

            foreach (var key in m_pSpriteFrames.Keys)
            {
                MGSpriteFrame frame = m_pSpriteFrames[key];
                if (frame != null && (frame.Texture.Name == texture.Name))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                m_pSpriteFrames.Remove(key);
            }
        }

        public MGSpriteFrame SpriteFrameByName(string pszName)
        {
            MGSpriteFrame frame = m_pSpriteFrames[pszName];
            if (frame == null)
            {
                // try alias dictionary
                string key = (string)m_pSpriteFramesAliases[pszName];
                if (key != null)
                {
                    frame = m_pSpriteFrames[key];
                    if (frame == null)
                    {
                        Debug.WriteLine("cocos2d: CCSpriteFrameCahce: Frame '{0}' not found", pszName);
                    }
                }
            }
            return frame;
        }

        private string valueForKey(string key, Dictionary<string, Object> dict)
        {
            if (dict != null)
            {
                if (dict.Keys.Contains(key))
                {
                    string pString = (string)dict[key];
                    return pString != null ? pString : "";
                }
            }
            return "";
        }
    }
}
