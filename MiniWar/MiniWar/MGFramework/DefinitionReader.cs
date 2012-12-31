using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;

namespace MGFramework
{
    public class DefinitionReader
    {
        #region Delegates

        public delegate void Callback();

        #endregion

        public static int WebConnection;
        protected static Callback _callback;
        private Type _curClass;
        public DefinitionObject CurrentDefinitionObject;
        private bool _isCurrentElementDefined;

        public DefinitionReader()
        {
            WebConnection = 0;
            CurrentDefinitionObject = null;
            _isCurrentElementDefined = false;
        }

        public static void SetCallback(Callback c)
        {
            _callback = c;
        }

        private void StartReader(XmlReader reader)
        {
            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                switch (nodeType)
                {
                    case XmlNodeType.Element:
                        {
                            StartElement(reader.Name);
                            break;
                        }
                    case XmlNodeType.Attribute:
                        {
                            break;
                        }
                    case XmlNodeType.Text:
                        {
                            ParseContent(reader.Value);
                            break;
                        }
                    default:
                        {
                            if (nodeType == XmlNodeType.EndElement)
                            {
                                EndElement(reader.Name);
                            }
                            break;
                        }
                }
            }
        }

        private void HandleGetResponse(IAsyncResult ar)
        {
            var webRequest = (WebRequest)ar.AsyncState;
            WebResponse webResponse = webRequest.EndGetResponse(ar);
            Stream responseStream = webResponse.GetResponseStream();
            XmlReader reader = XmlReader.Create(responseStream);
            StartReader(reader);
            WebConnection--;
            if (WebConnection == 0)
            {
               
                _callback();
            }
        }

        public void ParseXMLFileFromWeb(string pathname)
        {
            WebConnection++;
            WebRequest webRequest = WebRequest.Create(pathname);
            webRequest.BeginGetResponse(HandleGetResponse, webRequest);
        }

        public void ParseXMLFile(string pathname)
        {
            XmlReader reader = XmlReader.Create(pathname);
            StartReader(reader);
        }

        private void StartElement(string name)
        {
            //Assembly executingAssembly = Assembly.GetExecutingAssembly();
            //Type type = executingAssembly.GetType("MGFramework." + name, false);
            Type type = Type.GetType("MGFramework." + name, false);

            if (type != null)
            {
                var definitionObject = (DefinitionObject)Activator.CreateInstance(type);
                if (_isCurrentElementDefined)
                {
                    definitionObject.InitWithParentObject(CurrentDefinitionObject);
                }
                definitionObject.DefinitionObjectDidInit();
                if (definitionObject.ParentDefinitionObject != null)
                {
                    definitionObject.ParentDefinitionObject.ChildDefinitionObjectDidInit(definitionObject);
                }
                _curClass = type;
                CurrentDefinitionObject = definitionObject;
                _isCurrentElementDefined = true;
                return;
            }
            _isCurrentElementDefined = false;
            if (CurrentDefinitionObject != null)
            {
                CurrentDefinitionObject.UndefinedElementDidStart(name);
            }
        }

        private void ParseContent(string str)
        {
            if (_isCurrentElementDefined)
            {
                CurrentDefinitionObject.DefinitionObjectDidReceiveString(str);
                return;
            }
            CurrentDefinitionObject.UndefinedElementDidReceiveString(str);
        }

        private void EndElement(string name)
        {
            //Assembly executingAssembly = Assembly.GetExecutingAssembly();
            //Type type = executingAssembly.GetType("MGFramework." + name, false);
            Type type = Type.GetType("MGFramework." + name, false);
            if (type != _curClass)
            {
                if (CurrentDefinitionObject != null)
                {
                    CurrentDefinitionObject.UndefinedElementDidFinish(name);
                }
                return;
            }
            CurrentDefinitionObject.DefinitionObjectDidFinishParsing();
            if (CurrentDefinitionObject.ParentDefinitionObject != null)
            {
                CurrentDefinitionObject.ParentDefinitionObject.ChildDefinitionObjectDidFinishParsing(
                    CurrentDefinitionObject);
            }
            CurrentDefinitionObject = CurrentDefinitionObject.ParentDefinitionObject;
            if (CurrentDefinitionObject != null)
            {
                _curClass = CurrentDefinitionObject.GetType();
                _isCurrentElementDefined = true;
                return;
            }
            _curClass = null;
            _isCurrentElementDefined = false;
        }
    }
}