using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGAnimation
    {
        private readonly List<FrameStruct> _fsList;
        private readonly List<float> _timeList;
        protected float _duration;
        protected string _name;
        protected float TotalDuration;


        protected string m_nameStr;
        protected float m_fDelay;
        protected List<MGSpriteFrame> m_pobFrames;

        public MGAnimation()
        {
            m_pobFrames = new List<MGSpriteFrame>();


            _fsList = new List<FrameStruct>();
            _timeList = new List<float>();
            TotalDuration = 0f;
        }

        public MGAnimation(string aName, float duration)
        {
            _fsList = new List<FrameStruct>();
            _timeList = new List<float>();
            TotalDuration = 0f;
            _name = aName;
            _duration = duration;
        }

        public MGAnimation(string aName, float duration, params string[] nameList)
        {
            _fsList = new List<FrameStruct>();
            _timeList = new List<float>();
            _name = aName;
            _duration = duration;
            for (int i = 0; i < nameList.Length; i++)
            {
                AddFS(nameList[i]);
            }
        }
        public static MGAnimation animationWithFrames(List<MGSpriteFrame> frames, float delay)
        {
            MGAnimation pAnimation = new MGAnimation();
            pAnimation.initWithFrames(frames, delay);

            return pAnimation;
        }


        public static MGAnimation animationWithFrames(List<MGSpriteFrame> frames)
        {
            MGAnimation pAnimation = new MGAnimation();
            pAnimation.initWithFrames(frames);

            return pAnimation;
        }
        public bool initWithFrames(List<MGSpriteFrame> pFrames)
        {
            return initWithFrames(pFrames, .2f);
        }

        public bool initWithFrames(List<MGSpriteFrame> pFrames, float delay)
        {
            m_fDelay = delay;
            m_pobFrames = pFrames;
            _name = "1";
            foreach (var frame in pFrames)
            {
                var fs = new FrameStruct();
                fs.Anchor = new Vector2(0.5f, .5f);
                fs.Height = frame.Rect.Height;
                fs.Width = frame.Rect.Width;
                fs.Texture = frame.Texture;
                fs.TextCoords = new Vector2(frame.Rect.X, frame.Rect.Y);
                _fsList.Add(fs);
                _timeList.Add(.2f);
                TotalDuration += 0.2f;
            }
            _duration = TotalDuration;
            return true;
        }

        public float Duration
        {
            get { return _duration; }
        }

        public string Name
        {
            get { return _name; }
        }

        public void AddFS(string fs)
        {
            AddFS(fs, _duration);
        }

        public void AddFS(string fs, float time)
        {
            FrameStruct fS = DataManager.GetFS(fs);
            _fsList.Add(fS);
            _timeList.Add(time);
            TotalDuration += time;
        }

        public void SetAnimationTime(float totalTime)
        {
            int count = _timeList.Count;
            if (count > 0)
            {
                float item = totalTime / count;
                _timeList.Clear();
                for (int i = 0; i < count; i++)
                {
                    _timeList.Add(item);
                }
            }
        }

        public FrameStruct GetFrame(int index)
        {
            return _fsList[index];
        }

        public FrameStruct GetFrameByTime(float time)
        {
            if (time == 0f)
            {
                return _fsList[0];
            }
            time *= TotalDuration;
            for (int i = 0; i < _timeList.Count; i++)
            {
                float num = _timeList[i];
                if (time <= num)
                {
                    return _fsList[i];
                }
                time -= num;
            }
            return _fsList[_fsList.Count - 1];
        }

        public float GetFrameTime(int index)
        {
            return _timeList[index];
        }

        public int FrameCount()
        {
            return _fsList.Count;
        }
    }
}