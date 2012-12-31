using System;
using System.Collections.Generic;

namespace MGFramework
{
    public class MFrame : DefinitionObject
    {
        public string EventFunc;
        public List<PartState> PartStates;
        public float Time;

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
                    string value = array2[1];
                    if (a == "Time")
                    {
                        Time = (float) Convert.ToDouble(value);
                    }
                    else
                    {
                        if (a == "Event")
                        {
                            EventFunc = value;
                        }
                    }
                }
            }
        }

        public override void DefinitionObjectDidInit()
        {
            base.DefinitionObjectDidInit();
            EventFunc = null;
            PartStates = new List<PartState>();
        }

        public override void ChildDefinitionObjectDidFinishParsing(DefinitionObject childObject)
        {
            base.ChildDefinitionObjectDidFinishParsing(childObject);
            if (childObject.GetType() == typeof (PartState))
            {
                var item = (PartState) childObject;
                PartStates.Add(item);
            }
        }
    }
}