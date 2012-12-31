namespace MGFramework
{
    public class MGShow : MGAction
    {
        public override void Step(float dt)
        {
            Target.Visible = true;
            _isEnd = true;
        }
    }
}