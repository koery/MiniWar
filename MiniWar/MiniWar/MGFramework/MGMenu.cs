using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGFramework
{
    public class MGMenu : MGLayer
    {
        protected Color m_tColor;
        protected byte m_cOpacity;
        public MGMenuItem m_pSelectedItem;
        public MGMenu()
        {
            m_cOpacity = 0;
            m_pSelectedItem = null;
            IsTouchEnable = true;
        }


        public override float Opacity
        {
            get
            {
                return base.Opacity;
            }
            set
            {
                base.Opacity = value;
                for (int i = 0; i < ChildList.Count; i++)
                {
                    var node = ChildList[i];
                    node.Opacity = value;
                }
            }
        }

        public static MGMenu menuWithItems(params MGMenuItem[] item)
        {
            MGMenu pRet = new MGMenu();

            if (pRet != null && pRet.initWithItems(item))
            {
                return pRet;
            }

            return null;
        }

        public bool init()
        {
            return initWithItems(null);
        }

        bool initWithItems(params MGMenuItem[] item)
        {
            //if (base.init())
            //{
            //this.m_bIsTouchEnabled = true;

            // menu in the center of the screen
            Vector2 s = new Vector2(MGDirector.SharedDirector().ScreenWidth, MGDirector.SharedDirector().ScreenHeight);

            //this.m_bIsRelativeAnchorPoint = false;
            //Anchor = new Vector2(0.5f, 0.5f);
            this.ContentSize = s;

            // XXX: in v0.7, winSize should return the visible size
            // XXX: so the bar calculation should be done there
            Rectangle r;
            //CCApplication.sharedApplication().statusBarFrame(out r);

            //ccDeviceOrientation orientation = CCDirector.sharedDirector().deviceOrientation;
            //if (orientation == ccDeviceOrientation.CCDeviceOrientationLandscapeLeft
            //    ||
            //    orientation == ccDeviceOrientation.CCDeviceOrientationLandscapeRight)
            //{
            //    s.height -= r.size.width;
            //}
            //else
            //{
            //    s.height -= r.size.height;
            //}

            //position = new CCPoint(s.width / 2, s.height / 2);

            if (item != null)
            {
                foreach (var menuItem in item)
                {
                    this.AddChild(menuItem);
                }
            }
            //	[self alignItemsVertically];

            m_pSelectedItem = null;
            //m_eState = tCCMenuState.kCCMenuStateWaiting;
            return true;
            //}

            return false;
        }

        public void alignItemsVertically()
        {
            this.alignItemsVerticallyWithPadding(5);
        }

        public void alignItemsVerticallyWithPadding(float padding)
        {
            float height = -padding;

            if (ChildList != null && ChildList.Count > 0)
            {
                foreach (var child in ChildList)
                {
                    if (child != null)
                    {
                        height += child.ContentSize.Y * child.ScaleY + padding;
                    }
                }
            }

            float y = height / 2.0f;
            if (ChildList != null && ChildList.Count > 0)
            {
                foreach (var pChild in ChildList)
                {
                    if (pChild != null)
                    {
                        pChild.Position = new Vector2(0, y - pChild.ContentSize.Y * pChild.ScaleY / 2.0f);
                        y -= pChild.ContentSize.Y * pChild.ScaleY + padding;
                    }
                }
            }
        }

        public void alignItemsHorizontally()
        {
            this.alignItemsHorizontallyWithPadding(5);
        }


        public void alignItemsHorizontallyWithPadding(float padding)
        {
            float width = -padding;

            if (ChildList != null && ChildList.Count > 0)
            {
                foreach (var pChild in ChildList)
                {
                    if (pChild != null)
                    {
                        width += pChild.ContentSize.X * pChild.ScaleX + padding;
                    }
                }
            }

            float x = -width / 2.0f;
            if (ChildList != null && ChildList.Count > 0)
            {
                foreach (var pChild in ChildList)
                {
                    if (pChild != null)
                    {
                        pChild.Position = new Vector2(x + pChild.ContentSize.X * pChild.ScaleX / 2.0f, 0);
                        x += pChild.ContentSize.X * pChild.ScaleX + padding;
                    }
                }
            }
        }

        public override bool TouchesBegan(Microsoft.Xna.Framework.Input.MouseState touch, Point point)
        {
            //if (!CanClick)
            //{
            //    return false;
            //}
            if (State != MGMenuState.MenuStateWaiting || !Visible)
            {
                return false;
            }

            for (MGNode c = this.Parent; c != null; c = c.Parent)
            {
                if (c.Visible == false)
                {
                    return false;
                }
            }

            m_pSelectedItem = this.ItemForTouch(touch);

            if (m_pSelectedItem != null)
            {
                State = MGMenuState.MenuStateTrackingTouch;
                m_pSelectedItem.selected();

                return true;
            }

            return false;
        }

        public override bool TouchesEnded(MouseState touch, Point point)
        {
            //Debug.Assert(State == MGMenuState.MenuStateTrackingTouch, "[Menu ccTouchEnded] -- invalid state");

            if (m_pSelectedItem != null)
            {
                m_pSelectedItem.unselected();
                m_pSelectedItem.Activate();
            }

            State = MGMenuState.MenuStateWaiting;
            return base.TouchesMoved(touch, point);
        }

        public override bool TouchesCancel(MouseState touch, Point point)
        {
            //Debug.Assert(State == MGMenuState.MenuStateTrackingTouch, "[Menu ccTouchCancelled] -- invalid state");

            if (m_pSelectedItem != null)
            {
                m_pSelectedItem.unselected();
            }

            State = MGMenuState.MenuStateWaiting;
            return base.TouchesCancel(touch, point);
        }

        public override bool TouchesMoved(MouseState touch, Point point)
        {
            //Debug.Assert(State == MGMenuState.MenuStateTrackingTouch, "[Menu ccTouchMoved] -- invalid state");

            MGMenuItem currentItem = this.ItemForTouch(touch);

            if (currentItem != m_pSelectedItem)
            {
                if (m_pSelectedItem != null)
                {
                    m_pSelectedItem.unselected();
                }

                m_pSelectedItem = currentItem;

                if (m_pSelectedItem != null)
                {
                    m_pSelectedItem.selected();
                }
            }
            return true;
        }

        public MGMenuItem ItemForTouch(MouseState touch)
        {
            Rectangle _windowsBounds = MGDirector.WindowsBounds;
            var x = touch.X;
            var y = touch.Y;
            Vector2 point = MGDirector.SharedDirector().ConvertToGamePos(new Vector2(x, y));
            var touchLocation = point;
            //touchLocation = MGDirector.SharedDirector().ConvertToGamePos(touchLocation);

            if (ChildList != null && ChildList.Count > 0)
            {
                foreach (var pChild in ChildList)
                {
                    if (pChild != null && pChild.Visible && ((MGMenuItem)pChild).Enabled)
                    {
                        //Vector2 local = pChild.ConvertToNodeSpace(touchLocation);
                        Rectangle r = ((MGMenuItem)pChild).Rect();
                        //r.origin = CCPoint.Zero;
                        var p = Position;
                        r = new Rectangle((int)(p.X + r.X), (int)(p.Y + r.Y), r.Width, r.Height);
                        if (r.Contains((int)touchLocation.X, (int)touchLocation.Y))
                        {
                            return (MGMenuItem)pChild;
                        }
                    }
                }
            }

            return null;
        }


        //public Color Color
        //{
        //    get { throw new NotImplementedException(); }
        //    set { throw new NotImplementedException(); }
        //}

        //public byte Opacity
        //{
        //    get { Opacity }
        //    set { throw new NotImplementedException(); }
        //}



        protected MGMenuState State;
    }

    public enum MGMenuState
    {
        MenuStateWaiting,
        MenuStateTrackingTouch
    };
}
