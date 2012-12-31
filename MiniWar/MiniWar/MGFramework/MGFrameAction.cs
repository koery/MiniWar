namespace MGFramework
{
    public class MGFrameAction : MGAction
    {
        protected string AnimName;
        protected FrameStruct FS;
        protected int Index;

        public static MGFrameAction ActionWithFrameStruct(FrameStruct FS)
        {
            return new MGFrameAction
                       {
                           FS = FS
                       };
        }

        public static MGFrameAction ActionWithAnimationIndex(string AnimName, int Index)
        {
            return new MGFrameAction
                       {
                           AnimName = AnimName,
                           Index = Index
                       };
        }

        public override void Step(float dt)
        {
            if (FS == null)
            {
                ((MGSprite) Target).SetFrame(AnimName, Index);
            }
            else
            {
                ((MGSprite) Target).FS = FS;
            }
            _isEnd = true;
        }
    }
}