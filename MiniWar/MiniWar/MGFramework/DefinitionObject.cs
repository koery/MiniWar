using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class DefinitionObject
    {
        private List<string> _elementNames;
        public DefinitionObject ParentDefinitionObject;

        public DefinitionObject()
        {
            ParentDefinitionObject = null;
        }

        public static Vector2 StrToVector2(string str)
        {
            string[] array = str.Split(new[]{
                                               ','
                                           });
            string value = array[0];
            string value2 = array[1];
            return new Vector2((float) Convert.ToDouble(value), (float) Convert.ToDouble(value2));
        }

        public virtual DefinitionObject InitWithParentObject(DefinitionObject parentObject)
        {
            ParentDefinitionObject = parentObject;
            return this;
        }

        public virtual void DefinitionObjectDidInit()
        {
            _elementNames = new List<string>();
        }

        public virtual void DefinitionObjectDidReceiveString(string str)
        {
        }

        public virtual void DefinitionObjectDidFinishParsing()
        {
        }

        public virtual void ChildDefinitionObjectDidInit(DefinitionObject childObject)
        {
        }

        public virtual void ChildDefinitionObject(DefinitionObject childObject, string str)
        {
        }

        public virtual void ChildDefinitionObjectDidFinishParsing(DefinitionObject childObject)
        {
        }

        public virtual void UndefinedElementDidStart(string elementName)
        {
            _elementNames.Add(elementName);
        }

        public virtual void UndefinedElementDidReceiveString(string str)
        {
        }

        public virtual void UndefinedElementDidFinish(string elementName)
        {
            if (_elementNames[_elementNames.Count - 1] == elementName)
            {
                _elementNames.RemoveAt(_elementNames.Count - 1);
            }
        }
    }
}