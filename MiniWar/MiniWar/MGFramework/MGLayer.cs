using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGFramework
{
    public class MGLayer : MGNode
    {
        protected bool CanAddTriggerButton;
        private bool _canAddTriggerButtonInThisFrame;
        protected bool _canClick;
        protected Dictionary<int, MGUIBase> TriggerList;
        protected List<MGUIBase> UIBaseList;
        //public bool IsTouchEnabled { get; set; }
        public MGLayer()
        {
            _color = Color.White;
            UIBaseList = new List<MGUIBase>();
            TriggerList = new Dictionary<int, MGUIBase>();
            _canClick = false;
            CanAddTriggerButton = true;
        }

        public virtual bool CanClick
        {
            get { return _canClick; }
            set
            {
                if (_canClick != value)
                {
                    _canClick = value;
                    RemoveAllTriggerItem();
                }
            }
        }



        public void InitUI()
        {
            foreach (MGNode current in ChildList)
            {
                current.InitUI(this);
            }
        }

        public override void OnSceneActive()
        {
            Anchor = new Vector2(.5f, .5f);
            _canClick = true;
        }

        public void AddUIBase(MGUIBase baseItem)
        {
            UIBaseList.Add(baseItem);
        }

        public bool UIBaseTouchBegan(MouseState touch, Point point, bool isHandle)
        {
            touch.GetHashCode();
            _canAddTriggerButtonInThisFrame = true;
            if (TriggerList.ContainsKey(touch.GetHashCode()))
            {
                MGUIBase uiBase = TriggerList[touch.GetHashCode()];
                if (isHandle)
                {
                    uiBase.TouchesCancel(touch, point);
                    RemoveTriggerItem(touch.GetHashCode(), uiBase);
                }
            }
            else
            {
                if (!CanAddTriggerButton)
                {
                    return isHandle;
                }
                foreach (MGUIBase current in UIBaseList)
                {
                    isHandle = current.TouchesBegan(touch, point);
                    if (isHandle)
                    {
                        AddTriggerItem(touch.GetHashCode(), current);
                        break;
                    }
                }
            }
            return isHandle;
        }

        public bool UIBaseTouchMoved(MouseState touch, Point point, bool isHandle)
        {
            _canAddTriggerButtonInThisFrame = true;
            if (TriggerList.ContainsKey(touch.GetHashCode()))
            {
                MGUIBase uiBase = TriggerList[touch.GetHashCode()];
                if (isHandle)
                {
                    uiBase.TouchesCancel(touch, point);
                    RemoveTriggerItem(touch.GetHashCode(), uiBase);
                }
                else
                {
                    isHandle = uiBase.TouchesMoved(touch, point);
                    if (isHandle)
                    {
                        RemoveTriggerItem(touch.GetHashCode(), uiBase);
                    }
                    isHandle = true;
                }
            }
            else
            {
                if (!CanAddTriggerButton)
                {
                    return isHandle;
                }
                foreach (MGUIBase current in UIBaseList)
                {
                    isHandle = current.TouchesMoved(touch, point);
                    if (isHandle)
                    {
                        AddTriggerItem(touch.GetHashCode(), current);
                        break;
                    }
                }
            }
            return isHandle;
        }

        public bool UIBaseTouchEnded(MouseState touch, Point point, bool isHandle)
        {
            _canAddTriggerButtonInThisFrame = true;
            if (TriggerList.ContainsKey(touch.GetHashCode()))
            {
                MGUIBase uiBase = TriggerList[touch.GetHashCode()];
                if (isHandle)
                {
                    uiBase.TouchesCancel(touch, point);
                    RemoveTriggerItem(touch.GetHashCode(), uiBase);
                }
                else
                {
                    uiBase.TouchesEnded(touch, point);
                }
            }
            return isHandle;
        }

        public void AddTriggerItem(int key, MGUIBase triggerItem)
        {
            if (!_canAddTriggerButtonInThisFrame)
            {
                return;
            }
            TriggerList.Add(key, triggerItem);
            if (TriggerList.Count >= Config.MAX_TRIGGER_BUTTON_COUNT)
            {
                CanAddTriggerButton = false;
                return;
            }
            CanAddTriggerButton = true;
        }

        public void RemoveTriggerItem(int key, MGUIBase triggerItem)
        {
            TriggerList.Remove(key);
            if (TriggerList.Count >= Config.MAX_TRIGGER_BUTTON_COUNT)
            {
                CanAddTriggerButton = false;
                return;
            }
            CanAddTriggerButton = true;
        }

        public void RemoveAllTriggerItem()
        {
            foreach (var current in TriggerList)
            {
                current.Value.TouchesCancel();
            }
            TriggerList.Clear();
            CanAddTriggerButton = true;
            _canAddTriggerButtonInThisFrame = false;
        }
    }
}