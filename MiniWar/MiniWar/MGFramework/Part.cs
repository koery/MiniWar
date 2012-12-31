using System;
using System.Collections.Generic;

namespace MGFramework
{
    public class Part : DefinitionObject
    {
        #region PartType enum

        public enum PartType
        {
            PartTypeImage,
            PartTypeButton,
            PartTypeCheckBox,
            PartTypeProgressBar,
            PartTypeLabelAtlas,
            PartTypeLabel
        }

        #endregion

        public string Content;

        public List<Image> Images;
        public PartType Type;
        public string UniqueID;
        public int ZAxis;

        public override void DefinitionObjectDidInit()
        {
            base.DefinitionObjectDidInit();
            Images = new List<Image>();
            Type = PartType.PartTypeImage;
            Content = null;
        }

        public override void DefinitionObjectDidReceiveString(string str)
        {
            base.DefinitionObjectDidReceiveString(str);
            string[] array = str.Split(new[] {'\n'});
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
                        if (a == "ImageID")
                        {
                            if (Type != PartType.PartTypeLabel)
                            {
                                Images.Add(DataManager.ImageByKey(text2));
                            }
                        }
                        else
                        {
                            if (a == "ZAxis")
                            {
                                ZAxis = Convert.ToInt32(text2);
                            }
                            else
                            {
                                if (a == "Type")
                                {
                                    Type = (PartType) Convert.ToInt32(text2);
                                }
                                else
                                {
                                    if (a == "Content")
                                    {
                                        Content = text2;
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