using System.Collections.Generic;

namespace MGFramework
{
    public class MObject : DefinitionObject
    {
        public List<Part> Parts;
        public string UniqueID;

        public override void DefinitionObjectDidReceiveString(string str)
        {
            base.DefinitionObjectDidReceiveString(str);
            string[] array = str.Split(new[] {'\n'});
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                if (text.Length > 2)
                {
                    string[] array2 = text.Split(new[] {'='});
                    string a = array2[0];
                    string text2 = array2[1];
                    if (a == "ObjectID")
                    {
                        UniqueID = text2;
                    }
                }
            }
        }

        public override void DefinitionObjectDidInit()
        {
            base.DefinitionObjectDidInit();
            Parts = new List<Part>();
        }

        public override void DefinitionObjectDidFinishParsing()
        {
            base.DefinitionObjectDidFinishParsing();
            DataManager.SetObject(this, UniqueID);
        }

        public override void ChildDefinitionObjectDidFinishParsing(DefinitionObject childObject)
        {
            base.ChildDefinitionObjectDidFinishParsing(childObject);
            if (childObject.GetType() == typeof (Part))
            {
                var item = (Part) childObject;
                Parts.Add(item);
            }
        }
    }
}