using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWar.Zombie
{
    class ActorLv
    {

        public static Type GetRandomActorType(int lv)
        {
            int i = Control.SharedControl().GetRandom(9999);
            List<Type> types = new List<Type>();
            types.Clear();
            if (lv == 1)
            {
                types.Add(typeof(ActorBabe));
                types.Add(typeof(ActorNormal));
                i = i % 2;
            }
            else if (lv == 2)
            {
                types.Add(typeof(ActorBabe));
                types.Add(typeof(ActorNormal));
                types.Add(typeof(ActorLr));
                i = i % 3;
            }
            else if (lv == 3)
            {
                types.Add(typeof(ActorBabe));
                types.Add(typeof(ActorNormal));
                types.Add(typeof(ActorLr));
                types.Add(typeof(ActorFast));
                i = i % 4;
            }
            return types[i];
        }

        public static Type GetJilv(int lv, int index)
        {
            switch (lv)
            {
                case 2:
                    switch (index)
                    {
                        case 1:
                            return GetRaodomTypePercentage(new float[] { 30, 50, 20 });
                        case 2:
                            return GetRaodomTypePercentage(new float[] { 25, 55, 20 });
                        case 3:
                            return GetRaodomTypePercentage(new float[] { 20, 60, 20 });
                        case 4:
                            return GetRaodomTypePercentage(new float[] { 20, 55, 25 });
                        case 5:
                            return GetRaodomTypePercentage(new float[] { 20, 50, 30 });
                    }
                    break;

                case 3:
                    switch (index)
                    {
                        case 1:
                            return GetRaodomTypePercentage(new float[] { 15, 40, 30, 15 });
                        case 2:
                            return GetRaodomTypePercentage(new float[] { 15, 37.5f, 30, 17.5f });
                        case 3:
                            return GetRaodomTypePercentage(new float[] { 15, 40, 25, 20 });
                        case 4:
                            return GetRaodomTypePercentage(new float[] { 15, 37.5f, 27.5f, 20 });
                        case 5:
                            return GetRaodomTypePercentage(new float[] { 15, 37.5f, 22.5f, 25 });
                    }
                    break;
            }
            return typeof(ActorNormal);
        }

        private static Type GetRaodomTypePercentage(float[] p)
        {
            float i = 0;
            for (int j = 0; j < p.Length; j++)
            {
                i += p[j];
            }
            if (i != 100)
            {
                throw new Exception("总值不是100");
            }
            i = Control.SharedControl().GetRandom(9999);
            i = i % 100;
            if (p.Length == 2)
            {
                if (i < p[0])
                {
                    return typeof(ActorBabe);
                }
                if (i >= p[0] && i < p[0] + p[1])
                {
                    return typeof(ActorNormal);
                }
            }
            else if (p.Length == 3)
            {
                if (i < p[0])
                {
                    return typeof(ActorBabe);
                }
                if (i >= p[0] && i < p[0] + p[1])
                {
                    return typeof(ActorNormal);
                }
                if (i >= p[0] + p[1] && i < p[0] + p[1] + p[2])
                {
                    return typeof(ActorLr);
                }
            }
            else if (p.Length == 4)
            {
                if (i < p[0])
                {
                    return typeof(ActorBabe);
                }
                if (i >= p[0] && i < p[0] + p[1])
                {
                    return typeof(ActorNormal);
                }
                if (i >= p[0] + p[1] && i < p[0] + p[1] + p[2])
                {
                    return typeof(ActorLr);
                }
                if (i > p[0] + p[1] + p[2] && i < p[0] + p[1] + p[2] + p[3])
                {
                    return typeof(ActorFast);
                }
            }
            return null;
        }

        public static int TotalActorCount { get; private set; }
        public static int SceneActorCount { get; set; }
        public static int Lv = 3;
        public static int Index = 5;
        private static float[] _times = new[] { 40 / 2f, 40 / 3f, 40 / 4f };
        private static float _timeLeft1 = 10;
        public static bool _isEnd;
        private static bool _isStart;
        public static void Update(float dt)
        {
            if (_isEnd || Lv == 1 && SceneActorCount >= 3 || Lv == 2 && SceneActorCount >= 4 || Lv == 3 && SceneActorCount >= 5)
                return;
            if (_isStart)
            {
                if (SceneActorCount == 0)
                {
                    _timeLeft1 = _times[Lv - 1];
                }
            }
            if ((_timeLeft1 += dt) > _times[Lv - 1])
            {
                Type type = GetJilv(Lv, Index);
                Actor actor = null;
                switch (type.Name)
                {
                    case "ActorBabe":
                        actor = new ActorBabe();
                        break;
                    case "ActorFast":
                        actor = new ActorFast();
                        break;
                    case "ActorNormal":
                        actor = new ActorNormal();
                        break;
                    case "ActorLr":
                        actor = new ActorLr();
                        break;
                }
                if (actor != null)
                {
                    TotalActorCount++;
                    _timeLeft1 = 0;
                    ZombieShowLayer.SharedZombieShow().AddChild(actor);
                    actor.Run();
                    _isStart = true;
                }
            }
            if (TotalActorCount > 99)
            {
                var actor = new ActorSb();
                ZombieShowLayer.SharedZombieShow().AddChild(actor);
                actor.Run();
                _isEnd = true;
            }
        }
    }
}
