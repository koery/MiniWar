using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGFramework
{
    public class MGButton : MGUIBase
    {
        protected Dictionary<string, MGNode> ButtonElement;
        protected IMGButtonDelegate _delegate;
        protected Dictionary<string, MGAction> TouchBeganAction;
        protected Dictionary<string, MGAction> TouchClickedAction;
        protected Dictionary<string, MGAction> TouchMoveOutAction;
        public FrameStruct FS;

        public MGButton()
        {
            ButtonElement = new Dictionary<string, MGNode>();
            TouchBeganAction = new Dictionary<string, MGAction>();
            TouchMoveOutAction = new Dictionary<string, MGAction>();
            TouchClickedAction = new Dictionary<string, MGAction>();
        }

        public IMGButtonDelegate Delegate
        {
            get { return _delegate; }
            set { _delegate = value; }
        }

        public override float Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                _color.A = (byte)_opacity;
                foreach (string current in ButtonElement.Keys)
                {
                    MGNode node = ButtonElement[current];
                    node.Opacity = value;
                }
            }
        }

        public static MGButton ButtonWithoutTexture(float width, float height)
        {
            var button = new MGButton();
            button.InitButtonWithoutTexture(width, height);
            return button;
        }

        public void InitButtonWithoutTexture(float width, float height)
        {
            Width = width;
            Height = height;
            base.Anchor = new Vector2(Width / 2f, Height / 2f);
        }

        public static MGButton ButtonWithTextureTwinkle(string fsName1, string fsName2)
        {
            var button = new MGButton();
            button.InitButtonWithTextureTwinkle(fsName1, fsName2);
            return button;
        }

        public void InitButtonWithTextureTwinkle(string fsName1, string fsName2)
        {
            FS = DataManager.GetFS(fsName1);
            var anim = new MGAnimation("Frame", 0.1f, new[]
                                                          {
                                                              fsName1,
                                                              fsName2
                                                          });
            var sprite = new MGSprite();
            sprite.AddAnimation(anim);
            sprite.SetFrame("Frame", 0);
            ButtonElement.Add("Image", sprite);
            base.AddChild(sprite, 0);
            Width = sprite.Width;
            Height = sprite.Height;
            base.Anchor = new Vector2(Width / 2f, Height / 2f);
            MGAction action = MGSequence.Actions(new MGAction[]
                                                        {
                                                            MGFrameAction.ActionWithAnimationIndex("Frame", 1)
                                                        });
            action.AssignTarget(sprite);
            MGAction action2 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 0)
                                                         });
            action2.AssignTarget(sprite);
            MGAction action3 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGRepeat.Actions(MGAnimate.ActionWithAnimation(anim), 1),
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 0)
                                                         });
            action3.AssignTarget(sprite);
            TouchBeganAction.Add("Image", action);
            TouchMoveOutAction.Add("Image", action2);
            TouchClickedAction.Add("Image", action3);
        }

        public static MGButton ButtonWithFadeoutTwinkle(string fsName1, float opacity)
        {
            var button = new MGButton();
            button.InitButtonWithFadeoutTwinkle(fsName1, opacity);
            return button;
        }

        public void InitButtonWithFadeoutTwinkle(string fsName1, float opacity)
        {
            var sprite = new MGSprite(fsName1);
            ButtonElement.Add("Image", sprite);
            base.AddChild(sprite, 0);
            Width = sprite.Width;
            Height = sprite.Height;
            base.Anchor = new Vector2(Width / 2f, Height / 2f);
            MGAction cDEAction = MGSequence.Actions(new MGAction[]
                                                        {
                                                            MGOpacityAction.ActionWithOpacity(opacity)
                                                        });
            cDEAction.AssignTarget(sprite);
            MGAction cDEAction2 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGOpacityAction.ActionWithOpacity(255f)
                                                         });
            cDEAction2.AssignTarget(sprite);
            MGSequence action = MGSequence.Actions(new MGAction[]
                                                       {
                                                           MGOpacityAction.ActionWithOpacity(255f),
                                                           MGDelay.ActionWithDuration(0.1f),
                                                           MGOpacityAction.ActionWithOpacity(128f),
                                                           MGDelay.ActionWithDuration(0.1f)
                                                       });
            MGAction cDEAction3 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGRepeat.Actions(action, 3),
                                                             new MGShow(),
                                                             MGOpacityAction.ActionWithOpacity(255f)
                                                         });
            cDEAction3.AssignTarget(sprite);
            TouchBeganAction.Add("Image", cDEAction);
            TouchMoveOutAction.Add("Image", cDEAction2);
            TouchClickedAction.Add("Image", cDEAction3);
        }

        public static MGButton ButtonWithToggleTwinkle(string fsName1)
        {
            var button = new MGButton();
            button.InitButtonWithToggleTwinkle(fsName1);
            return button;
        }

        public void InitButtonWithToggleTwinkle(string fsName1)
        {
            var sprite = new MGSprite(fsName1);
            ButtonElement.Add("Image", sprite);
            base.AddChild(sprite, 0);
            Width = sprite.Width;
            Height = sprite.Height;
            base.Anchor = new Vector2(Width / 2f, Height / 2f);
            MGAction cDEAction = MGSequence.Actions(new MGAction[]
                                                        {
                                                            MGOpacityAction.ActionWithOpacity(200f)
                                                        });
            cDEAction.AssignTarget(sprite);
            MGAction cDEAction2 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGOpacityAction.ActionWithOpacity(255f)
                                                         });
            cDEAction2.AssignTarget(sprite);
            MGSequence action = MGSequence.Actions(new MGAction[]
                                                       {
                                                           new MGToggleVisibility(),
                                                           MGDelay.ActionWithDuration(0.1f),
                                                           new MGToggleVisibility(),
                                                           MGDelay.ActionWithDuration(0.1f)
                                                       });
            MGAction cDEAction3 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGOpacityAction.ActionWithOpacity(255f),
                                                             MGRepeat.Actions(action, 1),
                                                             new MGShow(),
                                                             MGOpacityAction.ActionWithOpacity(255f)
                                                         });
            cDEAction3.AssignTarget(sprite);
            TouchBeganAction.Add("Image", cDEAction);
            TouchMoveOutAction.Add("Image", cDEAction2);
            TouchClickedAction.Add("Image", cDEAction3);
        }

        public static MGButton CheckboxWithTexture(string fsName1, string fsName2)
        {
            var button = new MGButton();
            button.InitCheckboxWithPrssingTexture(fsName1, fsName2, "", "");
            return button;
        }

        public static MGButton CheckboxWithPressingTexture(string fsName1, string fsName2, string fsName3,
                                                           string fsName4)
        {
            if (fsName3 == null) throw new ArgumentNullException("fsName3");
            var button = new MGButton();
            button.InitCheckboxWithPrssingTexture(fsName1, fsName2, fsName3, fsName4);
            return button;
        }

        public void InitCheckboxWithPrssingTexture(string fsName1, string fsName2, string fsName3, string fsName4)
        {
            if (fsName3 == "" && fsName4 == "")
            {
                var sprite = new MGSprite(fsName1);
                var sprite2 = new MGSprite(fsName2);
                sprite2.Visible = false;
                base.AddChild(sprite, 0);
                base.AddChild(sprite2, 0);
                ButtonElement.Add("ImageOn", sprite);
                ButtonElement.Add("ImageOff", sprite2);
                Width = sprite.Width;
                Height = sprite.Height;
                base.Anchor = new Vector2(Width / 2f, Height / 2f);
                MGAction cDEAction = MGSequence.Actions(new MGAction[]
                                                            {
                                                                new MGToggleVisibility()
                                                            });
                cDEAction.AssignTarget(sprite);
                TouchClickedAction.Add("ImageOn", cDEAction);
                MGAction cDEAction2 = MGSequence.Actions(new MGAction[]
                                                             {
                                                                 new MGToggleVisibility()
                                                             });
                cDEAction2.AssignTarget(sprite2);
                TouchClickedAction.Add("ImageOff", cDEAction2);
                return;
            }
            var anim = new MGAnimation("Frame", 0.1f, new[]
                                                          {
                                                              fsName1,
                                                              fsName3
                                                          });
            var anim2 = new MGAnimation("Frame", 0.1f, new[]
                                                           {
                                                               fsName2,
                                                               fsName4
                                                           });
            var cDESprite3 = new MGSprite();
            var cDESprite4 = new MGSprite();
            cDESprite3.AddAnimation(anim);
            cDESprite4.AddAnimation(anim2);
            cDESprite3.SetFrame("Frame", 0);
            cDESprite4.SetFrame("Frame", 0);
            base.AddChild(cDESprite3, 0);
            base.AddChild(cDESprite4, 0);
            cDESprite4.Visible = false;
            ButtonElement.Add("ImageOn", cDESprite3);
            ButtonElement.Add("ImageOff", cDESprite4);
            Width = cDESprite3.Width;
            Height = cDESprite3.Height;
            base.Anchor = new Vector2(Width / 2f, Height / 2f);
            MGAction cDEAction3 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 1)
                                                         });
            cDEAction3.AssignTarget(cDESprite3);
            MGAction cDEAction4 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 0)
                                                         });
            cDEAction4.AssignTarget(cDESprite3);
            MGAction cDEAction5 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             new MGToggleVisibility(),
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 0)
                                                         });
            cDEAction5.AssignTarget(cDESprite3);
            TouchBeganAction.Add("ImageOn", cDEAction3);
            TouchMoveOutAction.Add("ImageOn", cDEAction4);
            TouchClickedAction.Add("ImageOn", cDEAction5);
            MGAction cDEAction6 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 1)
                                                         });
            cDEAction6.AssignTarget(cDESprite4);
            MGAction cDEAction7 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 0)
                                                         });
            cDEAction7.AssignTarget(cDESprite4);
            MGAction cDEAction8 = MGSequence.Actions(new MGAction[]
                                                         {
                                                             new MGToggleVisibility(),
                                                             MGFrameAction.ActionWithAnimationIndex("Frame", 0)
                                                         });
            cDEAction8.AssignTarget(cDESprite4);
            TouchBeganAction.Add("ImageOff", cDEAction6);
            TouchMoveOutAction.Add("ImageOff", cDEAction7);
            TouchClickedAction.Add("ImageOff", cDEAction8);
        }

        public void SetClicked(bool isChecked)
        {
            if (ButtonElement.ContainsKey("ImageOn"))
            {
                MGNode cDENode = ButtonElement["ImageOn"];
                cDENode.Visible = isChecked;
            }
            if (ButtonElement.ContainsKey("ImageOff"))
            {
                MGNode cDENode2 = ButtonElement["ImageOff"];
                cDENode2.Visible = !isChecked;
            }
        }

        public bool GetClicked()
        {
            if (ButtonElement.ContainsKey("ImageOn"))
            {
                MGNode cDENode = ButtonElement["ImageOn"];
                return cDENode.Visible;
            }
            if (ButtonElement.ContainsKey("ImageOff"))
            {
                MGNode cDENode2 = ButtonElement["ImageOff"];
                return !cDENode2.Visible;
            }
            return true;
        }

        public override bool TouchesBegan(MouseState touch, Point point)
        {
            if (!Visible || !_enabled)
            {
                return TouchID == touch.GetHashCode() && TouchesCancel(touch, point);
            }
            if (TouchID != -1)
            {
                return true;
            }
            if (base.IsInside(new Vector2(point.X,point.Y)))
            {
                OnTouchBegan();
                TouchID = touch.GetHashCode();
                return true;
            }
            return false;
        }

        public override bool TouchesMoved(MouseState touch, Point point)
        {
            if (!Visible || !_enabled)
            {
                return TouchID == touch.GetHashCode() && TouchesCancel(touch, point);
            }
            if (TouchID == -1)
            {
                if (base.IsInside(new Vector2(point.X, point.Y)))
                {
                    OnTouchBegan();
                    TouchID = touch.GetHashCode();
                    return true;
                }
            }
            else
            {
                if (!base.IsInside(new Vector2(point.X, point.Y)))
                {
                    OnTouchMoveOut();
                    TouchID = -1;
                    return true;
                }
            }
            return false;
        }

        public override bool TouchesEnded(MouseState touch, Point point)
        {
            //if (!_visible || !_enabled)
            //{
            //    return TouchID == touch.Id && TouchesCancel(touch, point);
            //}
            //if (TouchID != -1)
            //{
            //    if (base.IsInside(point))
            //    {
            //        OnTouchRelease();
            //    }
            //    else
            //    {
            //        OnTouchMoveOut();
            //        ((MGLayer)_delegate).RemoveTriggerItem(TouchID, this);
            //        TouchID = -1;
            //    }
            //    return true;
            //}
            return false;
        }

        public override bool TouchesCancel(MouseState touch, Point point)
        {
            //OnTouchMoveOut();
            //TouchID = -1;
            return true;
        }

        public void CallTouchBeganFunc()
        {
            _delegate.ButtonTouchBegan(this);
        }

        public void CallTouchMoveOutFunc()
        {
            _delegate.ButtonMoveOut(this);
        }

        public void CallClickedBeganFunc()
        {
            _delegate.ButtonClickedBegan(this);
        }

        public void CallClickedFunc()
        {
            ((MGLayer)_delegate).RemoveTriggerItem(TouchID, this);
            _delegate.ButtonClicked(this);
            TouchID = -1;
        }

        public void OnTouchBegan()
        {
            CallTouchBeganFunc();
            foreach (string current in ButtonElement.Keys)
            {
                MGNode cDENode = ButtonElement[current];
                cDENode.StopAllAction();
                if (TouchBeganAction.ContainsKey(current))
                {
                    cDENode.RunAction(TouchBeganAction[current]);
                }
            }
        }

        public void OnTouchMoveIn()
        {
            foreach (string current in ButtonElement.Keys)
            {
                MGNode cDENode = ButtonElement[current];
                cDENode.StopAllAction();
                if (TouchBeganAction.ContainsKey(current))
                {
                    cDENode.RunAction(TouchBeganAction[current]);
                }
            }
        }

        public void OnTouchMoveOut()
        {
            foreach (string current in ButtonElement.Keys)
            {
                MGNode cDENode = ButtonElement[current];
                cDENode.StopAllAction();
                if (TouchMoveOutAction.ContainsKey(current))
                {
                    cDENode.RunAction(TouchMoveOutAction[current]);
                }
            }
        }

        public void OnTouchRelease()
        {
            CallClickedBeganFunc();
            float num = 0f;
            foreach (string current in ButtonElement.Keys)
            {
                MGNode cDENode = ButtonElement[current];
                cDENode.StopAllAction();
                if (TouchClickedAction.ContainsKey(current))
                {
                    num = Math.Max(num, TouchClickedAction[current].Duration);
                    cDENode.RunAction(TouchClickedAction[current]);
                }
            }
            if (num > 0f)
            {
                base.RunAction(MGSequence.Actions(new MGAction[]
                                                      {
                                                          MGDelay.ActionWithDuration(num),
                                                          MGCallFunc.ActionWithTarget(CallClickedFunc)
                                                      }));
                return;
            }
            base.RunAction(MGCallFunc.ActionWithTarget(CallClickedFunc));
        }
    }
}