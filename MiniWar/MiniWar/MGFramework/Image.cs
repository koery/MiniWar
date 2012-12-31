using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class Image : DefinitionObject
    {
        public const float INFINITY = float.MaxValue;
        public string uniqueID;
        public string filename;
        public Vector2 topLeft;
        public Vector2 widthHeight;
        public Vector2 anchor;
        public Image()
        {
        }
        public Image(string UniqueID, string Filename, Vector2 TopLeft, Vector2 WidthHeight, Vector2 Anchor)
        {
            this.uniqueID = UniqueID;
            this.topLeft = TopLeft;
            this.widthHeight = WidthHeight;
            this.anchor = Anchor;
            this.filename = Filename.Split(new char[]
			{
				'.'
			})[0];
            DataManager.SetImage(this, this.uniqueID);
        }
        public override void DefinitionObjectDidInit()
        {
            base.DefinitionObjectDidInit();
            this.anchor = Vector2.Zero;
        }
        public override void DefinitionObjectDidReceiveString(string str)
        {
            base.DefinitionObjectDidReceiveString(str);
            string[] array = str.Split(new char[] { '\n' });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                if (text.Length > 2)
                {
                    string[] array2 = text.Split(new char[]
					{
						'='
					});
                    string a = array2[0];
                    string text2 = array2[1];
                    if (a == "ImageID")
                    {
                        this.uniqueID = text2;
                    }
                    else
                    {
                        if (a == "LeftTop")
                        {
                            this.topLeft = DefinitionObject.StrToVector2(text2);
                        }
                        else
                        {
                            if (a == "WidthHeight")
                            {
                                this.widthHeight = DefinitionObject.StrToVector2(text2);
                            }
                            else
                            {
                                if (a == "AnchorPoint")
                                {
                                    this.anchor = DefinitionObject.StrToVector2(text2);
                                }
                                else
                                {
                                    if (a == "FileName")
                                    {
                                        this.filename = text2.Split(new char[]
										{
											'.'
										})[0];
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        public override void DefinitionObjectDidFinishParsing()
        {
            base.DefinitionObjectDidFinishParsing();
            this.anchor = Vector2.Zero;
            DataManager.SetImage(this, this.uniqueID);
        }
    }
}
