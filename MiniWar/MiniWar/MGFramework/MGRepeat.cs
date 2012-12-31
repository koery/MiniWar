namespace MGFramework
{
    public class MGRepeat : MGAction
    {
        protected MGAction Action;

        public static MGRepeat Actions(MGAction action, int t)
        {
            return new MGRepeat
                       {
                           Action = action,
                           _duration = t*action.Duration
                       };
        }

        public override void Step(float dt)
        {
            if (FirstTick)
            {
                FirstTick = false;
                _elapsed = 0f;
            }
            else
            {
                _elapsed += dt;
            }
            Action.Step(dt);
            if (Action.IsEnd)
            {
                Action.Start();
            }
            if (_elapsed > _duration)
            {
                _isEnd = true;
            }
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            Action.SetTarget(Target);
        }
    }
}