using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniWar.Card;
using MiniWar.Zombie;

namespace MiniWar
{
    class MainGameLogic
    {
        private static MainGameLogic _sharedLogic;
        public static MainGameLogic SharedMainGameLogic()
        {
            return _sharedLogic ?? (_sharedLogic = new MainGameLogic());
        }

        public bool IsPause { get; set; }
        public List<PaperInfo> PaperInfos;
        private MainGameLogic()
        {
            PaperInfos = new List<PaperInfo>();
        }

        public void GamePause()
        {
            for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
            {
                var actor = ZombieShowLayer.SharedZombieShow().Actors[i];
                actor.Stop();
            }
            IsPause = true;
        }

        public void GameResume()
        {
            for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
            {
                var actor = ZombieShowLayer.SharedZombieShow().Actors[i];
                actor.GoRun();
            }
            IsPause = false;
        }

        public bool IsGameStart = false;
        private float _timeLeft = 2;
        public void UpdateGameLoop(float dt)
        {
            if (IsGameStart)
            {
                if ((_timeLeft -= dt) < 0)
                {
                    _timeLeft = 0;
                    for (int i = 0; i < ZombieShowLayer.SharedZombieShow().Actors.Count; i++)
                    {
                        var actor = ZombieShowLayer.SharedZombieShow().Actors[i];
                        actor.ZOrder = (int)(768 - actor.Position.Y);
                    }
                }
                if (!IsPause)
                {
                    ActorMgr.SharedActorMgr().Update(dt);
                }

              
            }
        }
 
    }
}
