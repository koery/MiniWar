namespace MGFramework
{
    public class MGRepeatForever : MGAction
    {
        protected MGAction Action;

        public static MGRepeatForever Actions(MGAction action)
        {
            return new MGRepeatForever
                       {
                           Action = action
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
                _elapsed = 0f;
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