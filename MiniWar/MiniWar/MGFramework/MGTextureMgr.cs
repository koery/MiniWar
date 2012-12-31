using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGTextureMgr
    {
        #region Delegates

        public delegate void Callback();

        #endregion

        public static string TextureName;
        public static int WebConnection;
        public static Dictionary<string, Texture2D> TextureDictionary;
        protected static Callback callback;

        public static void Init()
        {
            WebConnection = 0;
            TextureDictionary = new Dictionary<string, Texture2D>();
        }

        public static void SetCallback(Callback c)
        {
            callback = c;
        }

        public static Texture2D TextureByKey(string key)
        {
            if (TextureDictionary.ContainsKey(key))
            {
                return TextureDictionary[key];
            }
            return null;
        }

        public static void SetTexture(Texture2D obj, string key)
        {
            TextureDictionary.Add(key, obj);
        }

        public static Texture2D LoadTexture(string filename)
        {
            Texture2D texture2D = TextureByKey(filename);
            if (texture2D == null)
            {
                texture2D = MGDirector.SharedDirector().Content.Load<Texture2D>(filename);
                SetTexture(texture2D, filename);
                return texture2D;
            }
            return texture2D;
        }

        private static void HandleGetResponse(IAsyncResult ar)
        {
            var webRequest = (WebRequest)ar.AsyncState;
            WebResponse webResponse = webRequest.EndGetResponse(ar);
            Stream responseStream = webResponse.GetResponseStream();
            Texture2D obj = Texture2D.FromStream(MGDirector.SharedDirector().Graphics.GraphicsDevice, responseStream);
            SetTexture(obj, TextureName);
            WebConnection--;
            callback();
        }

        public static void LoadTextureFromWeb(string path)
        {
            WebConnection++;
            string[] array = path.Split(new[] {
                                                '/'
                                            });
            int num = array.Count() - 1;
            string[] array2 = array[num].Split(new[]{
                                                       '.'
                                                   });
            TextureName = array2[0];
            if (TextureByKey(TextureName) == null)
            {
                WebRequest webRequest = WebRequest.Create(path);
                webRequest.BeginGetResponse(HandleGetResponse, webRequest);
            }
        }
    }
}