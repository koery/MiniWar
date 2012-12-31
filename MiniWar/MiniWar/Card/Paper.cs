using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using VSZombie.Toobz;

namespace MiniWar.Card
{
    class Paper
    {
        public MGSprite Sprite;
        public IntVector Vector { get; protected set; }
        public virtual void Launch(float dt)
        {

        }

        public virtual void Detonate()
        {
        }

        public virtual void SetVector(int x, int y)
        {
            Vector = new IntVector(x, y);
            if (Sprite != null)
            {
                Sprite.Position = Vector.ToUIVector2() + new Vector2(10, 16);
            }
        }
    }
}
