﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MGFramework
{
    public class CCSAXParser
    {
        ICCSAXDelegator m_pDelegator;
        public bool init(string pszEncoding)
        {
            // nothing to do
            return true;
        }

        public bool parse(string pszFile)
        {
            XmlReader xmlReader = XmlReader.Create(pszFile);
            int dataindex = 0;

            int Width = 0;
            int Height = 0; ;

            while (xmlReader.Read())
            {
                string name = xmlReader.Name;

                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:

                        string[] attrs = null;

                        if (name == "map")
                        {
                            Width = int.Parse(xmlReader.GetAttribute("width"));
                            Height = int.Parse(xmlReader.GetAttribute("height"));
                        }

                        if (xmlReader.HasAttributes)
                        {
                            attrs = new string[xmlReader.AttributeCount * 2];
                            xmlReader.MoveToFirstAttribute();
                            int i = 0;
                            attrs[0] = xmlReader.Name;
                            attrs[1] = xmlReader.Value;
                            i += 2;

                            while (xmlReader.MoveToNextAttribute())
                            {
                                attrs[i] = xmlReader.Name;
                                attrs[i + 1] = xmlReader.Value;
                                i += 2;
                            }

                            // Move the reader back to the element node.
                            xmlReader.MoveToElement();
                        }
                        startElement(this, name, attrs);

                        byte[] buffer = null;

                        //read data content of tmx file
                        if (name == "data")
                        {
                            if (attrs != null)
                            {
                                string encoding = "";
                                for (int i = 0; i < attrs.Length; i++)
                                {
                                    if (attrs[i] == "encoding")
                                    {
                                        encoding = attrs[i + 1];
                                    }
                                }

                                if (encoding == "base64")
                                {
                                    int dataSize = (Width * Height * 4) + 1024;
                                    buffer = new byte[dataSize];
                                    xmlReader.ReadElementContentAsBase64(buffer, 0, dataSize);
                                }
                                else
                                {
                                    string value = xmlReader.ReadElementContentAsString();
                                    buffer = Encoding.UTF8.GetBytes(value);
                                }
                            }

                            textHandler(this, buffer, buffer.Length);
                            endElement(this, name);
                        }
                        else
                            if (name == "key" || name == "integer" || name == "real" || name == "string")
                            {
                                string value = xmlReader.ReadElementContentAsString();
                                buffer = Encoding.UTF8.GetBytes(value);
                                textHandler(this, buffer, buffer.Length);
                                endElement(this, name);
                            }
                        break;

                    case XmlNodeType.EndElement:
                        endElement(this, xmlReader.Name);
                        dataindex++;
                        break;
                }
            }

            return true;
        }

        public void setDelegator(ICCSAXDelegator pDelegator)
        {
            m_pDelegator = pDelegator;
        }

        public static void startElement(object ctx, string name, string[] atts)
        {
            ((CCSAXParser)(ctx)).m_pDelegator.startElement(ctx, name, atts);
        }

        public static void endElement(object ctx, string name)
        {
            ((CCSAXParser)(ctx)).m_pDelegator.endElement(ctx, name);
        }

        public static void textHandler(object ctx, byte[] ch, int len)
        {
            ((CCSAXParser)(ctx)).m_pDelegator.textHandler(ctx, ch, len);
        }
    }
}
