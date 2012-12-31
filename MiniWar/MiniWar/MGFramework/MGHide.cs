namespace MGFramework
{
    public class MGHide : MGAction
    {
        public override void Step(float dt)
        {
            Target.Visible = false;
            _isEnd = true;
        }
    }
}