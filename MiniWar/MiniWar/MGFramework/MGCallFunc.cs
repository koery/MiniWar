namespace MGFramework
{
    public class MGCallFunc : MGAction
    {
        #region Delegates

        public delegate void Callback();

        #endregion

        protected Callback _c;

        public static MGCallFunc ActionWithTarget(Callback c)
        {
            return new MGCallFunc
                       {
                           _c = c
                       };
        }

        public override void Step(float dt)
        {
            _c();
            _isEnd = true;
        }
    }
}