using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MObjectInstance
    {
        #region Delegates

        public delegate void Callback();

        #endregion

        private const float BLENDINGTIME = 0.06666f;
        private readonly MGNode _node;
        private readonly List<PartInstance> _partInstances;
        protected Callback AnimationEndCallback;
        private int _animationFrame;
        private bool _blending;
        private bool _end;
        protected Callback EventCallback;
        private bool _inited;
        private float _interval;
        private bool _loop;
        private float _time;
        private MAnimation _animation;

        public MObjectInstance(string objectName)
        {
            _node = new MGNode();
            MObject mObject = DataManager.ObjectByKey(objectName);
            _partInstances = new List<PartInstance>();
            for (int i = 0; i < mObject.Parts.Count; i++)
            {
                Part part = mObject.Parts[i];
                var item = new PartInstance(_node, part);
                _partInstances.Add(item);
            }
            _time = 0f;
            _interval = 0f;
            _inited = false;
            _blending = true;
            AnimationEndCallback = null;
            EventCallback = null;
        }

        public MGNode GetNode()
        {
            return _node;
        }

        public void SetAnimation(string animationName, bool loop, bool blending)
        {
            _end = false;
            _loop = loop;
            _animation = DataManager.AnimationByKey(animationName);
            if (!_inited)
            {
                _inited = true;
                _animationFrame = 0;
                MFrame mFrame = _animation.Frames[0];
                for (int i = 0; i < _partInstances.Count; i++)
                {
                    PartInstance partInstance = _partInstances[i];
                    partInstance.StateIdx = -1;
                    for (int j = 0; j < mFrame.PartStates.Count; j++)
                    {
                        PartState partState = mFrame.PartStates[j];
                        if (partInstance.GetName() == partState.UniqueID)
                        {
                            partInstance.StateIdx = j;
                            partInstance.ResetTime();
                            MGNode node = partInstance.GetNode();
                            node.Position = partState.Pos;
                            node.Rotation = partState.Angle;
                            node.Opacity = partState.Opacity;
                            node.Scale = new Vector2(partState.ScaleX, partState.ScaleY);
                        }
                    }
                }
                AnimationNextFrame();
                return;
            }
            MFrame mFrame2 = _animation.Frames[0];
            for (int k = 0; k < _partInstances.Count; k++)
            {
                PartInstance partInstance2 = _partInstances[k];
                partInstance2.StateIdx = -1;
                for (int l = 0; l < mFrame2.PartStates.Count; l++)
                {
                    PartState partState2 = mFrame2.PartStates[l];
                    if (partInstance2.GetName() == partState2.UniqueID)
                    {
                        partInstance2.StateIdx = l;
                    }
                }
            }
            if (blending)
            {
                _blending = true;
                _time = 0f;
                _animationFrame = -1;
                for (int m = 0; m < _partInstances.Count; m++)
                {
                    PartInstance partInstance3 = _partInstances[m];
                    partInstance3.ResetTime();
                }
                AnimationNextFrame();
                return;
            }
            _blending = false;
            _time = 0f;
            _animationFrame = 0;
            for (int n = 0; n < _partInstances.Count; n++)
            {
                PartInstance partInstance4 = _partInstances[n];
                partInstance4.ResetTime();
            }
            AnimationNextFrame();
        }

        private void SetPartFrame()
        {
            MFrame mFrame = _animation.Frames[_animationFrame];
            if (mFrame.EventFunc != null && EventCallback != null)
            {
                EventCallback();
            }
            for (int i = 0; i < mFrame.PartStates.Count; i++)
            {
                PartState partState = mFrame.PartStates[i];
                if (partState.Frame != -1)
                {
                    for (int j = 0; j < _partInstances.Count; j++)
                    {
                        PartInstance partInstance = _partInstances[j];
                        if (partInstance.GetName() == partState.UniqueID)
                        {
                            partInstance.SetFrame(partState.Frame);
                        }
                    }
                }
            }
        }

        private void ResetAnimation()
        {
            _time = 0f;
            _animationFrame = 0;
            for (int i = 0; i < _partInstances.Count; i++)
            {
                PartInstance partInstance = _partInstances[i];
                partInstance.ResetTime();
            }
            SetPartFrame();
            _animationFrame = 1;
        }

        private void AnimationNextFrame()
        {
            if (_animationFrame != -1)
            {
                SetPartFrame();
            }
            _animationFrame++;
            if (_animationFrame >= _animation.Frames.Count)
            {
                if (!_loop)
                {
                    _animationFrame = _animation.Frames.Count - 1;
                    if (!_end)
                    {
                        if (AnimationEndCallback != null)
                        {
                            AnimationEndCallback();
                        }
                        _end = true;
                    }
                    return;
                }
                ResetAnimation();
            }
            if (_blending && _animationFrame == 1)
            {
                _blending = false;
                ResetAnimation();
            }
            MFrame mFrame = _animation.Frames[_animationFrame];
            if (_blending && _animationFrame == 0)
            {
                _interval = 0.06666f;
            }
            else
            {
                _interval = mFrame.Time;
            }
            for (int i = 0; i < _partInstances.Count; i++)
            {
                PartInstance partInstance = _partInstances[i];
                if (partInstance.StateIdx != -1)
                {
                    PartState partState = mFrame.PartStates[partInstance.StateIdx];
                    partInstance.SetTarget(_interval, partState.Pos, partState.Angle, partState.Opacity,
                                           new Vector2(partState.ScaleX, partState.ScaleY));
                }
            }
        }

        public void SetAnimationEndCallback(Callback c)
        {
            AnimationEndCallback = c;
        }

        public void SetEventCallback(Callback c)
        {
            EventCallback = c;
        }

        public void Update(float dt)
        {
            if (_end && !_loop)
            {
                return;
            }
            _time += dt;
            for (int i = 0; i < _partInstances.Count; i++)
            {
                PartInstance partInstance = _partInstances[i];
                partInstance.Update(dt);
            }
            if (_time > _interval)
            {
                AnimationNextFrame();
            }
        }

        public void Flip()
        {
            if (_node.Scale.X > 0f)
            {
                _node.ScaleX = -1f;
                return;
            }
            _node.ScaleX = 1f;
        }

        public MGNode GetPartNode(string partName)
        {
            for (int i = 0; i < _partInstances.Count; i++)
            {
                if (_partInstances[i].GetName() == partName)
                {
                    return _partInstances[i].GetNode();
                }
            }
            return null;
        }
    }
}