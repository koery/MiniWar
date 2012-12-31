﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public static class MNS
    {
        public static Rectangle RectangleFromString(string pszContent)
        {
            Rectangle result = new Rectangle(0, 0, 0, 0);

            do
            {
                if (pszContent == null)
                {
                    break;
                }

                string content = pszContent;

                // find the first '{' and the third '}'
                int nPosLeft = content.IndexOf('{');
                int nPosRight = content.IndexOf('}');
                for (int i = 1; i < 3; ++i)
                {
                    if (nPosRight == -1)
                    {
                        break;
                    }
                    nPosRight = content.IndexOf('}', nPosRight + 1);
                }
                if (nPosLeft == -1 || nPosRight == -1)
                {
                    break;
                }
                content = content.Substring(nPosLeft + 1, nPosRight - nPosLeft - 1);
                int nPointEnd = content.IndexOf('}');
                if (nPointEnd == -1)
                {
                    break;
                }
                nPointEnd = content.IndexOf(',', nPointEnd);
                if (nPointEnd == -1)
                {
                    break;
                }

                // get the point string and size string
                string pointStr = content.Substring(0, nPointEnd);
                string sizeStr = content.Substring(nPointEnd + 1);
                //, content.Length - nPointEnd
                // split the string with ','
                List<string> pointInfo = new List<string>();

                if (!SplitWithForm(pointStr, pointInfo))
                {
                    break;
                }
                List<string> sizeInfo = new List<string>();
                if (!SplitWithForm(sizeStr, sizeInfo))
                {
                    break;
                }

                float x = float.Parse(pointInfo[0]);
                float y = float.Parse(pointInfo[1]);
                float width = float.Parse(sizeInfo[0]);
                float height = float.Parse(sizeInfo[1]);

                result = new Rectangle((int)x, (int)y, (int)width, (int)height);
            } while (false);

            return result;
        }

        public static Vector2 Vector2FromString(string pszContent)
        {
            Vector2 ret = new Vector2();

            do
            {
                List<string> strs = new List<string>();
                if (!SplitWithForm(pszContent, strs)) break;

                float x = float.Parse(strs[0]);
                float y = float.Parse(strs[1]);

                ret = new Vector2(x, y);
            } while (false);

            return ret;
        }

        public static bool SplitWithForm(string pStr, List<string> strs)
        {
            bool bRet = false;

            do
            {
                if (pStr == null)
                {
                    break;
                }

                string content = pStr;
                if (content.Length == 0)
                {
                    break;
                }

                int nPosLeft = content.IndexOf('{');
                int nPosRight = content.IndexOf('}');

                // don't have '{' and '}'
                if (nPosLeft == -1 || nPosRight == -1)
                {
                    break;
                }
                // '}' is before '{'
                if (nPosLeft > nPosRight)
                {
                    break;
                }

                string pointStr = content.Substring(nPosLeft + 1, nPosRight - nPosLeft - 1);
                // nothing between '{' and '}'
                if (pointStr.Length == 0)
                {
                    break;
                }

                int nPos1 = pointStr.IndexOf('{');
                int nPos2 = pointStr.IndexOf('}');
                // contain '{' or '}' 
                if (nPos1 != -1 || nPos2 != -1) break;

                Split(pointStr, ",", strs);
                if (strs.Count != 2 || strs[0].Length == 0 || strs[1].Length == 0)
                {
                    strs.Clear();
                    break;
                }

                bRet = true;
            } while (false);

            return bRet;
        }

        public static void Split(string src, string token, List<string> vect)
        {
            int nend = 0;
            int nbegin = 0;
            while (nend != -1)
            {
                nend = src.IndexOf(token, nbegin);
                if (nend == -1)
                    vect.Add(src.Substring(nbegin, src.Length - nbegin));
                else
                    vect.Add(src.Substring(nbegin, nend - nbegin));
                nbegin = nend + token.Length;
            }
        }
    }
}
