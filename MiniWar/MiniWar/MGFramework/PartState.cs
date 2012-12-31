using System;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class PartState : DefinitionObject
    {
        public float Angle;
        public int Frame;
        public float Opacity;
        public Vector2 Pos;
        public float ScaleX;
        public float ScaleY;
        public string UniqueID;

        public override void DefinitionObjectDidInit()
        {
            base.DefinitionObjectDidInit();
            Frame = -1;
            ScaleX = 1f;
            ScaleY = 1f;
            Opacity = 255f;
        }

        public override void DefinitionObjectDidReceiveString(string str)
        {
            base.DefinitionObjectDidReceiveString(str);
            string[] array = str.Split(new[]
                                           {
                                               '\n'
                                           });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                if (text.Length > 2)
                {
                    string[] array2 = text.Split(new[]
                                                     {
                                                         '='
                                                     });
                    string a = array2[0];
                    string text2 = array2[1];
                    if (a == "PartID")
                    {
                        UniqueID = text2;
                    }
                    else
                    {
                        if (a == "Position")
                        {
                            Pos = StrToVector2(text2);
                            Pos = new Vector2(Pos.X, Pos.Y);
                        }
                        else
                        {
                            if (a == "Angle")
                            {
                                Angle = (float) Convert.ToDouble(text2);
                            }
                            else
                            {
                                if (a == "Frame")
                                {
                                    Frame = Convert.ToInt32(text2);
                                }
                                else
                                {
                                    if (a == "ScaleX")
                                    {
                                        ScaleX = (float) Convert.ToDouble(text2);
                                    }
                                    else
                                    {
                                        if (a == "ScaleY")
                                        {
                                            ScaleY = (float) Convert.ToDouble(text2);
                                        }
                                        else
                                        {
                                            if (a == "Opacity")
                                            {
                                                Opacity = (float) Convert.ToDouble(text2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}