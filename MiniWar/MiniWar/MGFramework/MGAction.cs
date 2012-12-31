using System;
using System.Diagnostics;

namespace MGFramework
{
    public class MGAction
    {
        protected bool FirstTick;
        protected bool IsPause;
        protected MGNode Target;
        protected float _duration;
        protected float _elapsed;
        protected bool _isEnd;

        public MGAction()
        {
            FirstTick = true;
            IsPause = false;
        }

        public bool IsEnd
        {
            get { return _isEnd; }
        }

        public float Duration
        {
            get { return _duration; }
        }

        public float Elapsed
        {
            get { return _elapsed; }
        }

        public virtual void Start()
        {
            SetTarget(Target);
        }

        public virtual void Stop()
        {
            _isEnd = true;
        }

        public virtual void Pause()
        {
            IsPause = true;
        }

        public virtual void Resume()
        {
            IsPause = false;
        }

        public virtual void Update(float time)
        {
        }

        public virtual void Step(float dt)
        {
            if (IsPause)
            {
                return;
            }
            if (FirstTick)
            {
                FirstTick = false;
                _elapsed = 0f;
            }
            else
            {
                _elapsed += dt;
            }
            Update(Math.Min(1f, _elapsed/_duration));
        }

        public void AssignTarget(MGNode node)
        {
            Target = node;
        }

        public virtual void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
        }
    }
}