using System;
using System.Collections.Generic;

namespace MGFramework
{
    public class MGSpawn : MGAction
    {
        protected List<MGAction> ActionList;

        public static MGSpawn Actions(params MGAction[] actions)
        {
            var spawn = new MGSpawn();
            spawn.ActionList = new List<MGAction>(actions.Length);
            spawn.ActionList.AddRange(actions);
            spawn._duration = 0f;
            for (int i = 0; i < actions.Length; i++)
            {
                spawn._duration = Math.Max(spawn._duration, actions[i].Duration);
            }
            return spawn;
        }

        public override void Step(float dt)
        {
            bool flag = true;
            for (int i = 0; i < ActionList.Count; i++)
            {
                if (!ActionList[i].IsEnd)
                {
                    ActionList[i].Step(dt);
                    flag = false;
                }
            }
            if (flag)
            {
                _isEnd = true;
            }
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            foreach (MGAction current in ActionList)
            {
                current.SetTarget(Target);
            }
        }
    }
}