using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWar.Zombie
{
    class ActorMgr
    {
        private static ActorMgr _sharedActorMgr;
        public static ActorMgr SharedActorMgr()
        {
            if (_sharedActorMgr == null)
            {
                _sharedActorMgr = new ActorMgr();
            }
            return _sharedActorMgr;
        }

        private float _timeLeft = 4;
        private bool _gameStart = false;
        public void Update(float dt)
        {
            ActorLv.Update(dt);
            //if (_gameStart)
            //{
            //    if (ZombieShowLayer.SharedZombieShow().Actors.Count == 0)
            //    {
            //        _timeLeft = 0;
            //    }
            //}
            //if ((_timeLeft -= dt) < 0)
            //{
            //    if (!_gameStart)
            //    {
            //        _gameStart = true;
            //    }
            //    _timeLeft = 17;
            //    Type type = ActorLv.GetJilv(3,3);
            //    Actor actor = null;
            //    switch (type.Name)
            //    {
            //        case "ActorBabe":
            //            actor = new ActorBabe();
            //            break;
            //        case "ActorFast":
            //            actor = new ActorFast();
            //            break;
            //        case "ActorNormal":
            //            actor = new ActorNormal();
            //            break;
            //        case "ActorLr":
            //            actor = new ActorLr();
            //            break;

            //    }
            //    if (actor != null)
            //    {
            //        ZombieShowLayer.SharedZombieShow().AddChild(actor);
            //        actor.Run();
            //    }

            //}
        }
    }
}
