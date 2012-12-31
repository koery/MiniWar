using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGScaleBy : MGScaleTo
    {
        public new static MGScaleBy ActionWithDuration(float duartion, Vector2 scale)
        {
            return new MGScaleBy
                       {
                           Delta = scale
                       };
        }

        public new static MGScaleBy ActionWithDuration(float duartion, float scaleX, float scaleY)
        {
            return new MGScaleBy
                       {
                           Delta = new Vector2(scaleX, scaleY)
                       };
        }

        public new static MGScaleBy ActionWithDuration(float duartion, float scale)
        {
            return new MGScaleBy
                       {
                           Delta = new Vector2(scale, scale)
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            StartScale = node.Scale;
            EndScale = StartScale + EndScale;
        }
    }
}