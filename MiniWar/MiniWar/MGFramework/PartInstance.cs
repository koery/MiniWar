using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class PartInstance
    {
        private readonly MGNode _node;
        private readonly Part _part;
        private float _angleDiff;
        private int _frame;
        private float _opacityDiff;
        private Vector2 _posDiff;
        private float _preFrameTime;
        private float _scaleXDiff;
        private float _scaleYDiff;
        private float _targetAngle;
        private float _targetOpacity;
        private Vector2 _targetPos;
        private float _targetScaleX;
        private float _targetScaleY;
        private float _time;
        private float _timeInterval;
        public int StateIdx;

        public PartInstance(MGNode parent, Part part)
        {
            _part = part;
            StateIdx = -1;
            _frame = 0;
            _time = 0f;
            _preFrameTime = 0f;
            _timeInterval = 0f;
            if (_part.Type == Part.PartType.PartTypeImage)
            {
                _node = new MGSprite(part.Images[0].uniqueID);
                parent.AddChild(_node, part.ZAxis);
                if (part.Images.Count > 1)
                {
                    var animation = new MGAnimation("P", 0.5f);
                    for (int i = 0; i < part.Images.Count; i++)
                    {
                        animation.AddFS(part.Images[i].uniqueID);
                    }
                    ((MGSprite)_node).AddAnimation(animation);
                    return;
                }
            }
            else
            {
                if (_part.Type == Part.PartType.PartTypeButton)
                {
                    if (part.Images.Count > 1)
                    {
                        _node = MGButton.ButtonWithTextureTwinkle(part.Images[0].uniqueID, part.Images[1].uniqueID);
                    }
                    else
                    {
                        _node = MGButton.ButtonWithToggleTwinkle(part.Images[0].uniqueID);
                    }
                    if ((_node).Width <= 30f || (_node).Height <= 30f)
                    {
                        ((MGUIBase)_node).Bold = 10f;
                    }
                    parent.AddChild(_node, part.ZAxis);
                    return;
                }
                if (_part.Type == Part.PartType.PartTypeCheckBox)
                {
                    FrameStruct frameStruct = FrameStruct.FrameStructWithImage(part.Images[0]);
                    if (part.Images.Count > 1)
                    {
                        _node = MGButton.CheckboxWithTexture(part.Images[0].uniqueID, part.Images[1].uniqueID);
                        parent.AddChild(_node, part.ZAxis);
                    }
                    if (frameStruct.Width <= 30f || frameStruct.Height <= 30f)
                    {
                        ((MGUIBase)_node).Bold = 10f;
                        return;
                    }
                }
                else
                {
                    if (_part.Type == Part.PartType.PartTypeProgressBar)
                    {
                        _node = new MGProgressBar(part.Images[0].uniqueID);
                        string[] array = _part.UniqueID.Split(new[]
                                                                  {
                                                                      '_'
                                                                  });
                        if (array[1] == "L")
                        {
                            ((MGProgressBar)_node).SetType(MGProgressBar.BarType.ProgressBarLeft);
                        }
                        else
                        {
                            ((MGProgressBar)_node).SetType(MGProgressBar.BarType.ProgressBarRight);
                        }
                        ((MGProgressBar)_node).Percent = 0.5;
                        parent.AddChild(_node, part.ZAxis);
                        return;
                    }
                    if (_part.Type == Part.PartType.PartTypeLabelAtlas)
                    {
                        string[] array2 = _part.UniqueID.Split(new[]
                                                                   {
                                                                       '_'
                                                                   });
                        MGLabelAtlas.LabelType type;
                        if (array2[3] == "L")
                        {
                            type = MGLabelAtlas.LabelType.TextAlignmentLeft;
                        }
                        else
                        {
                            if (array2[3] == "C")
                            {
                                type = MGLabelAtlas.LabelType.TextAlignmentCenter;
                            }
                            else
                            {
                                type = MGLabelAtlas.LabelType.TextAlignmentRight;
                            }
                        }
                        int charWidth = Convert.ToInt32(array2[1]);
                        int num = Convert.ToInt32(array2[2]);
                        FrameStruct frameStruct2 = FrameStruct.FrameStructWithImage(part.Images[0]);
                        var itemWidth = (int)(frameStruct2.Width / num);
                        var itemHeight = (int)frameStruct2.Height;
                        _node = new MGLabelAtlas(part.Images[0].uniqueID, type, "000", '0', itemWidth, itemHeight,
                                                 charWidth);
                        parent.AddChild(_node, part.ZAxis);
                        return;
                    }
                    if (_part.Type == Part.PartType.PartTypeLabel)
                    {
                        string[] array3 = _part.UniqueID.Split(new[]
                                                                   {
                                                                       '_'
                                                                   });
                        MGLabel.LabelType type2;
                        if (array3[5] == "L")
                        {
                            type2 = MGLabel.LabelType.TextAlignmentLeft;
                        }
                        else
                        {
                            if (array3[5] == "C")
                            {
                                type2 = MGLabel.LabelType.TextAlignmentCenter;
                            }
                            else
                            {
                                type2 = MGLabel.LabelType.TextAlignmentRight;
                            }
                        }
                        Convert.ToInt32(array3[4]);
                        Convert.ToInt32(array3[3]);
                        Convert.ToInt32(array3[2]);
                        string arg_44B_0 = array3[1];
                        _node = new MGLabel(part.Content, type2,
                                            MGDirector.SharedDirector().Content.Load<SpriteFont>("SpriteFont1"));
                        parent.AddChild(_node, part.ZAxis);
                    }
                }
            }
        }

        public void Update(float dt)
        {
            _time += dt;
            _node.Position = _node.Position + _posDiff * dt;
            _node.Rotation += _angleDiff * dt;
            _node.Opacity += _opacityDiff * dt;
            _node.ScaleX += _scaleXDiff * dt;
            _node.ScaleY += _scaleYDiff * dt;
            if (_time > _timeInterval)
            {
                _node.Position = _targetPos;
                _node.Rotation = _targetAngle;
                _node.Opacity = _targetOpacity;
                _node.ScaleX = _targetScaleX;
                _node.ScaleY = _targetScaleY;
            }
        }

        public string GetName()
        {
            return _part.UniqueID;
        }

        public void ResetTime()
        {
            _preFrameTime = 0f;
            _time = 0f;
            _timeInterval = 0f;
        }

        public void SetTarget(float time, Vector2 pos, float rot, float opacity, Vector2 scale)
        {
            if (_time > _timeInterval)
            {
                _time -= _timeInterval;
            }
            _timeInterval = time - _preFrameTime;
            _preFrameTime = time;
            _posDiff = (pos - _node.Position) * (1f / _timeInterval);
            _targetPos = pos;
            _angleDiff = (rot - _node.Rotation) / _timeInterval;
            _targetAngle = rot;
            if (Math.Abs(rot + 360f - _node.Rotation) / _timeInterval < Math.Abs(_angleDiff))
            {
                _angleDiff = (rot + 360f - _node.Rotation) / _timeInterval;
            }
            else
            {
                if (Math.Abs(rot - 360f - _node.Rotation) / _timeInterval < Math.Abs(_angleDiff))
                {
                    _angleDiff = (rot - 360f - _node.Rotation) / _timeInterval;
                }
            }
            _opacityDiff = (opacity - _node.Opacity) / _timeInterval;
            _targetOpacity = opacity;
            _scaleXDiff = (scale.X - _node.ScaleX) / _timeInterval;
            _targetScaleX = scale.X;
            _scaleYDiff = (scale.Y - _node.ScaleY) / _timeInterval;
            _targetScaleY = scale.Y;
        }

        public MGNode GetNode()
        {
            return _node;
        }

        public void SetFrame(int frame)
        {
            if (_frame != frame)
            {
                _frame = frame;
                ((MGSprite)_node).SetFrame("P", _frame);
            }
        }
    }
}