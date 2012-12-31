using System.Collections.Generic;

namespace MGFramework
{
    public class MGSequence : MGAction
    {
        protected List<MGAction> ActionList;
        protected int Index;

        public static MGSequence Actions(params MGAction[] actions)
        {
            var sequence = new MGSequence();
            sequence.ActionList = new List<MGAction>(actions.Length);
            sequence.ActionList.AddRange(actions);
            sequence._duration = 0f;
            sequence.Index = 0;
            for (int i = 0; i < actions.Length; i++)
            {
                sequence._duration += actions[i].Duration;
            }
            return sequence;
        }

        public override void Step(float dt)
        {
            if (Index < ActionList.Count)
            {
                do
                {
                    ActionList[Index].Step(dt);
                    if (ActionList[Index].IsEnd)
                    {
                        dt = ActionList[Index].Elapsed - ActionList[Index].Duration;
                        Index++;
                        if (Index >= ActionList.Count)
                        {
                            return;
                        }
                        ActionList[Index].SetTarget(Target);
                    }
                    if (ActionList[Index].Duration != 0f)
                    {
                        return;
                    }
                } while (dt >= 0f);
                return;
            }
            _isEnd = true;
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            Index = 0;
            _isEnd = false;
            Target = node;
            if (ActionList.Count > 0)
            {
                ActionList[0].SetTarget(Target);
            }
        }
    }
}