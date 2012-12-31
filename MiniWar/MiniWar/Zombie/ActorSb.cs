using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using MiniWar.Toobz;
using VSZombie.Toobz;

namespace MiniWar.Zombie
{
    class ActorSb : Actor
    {
        public ActorSb()
        {
            Sprite = MGSprite.MGSpriteWithSpriteFrameName("30010.png");
            _y = Control.SharedControl().GetRandom(0, 5);
            Position = new Vector2(1360, _y * 94 + 57);
            var next = new Random().Next(-10, 10);
            Sprite.Position = new Vector2(0, 50 + next);

            ComeDirection = WireDirection.WireRight;
            GoDirection = WireDirection.WireLeft;
            AddChild(Sprite);
            RunAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(3001)[0]);
            StandAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(3001)[1]);
            DieAni = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(3001)[2]);
            _animate = MGAnimate.ActionWithAnimation(AnimationMgr.ShardAnimationMgr().GetNumZombie(3001)[3]);
            Hp = 2000;
          
        }

        public override void Dead()
        {
            var vectors = GetVector(Vector);
            for (int i = 0; i < vectors.Count; i++)
            {
                Actor actor = new ActorBabe();
                actor.Vector = vectors[i];
                ZombieShowLayer.SharedZombieShow().AddChild(actor);
                actor.SetPosition();
                actor.GoRun();
            }

            
            base.Dead();
        }

        private List<IntVector> GetVector(IntVector vector)
        {
            int x = vector.X;
            int y = vector.Y;
            var vectors = new List<IntVector>();
            if (x + 1 < GameConfig.WidthX)
            {
                vectors.Add(new IntVector(vector.X + 1, vector.Y));
            }
            if (x - 1 > 0)
            {
                vectors.Add(new IntVector(vector.X - 1, vector.Y));
            }
            if (y + 1 < GameConfig.HightY)
            {
                vectors.Add(new IntVector(vector.X, vector.Y + 1));
            }
            if (y - 1 > 0)
            {
                vectors.Add(new IntVector(vector.X, vector.Y - 1));
            }
            vectors.Add(new IntVector(vector.X, vector.Y));
            int i = Control.SharedControl().GetRandom(vectors.Count);
            var value = new List<IntVector>();
            value.Add(vectors[i]);
            vectors.Remove(vectors[i]);
            i = Control.SharedControl().GetRandom(vectors.Count);
            value.Add(vectors[i]);
            return value;
        }
    }
}
