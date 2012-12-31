namespace MGFramework
{
    public class MGToggleVisibility : MGAction
    {
        public override void Step(float dt)
        {
            bool visible = Target.Visible;
            Target.Visible = !visible;
            _isEnd = true;
        }
    }
}