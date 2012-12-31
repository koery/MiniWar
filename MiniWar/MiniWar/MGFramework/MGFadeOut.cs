namespace MGFramework
{
    public class MGFadeOut : MGFadeIn
    {
        public new static MGFadeOut ActionWithDuration(float duartion)
        {
            return new MGFadeOut
                       {
                           TarOpacity = 0f,
                           _duration = duartion
                       };
        }

        public override void SetTarget(MGNode node)
        {
            FirstTick = true;
            _isEnd = false;
            Target = node;
            OrgOpacity = node.Opacity;
            Diff = TarOpacity - OrgOpacity;
        }
    }
}