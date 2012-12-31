using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace VSZombie.Effect
{
    public class ElectricEffect
    {
        #region Properties

        private readonly SpriteBatch _spriteBatch;
        public int Density = 10;
        public List<ElectricFlow> ElectricFlows = new List<ElectricFlow>();
        public float SpeedFactor = 1f;
        public List<Texture2D> Textures = new List<Texture2D>();

        #endregion

        #region Constructor

        public ElectricEffect(SpriteBatch spriteBatch, List<Texture2D> textures)
        {
            _spriteBatch = spriteBatch;
            Textures = textures;
        }

        #endregion

        #region Update & Draw

        public void Update(float elapsedTime)
        {
            foreach (ElectricFlow flow in ElectricFlows)
            {
                flow.Update(elapsedTime);
            }
        }

        public void Draw()
        {
            if (ElectricFlows.Any(flow => flow.ElectricNodes.Count < 3))
            {
                return;
            }
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            foreach (ElectricFlow flow in ElectricFlows)
            {
                if (flow.ElectricNodes.Count < 3)
                {
                    return;
                }
                flow.Draw();
            }
            _spriteBatch.End();
        }

        #endregion

        public ElectricFlow AddFlow()
        {
            var flow = new ElectricFlow(_spriteBatch, Textures) { Density = Density };
            ElectricFlows.Add(flow);
            return flow;
        }

        public void ClearFlows()
        {
            ElectricFlows.Clear();
        }
    }
}