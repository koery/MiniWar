using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGTransition : MGScene
    {
        protected float Duration;
        protected float Elapsed;
        protected MGScene InScene;
        protected MGScene OutScene;
        protected Vector2[] P;
        protected float Ratio;

        protected MGTransition()
        {
            P = new Vector2[4];
            if (MGDirector.SharedDirector().Landscape)
            {
                P[0] = new Vector2(0f, Config.TARGET_WIN_HEIGHT);
                P[1] = new Vector2(0f, 0f);
                P[2] = new Vector2(Config.TARGET_WIN_WIDTH, Config.TARGET_WIN_HEIGHT);
                P[3] = new Vector2(Config.TARGET_WIN_WIDTH, 0f);
                return;
            }
            P[0] = new Vector2(0f, Config.TARGET_WIN_WIDTH);
            P[1] = new Vector2(0f, 0f);
            P[2] = new Vector2(Config.TARGET_WIN_HEIGHT, Config.TARGET_WIN_WIDTH);
            P[3] = new Vector2(Config.TARGET_WIN_HEIGHT, 0f);
        }

        public static MGTransition InitWithDuartion(float duration, MGScene inScene)
        {
            var transition = new MGTransition();
            transition.Elapsed = 0f;
            transition.InScene = inScene;
            transition.Duration = duration;
            transition.OutScene = MGDirector.SharedDirector().RunningScene;
            transition.AddChild(transition.InScene);
            transition.AddChild(transition.OutScene);
            return transition;
        }

        public virtual void Step(float time)
        {
            Ratio = time;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Elapsed += dt;
            float num = Elapsed/Duration;
            Step(Math.Min(1f, num));
            if (num >= 1f)
            {
                Finish();
            }
        }

        private void Finish()
        {
            MGDirector.SharedDirector().RunWithScene(InScene);
            InScene.OnSceneActive();
        }

        public override void DrawNode(SpriteBatch spriteBatch, MGCamera camera)
        {
            if (Ratio < 0.5)
            {
                OutScene.DrawNode(spriteBatch, camera);
                MGDirector.SharedDirector().BasicEffect.Alpha = (float) (Ratio/0.5);
                MGPrimitives.DrawPoly(P, Color.White);
                return;
            }
            InScene.DrawNode(spriteBatch, camera);
            MGDirector.SharedDirector().BasicEffect.Alpha = (float) (1.0 - (Ratio - 0.5)/0.5);
            MGPrimitives.DrawPoly(P, Color.White);
        }
    }
}