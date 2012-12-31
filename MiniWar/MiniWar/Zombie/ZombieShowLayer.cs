using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using MiniWar.Toobz;

namespace MiniWar.Zombie
{
    class ZombieShowLayer : MGLayer
    {
        private static ZombieShowLayer _sharedZombieShow;
        public static ZombieShowLayer SharedZombieShow()
        {
            if (_sharedZombieShow == null)
            {
                _sharedZombieShow = new ZombieShowLayer();
            }
            return _sharedZombieShow;
        }

        public List<Actor> Actors;

        private ZombieShowLayer()
        {
            Actors = new List<Actor>();
        }

        public void AddChild(Actor actor)
        {
            ActorLv.SceneActorCount++;
            AddChild(actor, 10);
            Actors.Add(actor);
        }

        public void RemoveChild(Actor actor)
        {
            ActorLv.SceneActorCount--;
            RemoveChild(actor.Sprite);
            Actors.Remove(actor);
        }

        public void Init()
        {
            WireShowLayer.SharedWireShow().ThunderCallBackStart = new ThunderCallBack(Thunder);
        }

        public void Thunder(object wireShow)
        {
            var showLayer = (wireShow as WireShowLayer);
            for (int i = 0; i < showLayer.Clears.Count; i++)
            {
                for (int j = 0; j < Actors.Count; j++)
                {
                    var actor = Actors[j];
                    if (actor.Position.X > GameConfig.WidthX * GameConfig.GirdSize + GameConfig.RelativeOrigin.X)
                    {
                        continue;
                    }
                    if (actor.Guid == showLayer.Guid)
                    {
                        continue;
                    }
                    if (actor.AtVector == showLayer.Clears[i].Vector)
                    {
                        actor.Guid = showLayer.Guid;
                        actor.ChangeHp(1000);
                    }
                    else if (actor.Vector == showLayer.Clears[i].Vector)
                    {
                        actor.Guid = showLayer.Guid;
                        actor.ChangeHp(1000);
                    }
                }
            }
        }
    }
}
