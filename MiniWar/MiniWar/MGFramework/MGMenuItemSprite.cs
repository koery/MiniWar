using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public interface ICCRGBAProtocol
    {
        Color Color { get; set; }
        byte Opacity { get; set; }
    }

    public class MGMenuItemSprite : MGMenuItem
    {
        #region Images

        MGNode m_pNormalImage;
        public MGNode NormalImage
        {
            get
            {
                return m_pNormalImage;
            }
            set
            {
                if (value != null)
                {
                    AddChild(value);
                    //value.Anchor = new Vector2(0, 0);
                    value.Visible = true;
                }

                if (m_pNormalImage != null)
                {
                    RemoveChild(m_pNormalImage);
                }

                m_pNormalImage = value;
            }
        }

        MGNode m_pSelectedImage;
        public MGNode SelectedImage
        {
            get
            {
                return m_pSelectedImage;
            }
            set
            {
                if (value != null)
                {
                    AddChild(value);
                    //value.Anchor = new Vector2(0, 0);
                    value.Visible = false;
                }

                if (m_pSelectedImage != null)
                {
                    RemoveChild(m_pSelectedImage);
                }

                m_pSelectedImage = value;
            }
        }

        MGNode m_pDisabledImage;
        public MGNode DisabledImage
        {
            get
            {
                return m_pDisabledImage;
            }
            set
            {
                if (value != null)
                {
                    AddChild(value);
                    //value.Anchor = new Vector2(0, 0);
                    value.Visible = false;
                }

                if (m_pDisabledImage != null)
                {
                    RemoveChild(m_pDisabledImage);
                }

                m_pDisabledImage = value;
            }
        }

        #endregion

        public MGMenuItemSprite()
            : base()
        {
            m_pNormalImage = null;
            m_pSelectedImage = null;
            m_pDisabledImage = null;
            //Anchor = new Vector2(0.5f, 0.5f);
        }

        public static MGMenuItemSprite itemFromNormalSprite(MGNode normalSprite, MGNode selectedSprite)
        {
            return itemFromNormalSprite(normalSprite, selectedSprite, null, null, null);
        }

        public static MGMenuItemSprite itemFromNormalSprite(MGNode normalSprite, MGNode selectedSprite,
                                                      ISelectorProtocol target, SelMenuHandler selector)
        {
            return itemFromNormalSprite(normalSprite, selectedSprite, null, target, selector);
        }

        public static MGMenuItemSprite itemFromNormalSprite(MGNode normalSprite, MGNode selectedSprite, MGNode disabledSprite,
                                                   ISelectorProtocol target, SelMenuHandler selector)
        {
            MGMenuItemSprite pRet = new MGMenuItemSprite();
            pRet.initFromNormalSprite(normalSprite, selectedSprite, disabledSprite, target, selector);
            return pRet;
        }

        public bool initFromNormalSprite(MGNode normalSprite, MGNode selectedSprite, MGNode disabledSprite,
                                  ISelectorProtocol target, SelMenuHandler selector)
        {
            if (normalSprite == null)
            {
                throw new ArgumentNullException("normalSprite");
            }

            InitWithTarget(target, selector);

            NormalImage = normalSprite;
            SelectedImage = selectedSprite;
            DisabledImage = disabledSprite;

            ContentSize = m_pNormalImage.ContentSize;

            return true;
        }

        public Color Color
        {
            get { return (m_pNormalImage as ICCRGBAProtocol).Color; }
            set
            {
                (m_pNormalImage as ICCRGBAProtocol).Color = value;

                if (m_pSelectedImage != null)
                {
                    (m_pSelectedImage as ICCRGBAProtocol).Color = value;
                }

                if (m_pDisabledImage != null)
                {
                    (m_pDisabledImage as ICCRGBAProtocol).Color = value;
                }
            }
        }


        //public byte Opacity
        //{
        //    get
        //    {
        //        return (m_pNormalImage as ICCRGBAProtocol).Opacity;
        //    }
        //    set
        //    {
        //        (m_pNormalImage as ICCRGBAProtocol).Opacity = value;

        //        if (m_pSelectedImage != null)
        //        {
        //            (m_pSelectedImage as ICCRGBAProtocol).Opacity = value;
        //        }

        //        if (m_pDisabledImage != null)
        //        {
        //            (m_pDisabledImage as ICCRGBAProtocol).Opacity = value;
        //        }
        //    }
        //}


        public override float Opacity
        {
            get
            {
                return (m_pNormalImage as ICCRGBAProtocol).Opacity;
            }
            set
            {
                m_pNormalImage.Opacity = value;

                if (m_pSelectedImage != null)
                {
                    m_pSelectedImage.Opacity = value;
                }

                if (m_pDisabledImage != null)
                {
                    m_pDisabledImage.Opacity = value;
                }
            }
        }





        public override void selected()
        {
            base.selected();

            if (m_pDisabledImage != null)
            {
                m_pDisabledImage.Visible = false;
            }

            if (m_pSelectedImage != null)
            {
                m_pNormalImage.Visible = false;
                m_pSelectedImage.Visible = true;
            }
            else
            {
                m_pNormalImage.Visible = true;
            }
        }

        public override void unselected()
        {
            base.unselected();

            m_pNormalImage.Visible = true;

            if (m_pSelectedImage != null)
            {
                m_pSelectedImage.Visible = false;
            }

            if (m_pDisabledImage != null)
            {
                m_pDisabledImage.Visible = false;
            }
        }

        public override bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;

                if (m_pSelectedImage != null)
                {
                    m_pSelectedImage.Visible = false;
                }

                if (value)
                {
                    m_pNormalImage.Visible = true;

                    if (m_pDisabledImage != null)
                    {
                        m_pDisabledImage.Visible = false;
                    }
                }
                else
                {
                    if (m_pDisabledImage != null)
                    {
                        m_pDisabledImage.Visible = true;
                        m_pNormalImage.Visible = false;
                    }
                    else
                    {
                        m_pNormalImage.Visible = true;
                    }
                }
            }
        }
    }
}
