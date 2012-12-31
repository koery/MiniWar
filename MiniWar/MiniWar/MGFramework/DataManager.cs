using System.Collections.Generic;

namespace MGFramework
{
    public class DataManager
    {
        public static Dictionary<string, Image> ImageDictionary;
        public static Dictionary<string, MObject> ObjectDictionary;
        public static Dictionary<string, MAnimation> AnimationDictionary;

        public static void InitDataDefinitions()
        {
            ImageDictionary = new Dictionary<string, Image>();
            ObjectDictionary = new Dictionary<string, MObject>();
            AnimationDictionary = new Dictionary<string, MAnimation>();
        }

        public static Image ImageByKey(string key)
        {
            return ImageDictionary[key];
        }

        public static void SetImage(Image img, string key)
        {
            ImageDictionary.Add(key, img);
        }

        public static MObject ObjectByKey(string key)
        {
            return ObjectDictionary[key];
        }

        public static void SetObject(MObject obj, string key)
        {
            ObjectDictionary.Add(key, obj);
        }

        public static MAnimation AnimationByKey(string key)
        {
            return AnimationDictionary[key];
        }

        public static void SetAnimation(MAnimation anim, string key)
        {
            AnimationDictionary.Add(key, anim);
        }

        public static FrameStruct GetFS(string key)
        {
            Image image = ImageByKey(key);
            if (image != null)
            {
                return FrameStruct.FrameStructWithImage(image);
            }
            return null;
        }
    }
}