using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public delegate void SelSchedule(float dt);
    public delegate void SelCallFunc();
    public delegate void SelCallFuncN(MGNode sender);
    public delegate void SelCallFuncND(MGNode sender, object data);
    public delegate void SelCallFuncO(Object sender);
    public delegate void SelMenuHandler(Object sender);
    public delegate void SelEventHandler(MGEvent event_);

    public class MGEvent : Object
    {

    }

    public interface ISelectorProtocol
    {
        void Update(float dt);
    }

    public class MGMenuItem : MGNode
    {
        protected bool IsSelected;
        protected bool IsEnabled;
        protected ISelectorProtocol Listener;
        protected SelMenuHandler Selector;
        public MGMenuItem()
        {
            IsSelected = false;
            IsEnabled = false;
            Listener = null;
            Selector = null;
            Anchor = new Vector2(.5f, .5f);
            IsTouchEnable = true;
        }

        public static MGMenuItem ItemWithTarget(ISelectorProtocol rec, SelMenuHandler selector)
        {
            var pRet = new MGMenuItem();
            pRet.InitWithTarget(rec, selector);

            return pRet;
        }

        public bool InitWithTarget(ISelectorProtocol rec, SelMenuHandler selector)
        {

            Listener = rec;
            Selector = selector;
            IsEnabled = true;
            IsSelected = false;
            return true;
        }

        public Rectangle Rect()
        {

            return new Rectangle((int)(Position.X - _anchorPoisiton.X),
                (int)(Position.Y - _anchorPoisiton.Y),
                (int)ContentSize.X,
                (int)ContentSize.Y);

            //return new Rectangle((int)(Position.X),
            // (int)(Position.Y),
            // (int)ContentSize.X,
            // (int)ContentSize.Y);
        }


        public virtual void Activate()
        {
            if (IsEnabled)
            {
                if (Listener != null)
                {
                    //(m_pListener.m_pfnSelector)(this);
                }
                if (Selector != null)
                {
                    Selector(this);
                }
            }
        }




        public virtual void selected()
        {
            IsSelected = true;
        }

        public virtual void unselected()
        {
            IsSelected = false;
        }

        public virtual void SetTarget(ISelectorProtocol rec, SelMenuHandler selector)
        {
            Listener = rec;
            Selector = selector;
        }

        public virtual bool Enabled
        {
            get { return IsEnabled; }
            set { IsEnabled = value; }
        }

        public virtual bool Selected
        {
            get { return IsSelected; }
        }
    }
}
