using Microsoft.Xna.Framework;

namespace MGFramework
{
    public class MGScaleTo : MGAction
    {
        protected Vector2 Delta;
        protected Vector2 EndScale;
        protected Vector2 Scale;
        protected Vector2 StartScale;

        public static MGScaleTo ActionWithDuration(float duartion, Vector2 Scale)
        {
            return new MGScaleTo
                       {
                           EndScale = Scale
                       };
        }

        public static MGScaleTo ActionWithDuration(float duartion, float ScaleX, float ScaleY)
        {
            return new MGScaleTo
                       {
                           EndScale = new Vector2(ScaleX, ScaleY)
                       };
        }

        public static MGScaleTo ActionWithDuration(float duartion, float Scale)
        {
            return new MGScaleTo
                       {
                           EndScale = new Vector2(Scale, Scale)
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            StartScale = node.Scale;
            Delta = StartScale - EndScale;
        }

        public override void Update(float t)
        {
            if (t == 1f)
            {
                _isEnd = true;
                Target.Scale = EndScale;
                return;
            }
            Target.Scale = t*Delta + StartScale;
        }
    }
}